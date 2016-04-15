using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

	public TextMesh amo;
	public TextMesh score;
	public GameObject weapon;
	private int scoreAmt = 0;

	public void AddToScore() {
		scoreAmt++;
	}
	public void AddToScore(int num) {
		scoreAmt += num;
	}

	// Use this for initialization
	void Start()
	{
		scoreAmt = 0;
		ChangeWeapon(0);
		UpdateAmo(0f);
		UpdateScore();
	}

	public void ChangeWeapon(int current) {

		string name = "";
		switch (current) {
			case 0:
				name = "gun";
				break;
			case 1:
				name = "machine";
				break;
			case 2:
				name = "laser";
				break;
			case 3:
				name = "fire";
				break;
		}
		weapon.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/"+name);
	}

	public void UpdateAmo(float a) {
		if (a < 0) {
			amo.text = "∞";
		} else {
			amo.text = ""+a;
		}
	}

	public void UpdateScore() {
		score.text = ""+scoreAmt;
	}
}
