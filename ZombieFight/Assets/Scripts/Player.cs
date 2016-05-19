using UnityEngine;
using XboxCtrlrInput;
using System.Collections;

public class Player : MonoBehaviour
{
    GameObject bg;
    GameObject self;
    Rigidbody rigidBody;
    bool isJumping;
    bool restRightStick = true;
    bool restRightTrigger = true;

    int direction = 1;
    public int Direction { get { return direction; } }
    private float shootX = 0.0f;
    public float ShootX { get { return shootX; } }
    private float shootY = 0.0f;
    public float ShootY { get { return shootY; } }

    private Health health;
    private GameManager gm;

    //list of bullets remaining (pistol, machine gun, fireball, laser in sec, sniper, shot gun, granade)
    float[] bullets = new float[7] { -1.0f, 200.0f, 10.0f, 10.0f, 4.0f, 25.0f, 10.0f };

    //index of currently equipped weapon
    int currentWeapon = 0;

    public GameObject pistolBulletPrefab = null;
    public GameObject machineGunBulletPrefab = null;
    public GameObject laserPrefab = null;
    public GameObject fireballPrefab = null;
    public GameObject sniperBulletPrefab = null;
    public GameObject shotGunPrefab = null;
    public GameObject grenadePrefab = null;

    float bulletCool = 0.15f;
    float sniperCool = 1.5f;
    float shotgunCool = 1.5f;
    float fireballCool = 1.0f;
    float grenadeCool = 2.0f;

    bool laser = false;
    public bool Laser { get { return laser; } }

    bool immune = false;
    bool visible = true;
    float flashTimer = 0.1f;
    float playerFlashTime = 1.0f;

    bool sniper, grenade, shotgun;

    void Start()
    {
        self = GameObject.FindGameObjectWithTag("Player");
        bg = GameObject.Find("BG");
        rigidBody = self.GetComponent<Rigidbody>();
        direction = 1;

        health = GetComponent<Health>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (PlayerPrefs.GetInt("Unlock 1") == 0)
        {
            sniper = false;
        }
        else { sniper = true; }

        if (PlayerPrefs.GetInt("Unlock 2") == 0)
        {
            grenade = false;
        }
        else { grenade = true; }

        if (PlayerPrefs.GetInt("Unlock 3") == 0)
        {
            shotgun = false;
        }
        else { shotgun = true; }
    }

