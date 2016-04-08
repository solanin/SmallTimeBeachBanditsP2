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
	}

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.name == "Bullet")
        {
            GameObject.Destroy(col.gameObject);
            health -= 1.0f;
            if(health <= 0.0f)
            {
                Destroy(self);
                alive = false;
                em.GenerateEnemy(index);
            }
            Destroy(col.gameObject);
        }
    }
}
