using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour
{

    public float speed = 1.0f;
    float xSpeed = 0.0f;
    float ySpeed = 0.0f;
    private Player player;
    public float life = 14; //active in seconds
    // Use this for initialization
    void Start()
    {
        float diagonalSpeed = speed / (Mathf.Sqrt(2));
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if ((!player.ShootLeft && !player.ShootRight && !player.ShootUp) || (player.ShootLeft && player.ShootRight && !player.ShootUp)) // if nothing is pressed or just left and right are pressed
        {
            xSpeed = speed * (float)player.Direction;
        }
        else if (player.ShootLeft && !player.ShootRight && !player.ShootUp) // shoot left
        {
            xSpeed -= speed;
        }
        else if (!player.ShootLeft && player.ShootRight && !player.ShootUp) //shoot right
        {
            xSpeed = speed;
        }
        else if ((!player.ShootLeft && !player.ShootRight && player.ShootUp) || (player.ShootLeft && player.ShootRight && !player.ShootUp)) //shoot up
        {
            ySpeed = speed;
        }
        else if (player.ShootLeft && !player.ShootRight && player.ShootUp) //shoot left-up    for some reason this some how doesn't work.
        {
            xSpeed -= diagonalSpeed;
            ySpeed = diagonalSpeed;
        }
        else if (!player.ShootLeft && player.ShootRight && player.ShootUp) //shoot right-up
        {
            xSpeed = diagonalSpeed;
            ySpeed = diagonalSpeed;
        }
        else
        {
            xSpeed = speed * (float)player.Direction;
        }
    }

    // Update is called once per frame
    void Update()
    {
        life -= Time.deltaTime;
        transform.position = new Vector3(transform.position.x + Time.deltaTime * xSpeed, transform.position.y + Time.deltaTime * ySpeed, transform.position.z);
        if (life <= 0 || transform.position.x - player.transform.position.x > 14.0 || transform.position.x - player.transform.position.x < -14.0 || transform.position.y - player.transform.position.y > 14.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
