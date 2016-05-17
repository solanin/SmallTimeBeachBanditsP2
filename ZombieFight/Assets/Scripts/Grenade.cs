using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour
{
    float force = 15.0f;
    float growth = 250.0f;
    private Player player;
    private Vector2 shootDirection;
    Rigidbody rigidBody;
    bool grow = false;
    float growTimer = 1.0f;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        shootDirection.x = player.ShootX;
        shootDirection.y = player.ShootY;
        shootDirection.Normalize();

        if (shootDirection.magnitude == 0.0f)
        {
            shootDirection.x = (float)player.Direction;
        }
        shootDirection *= force;

        rigidBody = this.GetComponent<Rigidbody>();
        rigidBody.velocity = shootDirection;
        transform.GetChild(0).transform.position = transform.position;
        Destroy(this.gameObject, 1.25f);
    }

    // Update is called once per frame
    void Update()
    {
        growTimer -= Time.deltaTime;
        if (growTimer <= 0.0f)
        {
            grow = true;
        }
        transform.GetChild(0).transform.position = transform.position;
        if (grow)
        {
            transform.GetChild(0).transform.localScale += new Vector3(growth * Time.deltaTime, growth * Time.deltaTime, 0.0f);
        }
    }
}
