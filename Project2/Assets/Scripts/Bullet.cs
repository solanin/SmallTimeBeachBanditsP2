using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed = 15.0f;
    private Player player;
    public float damage = 1.0f;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        speed *= (float)player.GetDirection();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + Time.deltaTime * speed, transform.position.y, transform.position.z);
        if (transform.position.x - player.transform.position.x > 14.0 || transform.position.x - player.transform.position.x < -14.0)
        {
            Destroy(this.gameObject);
        }
    }
}