    void Update()
	{
		if (!GameManager.isPaused) {
			transform.GetChild (0).transform.position = new Vector3 (self.transform.position.x, self.transform.position.y + 1.25f, -5.0f);

			if (!health.isDead ()) {
				if (bulletCool < 0.15f)
					bulletCool += Time.deltaTime;
				if (sniperCool < 1.5f)
					sniperCool += Time.deltaTime;
                if (shotgunCool < 1.5f)
                    shotgunCool += Time.deltaTime;
                if (fireballCool < 1.0f)
                    fireballCool += Time.deltaTime;
                if (grenadeCool < 1.0f)
                    grenadeCool += Time.deltaTime;

                if (immune) {
					playerFlashTime -= Time.deltaTime;
					Flash ();
					if (playerFlashTime <= 0.0f) {
						immune = false;
						playerFlashTime = 1.0f;
						flashTimer = 0.1f;
						GetComponentInChildren<MeshRenderer> ().enabled = true;
						visible = true;
					}
				}

				shootX = 0.0f;
				shootY = 0.0f;


				// XBox Controller Inputs
				if (XCI.GetAxis (XboxAxis.LeftStickX) != 0.0f) {
					rigidBody.velocity = new Vector3 (10.0f * XCI.GetAxis (XboxAxis.LeftStickX), rigidBody.velocity.y);

					if (XCI.GetAxis (XboxAxis.LeftStickX) > 0.0f) {
						direction = 1;
					} else {
						direction = -1;
					}
				}
				if ((XCI.GetAxis (XboxAxis.LeftStickY) > 0.5f) && !isJumping) {
					isJumping = true;
					rigidBody.velocity = new Vector3 (rigidBody.velocity.x, 60.0f);

					Physics.IgnoreLayerCollision (8, 12, true);
				}

				if (XCI.GetButtonDown (XboxButton.RightBumper)) {
					if (laser) {
						laser = false;
					}
					currentWeapon++;

                    if (!sniper && currentWeapon == 4)
                    {
                        currentWeapon++;
                    }

                    if (!shotgun && currentWeapon == 5)
                    {
                        currentWeapon++;
                    }
                    if (!grenade && currentWeapon == 6)
                    {
                        currentWeapon++;
                    }

					if (currentWeapon > 6) {
						currentWeapon = 0;
					}
					gm.UpdateUI (currentWeapon, bullets);
				}

				if (XCI.GetButtonDown (XboxButton.LeftBumper)) {
					if (laser) {
						laser = false;
					}
					currentWeapon--;
					if (currentWeapon < 0) {
						currentWeapon = 6;
					}


                    if (!grenade && currentWeapon == 6)
                    {
                        currentWeapon--;
                    }

                    if (!shotgun && currentWeapon == 5)
                    {
                        currentWeapon--;
                    }


                    if (!sniper && currentWeapon == 4)
                    {
                        currentWeapon--;
                    }

                    gm.UpdateUI (currentWeapon, bullets);
				}

				if (XCI.GetAxis (XboxAxis.RightStickX) > 0.5f || XCI.GetAxis (XboxAxis.RightStickX) < -0.5f || XCI.GetAxis (XboxAxis.RightStickY) > 0.5f || XCI.GetAxis (XboxAxis.RightStickY) < -0.5f) {
					shootX = XCI.GetAxis (XboxAxis.RightStickX);
					shootY = XCI.GetAxis (XboxAxis.RightStickY);
                    if (bullets[currentWeapon] == 0)
                    {
                        currentWeapon = 0;
                        gm.UpdateUI(currentWeapon, bullets);
                    }
					fireBullet ();
					restRightStick = false;
				}
            
				if (XCI.GetAxis (XboxAxis.RightStickX) < 0.5f && XCI.GetAxis (XboxAxis.RightStickX) > -0.5f && XCI.GetAxis (XboxAxis.RightStickY) < 0.5f && XCI.GetAxis (XboxAxis.RightStickY) > -0.5f) {
					restRightStick = true;
					shootX = 0.0f;
					shootY = 0.0f;
					laser = false;
				}

				if (XCI.GetAxis (XboxAxis.RightTrigger) > 0.5) {
					if (restRightTrigger) {
                        if (bullets[currentWeapon] == 0)
                        {
                            currentWeapon = 0;
                            gm.UpdateUI(currentWeapon, bullets);
                        }
                        fireShots();
						restRightTrigger = false;
					}
				} else {
					restRightTrigger = true;
				}
            
				//keyboard controls
				if (Input.GetKey (KeyCode.A)) {
					rigidBody.velocity = new Vector3 (-10.0f, rigidBody.velocity.y);
					direction = -1;
				}
				if (Input.GetKey (KeyCode.D)) {
					rigidBody.velocity = new Vector3 (10.0f, rigidBody.velocity.y);
					direction = 1;
				}
				if (Input.GetKeyDown (KeyCode.W) && !isJumping) {
					isJumping = true;
					rigidBody.velocity = new Vector3 (rigidBody.velocity.x, 60.0f);

					Physics.IgnoreLayerCollision (8, 12, true);
				}

				if (Input.GetKeyUp (KeyCode.E)) {
                    if (laser)
                    {
                        laser = false;
                    }
                    currentWeapon++;

                    if (!sniper && currentWeapon == 4)
                    {
                        currentWeapon++;
                    }

                    if (!shotgun && currentWeapon == 5)
                    {
                        currentWeapon++;
                    }
                    if (!grenade && currentWeapon == 6)
                    {
                        currentWeapon++;
                    }

                    if (currentWeapon > 6)
                    {
                        currentWeapon = 0;
                    }
                    gm.UpdateUI(currentWeapon, bullets);
                }
				if (Input.GetKeyUp (KeyCode.Q)) {
                    if (laser)
                    {
                        laser = false;
                    }
                    currentWeapon--;
                    if (currentWeapon < 0)
                    {
                        currentWeapon = 6;
                    }


                    if (!grenade && currentWeapon == 6)
                    {
                        currentWeapon--;
                    }

                    if (!shotgun && currentWeapon == 5)
                    {
                        currentWeapon--;
                    }


                    if (!sniper && currentWeapon == 4)
                    {
                        currentWeapon--;
                    }

                    gm.UpdateUI(currentWeapon, bullets);
                }

				//shooting direction
				if (Input.GetKey (KeyCode.U) || Input.GetKey (KeyCode.O) || Input.GetKey (KeyCode.M) || Input.GetKey (KeyCode.Period)) {
					if (Input.GetKey (KeyCode.U)) {
						shootX -= 1.0f;
						shootY += 1.0f;
					}
					if (Input.GetKey (KeyCode.O)) {
						shootX += 1.0f;
						shootY += 1.0f;
					}
					if (Input.GetKey (KeyCode.M)) {
						shootX -= 1.0f;
						shootY -= 1.0f;
					}
					if (Input.GetKey (KeyCode.Period)) {
						shootX += 1.0f;
						shootY -= 1.0f;
					}
				} else {
					if (Input.GetKey (KeyCode.J)) {
						shootX -= 1.0f;
					} else if (Input.GetKey (KeyCode.L)) {
						shootX += 1.0f;
					} else if (Input.GetKey (KeyCode.I)) {
						shootY += 1.0f;
					} else if (Input.GetKey (KeyCode.K)) {
						shootY -= 1.0f;
					}
				}
            

				if (Input.GetKeyDown (KeyCode.U) || Input.GetKeyDown (KeyCode.O) || Input.GetKeyDown (KeyCode.M) || Input.GetKeyDown (KeyCode.Period)) {
                    if (bullets[currentWeapon] == 0)
                    {
                        currentWeapon = 0;
                        gm.UpdateUI(currentWeapon, bullets);
                    }
					fireShots ();
				} else if (Input.GetKeyDown (KeyCode.L) || Input.GetKeyDown (KeyCode.I) || Input.GetKeyDown (KeyCode.J) || Input.GetKeyDown (KeyCode.K)) {
                    if (bullets[currentWeapon] == 0)
                    {
                        currentWeapon = 0;
                        gm.UpdateUI(currentWeapon, bullets);
                    }
                    fireShots ();
				}

				if (Input.GetKey (KeyCode.L) || Input.GetKey (KeyCode.I) || Input.GetKey (KeyCode.J) || Input.GetKey (KeyCode.K) || Input.GetKey (KeyCode.U) || Input.GetKey (KeyCode.O) || Input.GetKey (KeyCode.M) || Input.GetKey (KeyCode.Period)) {
                    if (bullets[currentWeapon] == 0)
                    {
                        currentWeapon = 0;
                        gm.UpdateUI(currentWeapon, bullets);
                        fireShots();
                    }
                    else
                    {
                        fireBullet();
                    }
                    
				} else if (restRightStick) {
					laser = false;
				}
			}

			// Screen Wrap
			if (transform.position.x > (bg.transform.position.x + 50f)) {
				bg.transform.position = new Vector3 (bg.transform.position.x + 100f, bg.transform.position.y, bg.transform.position.z);
			} else if (transform.position.x < (bg.transform.position.x - 50f)) {
				bg.transform.position = new Vector3 (bg.transform.position.x - 100f, bg.transform.position.y, bg.transform.position.z);
			}

			//laser ammo check
			if (laser) {
				bullets [3] -= Time.deltaTime;
				if (bullets [3] <= 0.0f) {
					laser = false;
					bullets [3] = 0.0f;
				}
				gm.UpdateAmo (bullets [3]);
			}

			if (isJumping) {
				if (rigidBody.velocity.y < 0.0f) {
					Physics.IgnoreLayerCollision (8, 12, false);
				}

				RaycastHit hit;

				if (Physics.Raycast (transform.position, -Vector3.up, out hit, 1.2f) && rigidBody.velocity.y <= 0.0f) {
					if (hit.distance <= 1.01f) {
						isJumping = false;
					}
				}
			}
		}
	}


