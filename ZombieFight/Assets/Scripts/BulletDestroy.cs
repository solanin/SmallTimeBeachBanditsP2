using UnityEngine;
using System.Collections;

public class BulletDestroy : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet" || col.gameObject.tag == "MBullet" || col.gameObject.tag == "SnipeBullet" || col.gameObject.tag == "ShotBullet")
        {
            Destroy(col.gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Fireball")
        {
            Destroy(col.gameObject);
        }
    }
}
