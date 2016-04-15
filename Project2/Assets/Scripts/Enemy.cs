using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    GameObject player;
    Rigidbody body;
    GameObject self;
    EnemyManager em;
    public float speed = 25.0f;
	private Health health;
    public float damage = 1.0f;
    public bool alive = true;
    public int index;
    float laserCounter = 0.5f;
    float fireballCounter = 0.3f;
    bool fireball = false;
	bool laser = false;
    public bool jumper = false;
	bool canJump;

    int fireballCount = 0;

    public GameObject healthDrop;
    public GameObject machineDrop;
    public GameObject fireballDrop;
    public GameObject laserDrop;

    // Use this for initialization
    void Start () {
        em = GameObject.FindGameObjectWithTag("Respawn").GetComponent<EnemyManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        body = this.GetComponentInParent<Rigidbody>();
        self = body.gameObject;
		alive = true;
        canJump = true;
		health = GetComponent<Health>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 displacement = (player.transform.position - this.transform.position);
        displacement.y = 0.0f; 
        body.AddForce((displacement.normalized* speed));
        if(Mathf.Abs(transform.position.x - player.transform.position.x) > 20.0f)
        {
            Destroy(self);
            alive = false;
            em.GenerateEnemy(index);
        }

        if (laser)
        {
            if (laserCounter == 0.5f)
			{
				health.takeDamage(0.05f);
                laserCounter = 0.0f;
            }
            else
            {
                laserCounter -= Time.deltaTime;
                if (laserCounter <= 0.0f)
                {
                    laserCounter = 0.5f;
                }
            }
            if (!player.GetComponent<Player>().getLaser())
            {
                laser = false;
            }
        }
        if (fireball)
        {
            if (fireballCounter == 0.3f)
			{
				health.takeDamage(0.1f);
                fireballCounter = 0.0f;
            }
            else
            {
                fireballCounter -= Time.deltaTime;
                if (fireballCounter <= 0.0f)
                {
                    fireballCounter = 0.3f;
                }
            }
        }
        if (jumper && canJump && player.transform.position.y - self.transform.position.y > 1)
        {
            body.velocity = new Vector3(body.velocity.x, 30.0f);
            canJump = false;

            Physics.IgnoreLayerCollision(9, 12, true);
        }

        if (!canJump)
        {
            if (body.velocity.y < 0.0f)
            {
                Physics.IgnoreLayerCollision(9, 12, false);
            }


            RaycastHit hit;

            if (Physics.Raycast(transform.position, -Vector3.up, out hit, 1.2f) && body.velocity.y <= 0.0f)
            {
                if (hit.distance <= 1.01f)
                {
                    canJump = true;
                }
            }
        }

		if (health.isDead())
        {
            int drop = Random.Range(0, 20);

            if ( drop < 12)
            {
                //drop nothing
            }
            else if (drop < 15)
            {
                // drop health
                Instantiate(healthDrop, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
            }
            else if (drop < 17)
            {
                //drop machine gun
                Instantiate(machineDrop, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
            }
            else if (drop < 19)
            {
                //drop fire ball
                Instantiate(fireballDrop, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
            }
            else if (drop == 19)
            {
                //drop laser
                Instantiate(laserDrop, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
            }

            Destroy(self);
            alive = false;
			em.GenerateEnemy(index);
            GameObject.Find("UI").GetComponent<UI>().AddToScore();
            GameObject.Find("UI").GetComponent<UI>().UpdateScore();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.tag)
        {
            case "Bullet":
                GameObject.Destroy(col.gameObject);
				health.takeDamage(0.3f);
                Destroy(col.gameObject);
                break;
            case "Fireball":
                
                fireball = true;
                fireballCount++;
                break;
            case "Ground":
                canJump = true;
                break;
            case "Platform":
                canJump = true;
                break;
            default:
                break;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Laser")
        {
            laser = false;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Fireball")
        {
            fireball = true;
            fireballCount++;
        }
        else if (col.gameObject.tag == "Laser")
        {
            laser = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag =="Fireball")
        {
            fireballCount--;
            if (fireballCount <= 0)
            {
                fireball = false;
            }
        }
        else if (col.gameObject.tag == "Laser")
        {
            laser = false;
        }
    }
}
