using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour
{

    public float speed = 0.5f;
    private Player player;
    public float life = 14; //active in seconds
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        speed *= (float)player.GetDirection();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + Time.deltaTime*speed, transform.position.y, transform.position.z);
        life -= Time.deltaTime;
        if (life <= 0 || transform.position.x - player.transform.position.x > 14.0 || transform.position.x - player.transform.position.x < -14.0)
        {
            Destroy(this.gameObject);
        }
    }
}
