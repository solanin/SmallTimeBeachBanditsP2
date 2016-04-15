using UnityEngine;
using System.Collections;

public class Drops : MonoBehaviour {
    GameObject player;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
		Destroy(this.gameObject, 10.0f)
	}
	
	// Update is called once per frame
	void Update () {
	    if (transform.position.y < -10.0f || Mathf.Abs(transform.position.x - player.transform.position.x) > 20.0f)
        {
            Destroy(this.gameObject);
        }
	}
}
