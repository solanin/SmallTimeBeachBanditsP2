﻿using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed = 15.0f;
    float xSpeed = 0.0f;
    float ySpeed = 0.0f;
    private Player player;
    private Vector2 shootDirection;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        shootDirection.x = player.ShootX;
        shootDirection.y = player.ShootY;
        shootDirection.Normalize();
        xSpeed = shootDirection.x * speed;
        ySpeed = shootDirection.y * speed;

        if (xSpeed == 0.0f && ySpeed == 0.0f)
        {
            xSpeed = speed * (float)player.Direction;
        }
        Destroy(this.gameObject, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + Time.deltaTime * xSpeed, transform.position.y + Time.deltaTime * ySpeed, transform.position.z);
        if (transform.position.x - player.transform.position.x > 14.0 || transform.position.x - player.transform.position.x < -14.0 || transform.position.y - player.transform.position.y > 14.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
