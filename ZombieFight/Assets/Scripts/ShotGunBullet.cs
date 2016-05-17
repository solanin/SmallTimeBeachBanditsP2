using UnityEngine;
using System.Collections;

public class ShotGunBullet : MonoBehaviour
{
    private Player player;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x - player.transform.position.x > 14.0 || transform.position.x - player.transform.position.x < -14.0 || transform.position.y - player.transform.position.y > 14.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