    void fireBullet()
    {
        switch (currentWeapon)
        {
            default:
                break;
            case 1:
                if (bulletCool >= 0.15f && bullets[currentWeapon] > 0)
                {
                    Instantiate(machineGunBulletPrefab, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
                    bulletCool = 0.0f;
                    bullets[currentWeapon] -= 4;
                    gm.UpdateAmo(bullets[currentWeapon]);
                }
                break;
            case 3:
                if (!laser && bullets[currentWeapon] > 0.0f)
                {
                    Instantiate(laserPrefab, new Vector3(transform.position.x, transform.position.y, 0.5f), Quaternion.identity);
                    laser = true;
                    gm.UpdateAmo(bullets[currentWeapon]);
                }
                break;
        }
    }

    void fireShots()
    {
        switch (currentWeapon)
        {
            case 0:
                Instantiate(pistolBulletPrefab, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
                break;
            case 2:
                if (bullets[currentWeapon] > 0 && fireballCool >= 1.0f)
                {
                    Instantiate(fireballPrefab, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
                    fireballCool = 0.0f;
                    bullets[currentWeapon] -= 1;
                    gm.UpdateAmo(bullets[currentWeapon]);
                }
                break;
            case 4:
                if (bullets[currentWeapon] > 0 && sniperCool >= 1.5f)
                {
                    Instantiate(sniperBulletPrefab, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
                    sniperCool = 0.0f;
                    bullets[currentWeapon] -= 1;
                    gm.UpdateAmo(bullets[currentWeapon]);
                }
                break;
            case 5:
                if (bullets[currentWeapon] > 0 && shotgunCool >= 1.5f)
                {
                    Instantiate(shotGunPrefab, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
                    shotgunCool = 0.0f;
                    bullets[currentWeapon] -= 5;
                    gm.UpdateAmo(bullets[currentWeapon]);
                }
                break;
            case 6:
                if (bullets[currentWeapon] > 0 && grenadeCool >= 2.0f)
                {
                    Instantiate(grenadePrefab, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
                    grenadeCool = 2.0f;
                    bullets[currentWeapon] -= 1;
                    gm.UpdateAmo(bullets[currentWeapon]);
                }
                break;
            default:
                break;
        }
    }

    void Flash()
    {
        flashTimer -= Time.deltaTime;
        if (flashTimer <= 0.0f)
        {
            if (visible)
            {
                GetComponentInChildren<MeshRenderer>().enabled = false;
                visible = false;
            }
            else
            {
                GetComponentInChildren<MeshRenderer>().enabled = true;
                visible = true;
            }
            flashTimer = 0.1f;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.tag)
        {
            case "Enemy":
                if (!immune && !health.isDead())
                {
                    immune = true;
                    health.takeDamage(10.0f);
                    if (health.isDead())
                    {
                        gm.EndGame();
                    }
                }
                break;
            case "HealthDrop":
                health.gainHealth(10.0f);
                Destroy(col.gameObject);
                break;
            case "MachineDrop":
                bullets[1] += 100.0f;
                if (bullets[1] > 200.0f)
                    bullets[1] = 200.0f;
                if (currentWeapon == 1)
                    gm.UpdateAmo(bullets[1]);
                Destroy(col.gameObject);
                break;
            case "FireballDrop":
                bullets[2] += 5.0f;
                if (bullets[2] > 10.0f)
                    bullets[2] = 10.0f;
                if (currentWeapon == 2)
                    gm.UpdateAmo(bullets[2]);
                Destroy(col.gameObject);
                break;
            case "LaserDrop":
                bullets[3] += 5.0f;
                if (bullets[3] > 10.0f)
                    bullets[3] = 10.0f;
                if (currentWeapon == 3)
                    gm.UpdateAmo(bullets[3]);
                Destroy(col.gameObject);
                break;
            case "SniperDrop":
                bullets[4] += 2.0f;
                if (bullets[4] > 4.0f)
                    bullets[4] = 4.0f;
                if (currentWeapon == 4)
                    gm.UpdateAmo(bullets[4]);
                Destroy(col.gameObject);
                break;
            case "ShotgunDrop":
                bullets[5] += 15.0f;
                if (bullets[5] > 25.0f)
                    bullets[5] = 25.0f;
                if (currentWeapon == 5)
                    gm.UpdateAmo(bullets[5]);
                Destroy(col.gameObject);
                break;
            case "GrenadeDrop":
                bullets[6] += 3.0f;
                if (bullets[6] > 5.0f) 
                    bullets[6] = 5.0f;
                if (currentWeapon == 6)
                    gm.UpdateAmo(bullets[6]);
                Destroy(col.gameObject);
                break;
        }

    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Enemy" && !immune && !health.isDead())
        {
            immune = true;
            health.takeDamage(10.0f);
            if (health.isDead())
            {
                gm.EndGame();
            }
        }
    }


}

