using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    private Player player;
    private int direction = 0;
    bool diagonal = false;
    RaycastHit hit;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if ((!player.GetShootLeft() && !player.GetShootRight() && !player.GetShootUp()) || (player.GetShootLeft() && player.GetShootRight() && !player.GetShootUp())) // if nothing is pressed or just left and right are pressed
        {
            direction = player.GetDirection();
        }
        else if (player.GetShootLeft() && !player.GetShootRight() && !player.GetShootUp()) // shoot left
        {
            direction = -1;
        }
        else if (!player.GetShootLeft() && player.GetShootRight() && !player.GetShootUp()) //shoot right
        {
            direction = 1;
        }
        else if ((!player.GetShootLeft() && !player.GetShootRight() && player.GetShootUp()) || (player.GetShootLeft() && player.GetShootRight() && !player.GetShootUp())) //shoot up
        {
            direction = 0;
        }
        else if (player.GetShootLeft() && !player.GetShootRight() && player.GetShootUp()) //shoot left-up    for some reason this some how doesn't work.
        {
            direction = -1;
            diagonal = true;
        }
        else if (!player.GetShootLeft() && player.GetShootRight() && player.GetShootUp()) //shoot right-up
        {
            direction = 1;
            diagonal = true;
        }
        else
        {
            direction = player.GetDirection();
        }
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
            if ((!player.GetShootLeft() && !player.GetShootRight() && !player.GetShootUp()) || (player.GetShootLeft() && player.GetShootRight() && !player.GetShootUp())) // if nothing is pressed or just left and right are pressed
            {
                direction = player.GetDirection();
                diagonal = false;
            }
            else if (player.GetShootLeft() && !player.GetShootRight() && !player.GetShootUp()) // shoot left
            {
                diagonal = false;
                direction = -1;
            }
            else if (!player.GetShootLeft() && player.GetShootRight() && !player.GetShootUp()) //shoot right
            {
                diagonal = false;
                direction = 1;
            }
            else if ((!player.GetShootLeft() && !player.GetShootRight() && player.GetShootUp()) || (player.GetShootLeft() && player.GetShootRight() && !player.GetShootUp())) //shoot up
            {
                diagonal = false;
                direction = 0;
            }
            else if (player.GetShootLeft() && !player.GetShootRight() && player.GetShootUp()) //shoot left-up    for some reason this some how doesn't work.
            {
                direction = -1;
                diagonal = true;
            }
            else if (!player.GetShootLeft() && player.GetShootRight() && player.GetShootUp()) //shoot right-up
            {
                direction = 1;
                diagonal = true;
            }
            else
            {
                diagonal = false;
                direction = player.GetDirection();
            }

            float distance = 14.0f;
            transform.rotation = Quaternion.identity;
            if (diagonal)
            {
                if (Physics.Raycast(player.transform.position, new Vector3(direction, 1.0f, 0.0f), out hit, 14.0f) && hit.collider.tag == "Obstacle")
                {
                    if (hit.distance < distance)
                    {
                        distance = hit.distance;
                    }
                }

                
                float xDist = direction * (distance / Mathf.Sqrt(2));
                
                transform.position = new Vector3(player.transform.position.x + (direction * xDist / 2) + (direction * 0.5f), player.transform.position.y + 20, player.transform.position.z);
                transform.localScale = new Vector3(distance, 0.75f, 1.5f);

                if (direction > 0)
                {
                    transform.Rotate(0.0f, 0.0f, 45.0f);
                }
                else
                {
                    transform.Rotate(0.0f, 0.0f, -45.0f);
                }


            }
            else
            {
                if (direction != 0)
                {
                    if (Physics.Raycast(player.transform.position, new Vector3(direction, 0.0f, 0.0f), out hit, 14.0f) && hit.collider.tag == "Obstacle")
                    {
                        if (hit.distance < distance)
                        {
                            distance = hit.distance;
                        }
                    }
                    transform.position = new Vector3(player.transform.position.x + (direction * distance / 2) + (direction * 0.5f), player.transform.position.y, player.transform.position.z);
                    transform.localScale = new Vector3(distance, 0.75f, 1.5f);
                }
                else
                {
                    if (Physics.Raycast(player.transform.position, Vector3.up, out hit, 14.0f) && hit.collider.tag == "Obstacle")
                    {
                        if (hit.distance < distance)
                        {
                            distance = hit.distance;
                        }
                    }
                    transform.position = new Vector3(player.transform.position.x, player.transform.position.y + (distance / 2), player.transform.position.z);

                    transform.localScale = new Vector3(distance, 0.75f, 1.5f);
                }
                
            }


            
           


            //GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
            //float distance = 13.5f;

            //if (obstacles.Length > 0)
            //{
            //    for (int i = 0; i < obstacles.Length; i++)
            //    {
            //        if (direction > 0 && (obstacles[i].transform.position.x  - (player.transform.position.x + 0.5f)) < distance)
            //        {
            //            distance = obstacles[i].transform.position.x - player.transform.position.x;
            //        }
            //        else if (direction < 0 && (player.transform.position.x - 0.5f - obstacles[i].transform.position.x) > distance)
            //        {
            //            distance = player.transform.position.x - obstacles[i].transform.position.x;
            //        }
            //    }
            //}

            transform.localScale = new Vector3(distance, 0.75f, 1.5f);
            transform.position = new Vector3(player.transform.position.x + (direction * distance / 2) + (direction* 0.5f), player.transform.position.y, player.transform.position.z);
        }
        
    }
}
