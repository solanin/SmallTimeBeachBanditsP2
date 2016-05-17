using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public Transform healthBar;
	public double BAR_WIDTH_MAX;

	public double MAX_HEALTH;
	private double health;

	// Use this for initialization
	void Start () {
		reset();
	}
	
	// Update is called once per frame
	void Update () {
		healthBar.localScale = new Vector3 ((float)(BAR_WIDTH_MAX * (health/MAX_HEALTH)), 0.2f, 1);
	}

	public void takeDamage(double damage) {
		health-=damage;
		if (isDead()) { health = 0.00; }
        //print((BAR_WIDTH_MAX * (health / MAX_HEALTH)));
	}
	
    public void gainHealth(float gain)
    {
        health += gain;
        if (health > MAX_HEALTH)
        {
            health = MAX_HEALTH;
        }
    }

	public bool isDead() {
		return (health <= 0.0f);
	}

	public void reset() {
		health = MAX_HEALTH;
	}

	public double getHealth() {
		return health;
	}
}
