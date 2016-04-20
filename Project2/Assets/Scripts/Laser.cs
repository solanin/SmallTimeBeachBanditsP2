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

        if ((!player.ShootLeft && !player.ShootRight && !player.ShootUp) || (player.ShootLeft && player.ShootRight && !player.ShootUp)) // if nothing is pressed or just left and right are pressed
        {
            direction = player.Direction;
        }
        else if (player.ShootLeft && !player.ShootRight && !player.ShootUp) // shoot left
        {
            direction = -1;
        }
        else if (!player.ShootLeft && player.ShootRight && !player.ShootUp) //shoot right
        {
            direction = 1;
        }
        else if ((!player.ShootLeft && !player.ShootRight && player.ShootUp) || (player.ShootLeft && player.ShootRight && !player.ShootUp)) //shoot up
        {
            direction = 0;
        }
        else if (player.ShootLeft && !player.ShootRight && player.ShootUp) //shoot left-up    for some reason this some how doesn't work.
        {
            direction = -1;
            diagonal = true;
        }
        else if (!player.ShootLeft && player.ShootRight && player.ShootUp) //shoot right-up
        {
            direction = 1;
            diagonal = true;
        }
        else
        {
            direction = player.Direction;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.Laser)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if ((!player.ShootLeft && !player.ShootRight && !player.ShootUp) || (player.ShootLeft && player.ShootRight && !player.ShootUp)) // if nothing is pressed or just left and right are pressed
            {
                direction = player.Direction;
                diagonal = false;
            }
            else if (player.ShootLeft && !player.ShootRight && !player.ShootUp) // shoot left
            {
                diagonal = false;
                direction = -1;
            }
            else if (!player.ShootLeft && player.ShootRight && !player.ShootUp) //shoot right
            {
                diagonal = false;
                direction = 1;
            }
            else if ((!player.ShootLeft && !player.ShootRight && player.ShootUp) || (player.ShootLeft && player.ShootRight && !player.ShootUp)) //shoot up
            {
                diagonal = false;
                direction = 0;
            }
            else if (player.ShootLeft && !player.ShootRight && player.ShootUp) //shoot left-up    for some reason this some how doesn't work.
            {
                direction = -1;
                diagonal = true;
            }
            else if (!player.ShootLeft && player.ShootRight && player.ShootUp) //shoot right-up
            {
				direction = 1;
                diagonal = true;
            }
            else
            {
                diagonal = false;
                direction = player.Direction;
            }

            float distance = 14.0f;
            transform.rotation = Quaternion.identity;
            if (diagonal)
            {
                distance = 19.0f;

                float xDist = direction * (distance / Mathf.Sqrt(2));
                transform.position = new Vector3(player.transform.position.x + (xDist / 2), player.transform.position.y + (Mathf.Abs(xDist) / 2), player.transform.position.z);
                transform.localScale = new Vector3(distance, 0.5f, 0.5f);

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
                    transform.position = new Vector3(player.transform.position.x + (direction * distance / 2) + (direction * 0.5f), player.transform.position.y, player.transform.position.z);
                    transform.localScale = new Vector3(distance, 0.5f, 0.5f);
                }
                else
                {
                    transform.position = new Vector3(player.transform.position.x, player.transform.position.y + (distance / 2), player.transform.position.z);

                    transform.localScale = new Vector3(0.5f, distance, 0.5f);
                }
            }
        }
        
    }
}
