using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public Transform healthBar;
	public float BAR_WIDTH_MAX;

	public float MAX_HEALTH;
	private float health;

	// Use this for initialization
	void Start () {
		reset();
	}
	
	// Update is called once per frame
	void Update () {
		healthBar.localScale = new Vector3 ((BAR_WIDTH_MAX * (health/MAX_HEALTH)), 0.2f, 1);
	}

	public void takeDamage(float damage) {
		health-=damage;
		if (isDead()) { health = 0.0f; }
        print((BAR_WIDTH_MAX * (health / MAX_HEALTH)));
	}
	
	public bool isDead() {
		return (health <= 0.0f);
	}

	public void reset() {
		health = MAX_HEALTH;
	}

	public float getHealth() {
		return health;
	}
}
