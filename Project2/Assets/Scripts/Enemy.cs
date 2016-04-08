using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    GameObject player;
    Rigidbody body;
    GameObject self;
    EnemyManager em;
    public float speed = 25.0f;
    public float health = 2.0f;
    public float damage = 1.0f;
    public bool alive = true;
    public int index;
    float laserCounter = 0.5f;
    float fireballCounter = 0.7f;
    bool fireball = false;
    bool laser = false;

    int fireballCount = 0;

	// Use this for initialization
	void Start () {
        em = GameObject.FindGameObjectWithTag("Respawn").GetComponent<EnemyManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        body = this.GetComponentInParent<Rigidbody>();
        self = body.gameObject;
        alive = true;
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
            if (laserCounter == 0.7f)
            {
                health -= 0.1f;
            }
            else
            {
                laserCounter -= Time.deltaTime;
                if (laserCounter <= 0.0)
                {
                    laserCounter = 0.7f;
                }
            }
        }
        if (fireball)
        {
            if (fireballCounter == 0.5f)
            {
                health -= 0.1f;
            }
            else
            {
                fireballCounter -= Time.deltaTime;
                if (fireballCounter <= 0.0)
                {
                    fireballCounter = 0.5f;
                }
            }
        }


        if (health <= 0.0f)
        {
            Destroy(self);
            alive = false;
            em.GenerateEnemy(index);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.tag)
        {
            case "Bullet":
                GameObject.Destroy(col.gameObject);
                health -= 1.0f;
                if (health <= 0.0f)
                {
                    Destroy(self);
                    alive = false;
                    em.GenerateEnemy(index);
                }
                Destroy(col.gameObject);
                break;
            case "Laser":
                laser = true;
                break;
            case "Fireball":
                
                fireball = true;
                fireballCount++;
                break;
            default:
                break;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Fireball")
        {
            fireball = true;
            fireballCount++;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Fireball")
        {
            fireballCount--;
            if (fireballCount <= 0)
            {
                fireball = false;
            }
        }
    }
    void OnCollisionExit(Collision col)
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
