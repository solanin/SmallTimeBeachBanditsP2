using UnityEngine;
using System.Collections;

public class ShotGun : MonoBehaviour {

    public float speed = 15.0f;
    private Player player;
    private Vector2 shootDirection;
    Vector2 angleComparison = new Vector2(1.0f, 0.0f);
    float angle;
    Vector2[] directions;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        shootDirection.x = player.ShootX;
        shootDirection.y = player.ShootY;
        shootDirection.Normalize();

        if (shootDirection.magnitude == 0.0f)
        {
            shootDirection.x = speed * (float)player.Direction;
        }

        Destroy(this.gameObject, 3.0f);
        angle = Vector2.Angle(angleComparison, shootDirection);
        if (shootDirection.y < 0) angle *= -1.0f;
        angle -= 20.0f;

        directions = new Vector2[5];
        for (int i = 0; i < transform.childCount; i++)
        {
            directions[i] = Vector2FromAngle(angle + (10.0f * i));
            directions[i] *= speed;
        }

    }

    // Update is called once per frame
    void Update () {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childBullet = transform.GetChild(i);
            childBullet.transform.Translate(directions[i] * Time.deltaTime);
        }
    }

    Vector2 Vector2FromAngle(float a)
    {
        a *= Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(a), Mathf.Sin(a));
    }
}
