using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    private Player player;
    private int direction = 0;
    bool diagonal = false;
    private Vector2 shootDirection;
    public float distance = 19.0f;
    Vector2 angleComparison = new Vector2(1.0f, 0.0f);

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        shootDirection.x = player.ShootX;
        shootDirection.y = player.ShootY;
        shootDirection.Normalize();
        shootDirection *= distance;

        if (shootDirection.x == 0.0f)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + (shootDirection.y / 2), player.transform.position.z);
            transform.localScale = new Vector3(0.5f, distance, 0.5f);
        }
        else if (shootDirection.y == 0.0f)
        {
            transform.position = new Vector3(player.transform.position.x + (shootDirection.x / 2), player.transform.position.y, player.transform.position.z);
            transform.localScale = new Vector3(distance, 0.5f, 0.5f);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x + (shootDirection.x / 2), player.transform.position.y + (shootDirection.y / 2), player.transform.position.z);
            transform.localScale = new Vector3(distance, 0.5f, 0.5f);
            float angle = Vector2.Angle(angleComparison, shootDirection);
            if (shootDirection.y < 0) angle *= -1.0f;
            transform.Rotate(0.0f, 0.0f, angle);
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
            transform.rotation = Quaternion.identity;
            shootDirection.x = player.ShootX;
            shootDirection.y = player.ShootY;
            shootDirection.Normalize();
            shootDirection *= distance;

            if (shootDirection.x == 0.0f)
            {
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y + (shootDirection.y / 2), player.transform.position.z);
                transform.localScale = new Vector3(0.5f, distance, 0.5f);
            }
            else if (shootDirection.y == 0.0f)
            {
                transform.position = new Vector3(player.transform.position.x + (shootDirection.x / 2), player.transform.position.y, player.transform.position.z);
                transform.localScale = new Vector3(distance, 0.5f, 0.5f);
            }
            else
            {
                transform.position = new Vector3(player.transform.position.x + (shootDirection.x / 2), player.transform.position.y + (shootDirection.y / 2), player.transform.position.z);
                transform.localScale = new Vector3(distance, 0.5f, 0.5f);
                float angle = Vector2.Angle(angleComparison, shootDirection);
                if (shootDirection.y < 0) angle *= -1.0f;
                transform.Rotate(0.0f, 0.0f, angle);
            }
        }

    }
}
