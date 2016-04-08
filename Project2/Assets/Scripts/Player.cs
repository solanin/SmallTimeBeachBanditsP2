using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    GameObject bg;
    GameObject self;
    Rigidbody rigidBody;
    bool isJumping;
    int direction = 1;
    private bool shootUp = false;
    private bool shootRight = false;
    private bool shootLeft = false;
    private Health health;

    //list of usable weapons
    //for the sake of this deliverable, all weapons will be allowed
    string[] weapons = new string[4] { "pistol", "machineGun", "laser", "fireball" };

    //list of bullets remaining
    float[] bullets = new float[4] { 1.0f, 200.0f, 10.0f, 10.0f };

    //index of currently equipped weapon
    int currentWeapon = 0;

    public GameObject pistolBulletPrefab = null;
    public GameObject machineGunBulletPrefab = null;
    public GameObject laserPrefab = null;
    public GameObject fireballPrefab = null;

    public int fireballDamage = 10;
    public int pistolDamage = 2;
    public int machineGunDamage = 2;
    public int laserDamage = 10;

    float bulletCool = 0.15f;
    bool laser = false;
    float prevY;

    bool immune = false;
    bool visible = true;
    float flashTimer = 0.1f;
    float playerFlashTime = 1.0f;


    // Use this for initialization
    void Start()
    {
        self = GameObject.FindGameObjectWithTag("Player");
        bg = GameObject.Find("BG");
        rigidBody = self.GetComponent<Rigidbody>();
        direction = 1;

        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletCool < 0.15f) bulletCool += Time.deltaTime;
        if (immune)
        {
            playerFlashTime -= Time.deltaTime;
            Flash();
            if (playerFlashTime <= 0.0f)
            {
                immune = false;
                playerFlashTime = 1.0f;
                flashTimer = 0.1f;
                GetComponentInChildren<MeshRenderer>().enabled = true;
                visible = true;
            }
        }

		if (!health.isDead()) {
	        if (Input.GetKey(KeyCode.A))
	        {
	            rigidBody.velocity = new Vector3(-10.0f, rigidBody.velocity.y);
	            direction = -1;
	        }
	        if (Input.GetKey(KeyCode.D))
	        {
	            rigidBody.velocity = new Vector3(10.0f, rigidBody.velocity.y);
	            direction = 1;
	        }
	        if (Input.GetKeyDown(KeyCode.W) && !isJumping)
	        {
	            isJumping = true;
	            rigidBody.velocity = new Vector3(rigidBody.velocity.x, 60.0f);
	        }

	        if (Input.GetKey(KeyCode.Space))
	        {
	            fireBullet();
	        }
	        if (Input.GetKeyUp(KeyCode.Space))
	        {
	            fireShots();
	        }
	        if (Input.GetKeyUp(KeyCode.R))
	        {
	            currentWeapon++;
	            if (currentWeapon > 3)
	            {
	                currentWeapon = 0;
	            }
	        }
	        if (Input.GetKeyUp(KeyCode.E))
	        {
	            currentWeapon--;
	            if (currentWeapon < 0)
	            {
	                currentWeapon = 3;
	            }
	        }

	        //shooting direction
	        if (Input.GetKeyDown(KeyCode.I))
	        {
	            shootUp = true;
	        }

	        if (Input.GetKeyUp(KeyCode.I))
	        {
	            shootUp = false;
	        }

	        if (Input.GetKeyDown(KeyCode.J))
	        {
	            shootLeft = true;
	        }

	        if (Input.GetKeyUp(KeyCode.J))
	        {
	            shootLeft = false;
	        }

	        if (Input.GetKeyDown(KeyCode.L))
	        {
	            shootRight = true;
	        }

	        if (Input.GetKeyUp(KeyCode.L))
	        {
	            shootRight = false;
	        }
		}

        // Screen Wrap
        if (transform.position.x > (bg.transform.position.x + 50f))
        {
            bg.transform.position = new Vector3(bg.transform.position.x + 100f, bg.transform.position.y, bg.transform.position.z);
        }
        else if (transform.position.x < (bg.transform.position.x - 50f))
        {
            bg.transform.position = new Vector3(bg.transform.position.x - 100f, bg.transform.position.y, bg.transform.position.z);
        }

        //laser ammo check
        if (laser)
        {
            bullets[2] -= Time.deltaTime;
            if (bullets[2] < 0.0f)
            {
                laser = false;
            }
        }

        if (isJumping)
        {
            //if (prevY == transform.position.y && rigidBody.velocity.y == 0.0f)
            //{
            //    isJumping = false;
            //    //Debug.Log("landed");
            //}

            RaycastHit hit;

            if (Physics.Raycast(transform.position, -Vector3.up, out hit, 1.2f) && rigidBody.velocity.y <= 0.0f)
            {
                if (hit.distance <= 1.01f)
                {
                    isJumping = false;
                    //Debug.Log("Landed");
                }
            }
        }

        // Update last pos
        prevY = transform.position.y;
    }


    void fireBullet()
    {
        //GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //bullet.tag = "Bullet";
        //bullet.transform.localScale = new Vector3(.1f, .1f, .1f);
        //bullet.transform.position = this.transform.position + new Vector3(direction, 0, 0);
        //bullet.AddComponent<Rigidbody>();
        //bullet.GetComponent<Rigidbody>().velocity = new Vector3(30.0f * direction, 0.0f, 0.0f);
        //bullet.GetComponent<Rigidbody>().useGravity = false;
        //Destroy(bullet, 1.50f);

        switch (currentWeapon)
        {
            default:
                break;
            case 1:
                if (bulletCool >= 0.15f && bullets[1] > 0)
                {
                    Instantiate(machineGunBulletPrefab, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
                    bulletCool = 0.0f;
                    bullets[1] -= 4;
                }
                break;
            case 2:
                if (!laser && bullets[2] > 0.0f)
                {

                    Instantiate(laserPrefab, new Vector3(transform.position.x, transform.position.y, 0.5f), Quaternion.identity);
                    laser = true;
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
                laser = false;
                break;
            case 3:
                if (bullets[3] > 0)
                {
                    Instantiate(fireballPrefab, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
                    bullets[3] -= 1;
                }
                break;
            default:
                break;
        }
    }

    /*
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isJumping = true;
        }
    }
	*/

    public int GetDirection()
    {
        return direction;
    }

    public bool GetLaser()
    {
        return laser;
    }
    public bool GetShootUp()
    {
        return shootUp;
    }
    public bool GetShootLeft()
    {
        return shootLeft;
    }
    public bool GetShootRight()
    {
        return shootRight;
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
		if (col.gameObject.tag == "Enemy" && !immune && !health.isDead())
        {
            immune = true;
            health.takeDamage(5.0f);
			if (health.isDead())
			{
				EndGame();
			}
        }
    }

    void OnCollisionStay(Collision col)
    {
		if (col.gameObject.tag == "Enemy" && !immune && !health.isDead())
        {
            immune = true;
            health.takeDamage(5.0f);
			if (health.isDead())
            {
				EndGame();
            }
        }
    }

	void EndGame(){
		GameObject.Find("GO").transform.position = new Vector3(transform.position.x, 2.5f, -5);
		GameObject.Find("GO").GetComponent<MeshRenderer>().enabled = true;
		GameObject.Find("GOText").GetComponent<MeshRenderer>().enabled = true;
	}
}

