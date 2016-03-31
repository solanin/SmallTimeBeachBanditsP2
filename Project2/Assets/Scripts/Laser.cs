using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    private Player player;
    private int direction;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        direction = player.GetDirection();
        print(player.GetDirection());
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.GetLaser())
        {
            Destroy(this.gameObject);
        }
        else
        {
            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
            float distance = 13.5f;
            if (obstacles.Length > 0)
            {
                for (int i = 0; i < obstacles.Length; i++)
                {
                    if (direction > 0 && (obstacles[i].transform.position.x  - (player.transform.position.x + 0.5f)) < distance)
                    {
                        distance = obstacles[i].transform.position.x - player.transform.position.x;
                    }
                    else if (direction < 0 && (player.transform.position.x - 0.5f - obstacles[i].transform.position.x) > distance)
                    {
                        distance = player.transform.position.x - obstacles[i].transform.position.x;
                    }
                }
            }

            transform.localScale = new Vector3(distance, 1.0f, 1.0f);
            transform.position = new Vector3(player.transform.position.x + (direction * distance / 2) + (direction* 0.5f), player.transform.position.y, player.transform.position.z);
        }
        
    }
}
