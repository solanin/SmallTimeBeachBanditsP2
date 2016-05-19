using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	private UI ui;
	public static bool isPaused;

	// Use this for initialization
	void Start () {

		ui = GameObject.Find("UI").GetComponent<UI>();
		isPaused = false;

		// Set up UI
		ui.ChangeWeapon(0);
		ui.UpdateAmo(-1.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateUI(int currentWeapon, float[] bullets)	{
		ui.ChangeWeapon(currentWeapon);
		ui.UpdateAmo(bullets[currentWeapon]);
	}

	public void UpdateAmo(float amt) 	{
		ui.UpdateAmo(amt);
	}

	public void EndGame(){
		isPaused = true;
        
		GameObject.Find("GO").transform.position = new Vector3(GameObject.Find("Player").transform.position.x, 2.5f, -5);
		GameObject.Find("GO").GetComponent<MeshRenderer>().enabled = true;
		GameObject.Find("GOText").GetComponent<MeshRenderer>().enabled = true;
		GameObject.Find("btnReplay").GetComponent<SpriteRenderer>().enabled = true;
		GameObject.Find("btnReplay").GetComponent<BoxCollider2D>().enabled = true;
		GameObject.Find("btnShop").GetComponent<SpriteRenderer>().enabled = true;
		GameObject.Find("btnShop").GetComponent<BoxCollider2D>().enabled = true;
		GameObject.Find("btnBack").GetComponent<SpriteRenderer>().enabled = true;
		GameObject.Find("btnBack").GetComponent<BoxCollider2D>().enabled = true;
		GameObject.Find("bank").GetComponent<MeshRenderer>().enabled = true;

		int score = ui.getScore();

		// Save to bank
		ShopManager.AddBank(score);
		GameObject.Find("bank").GetComponent<TextMesh>().text = "Earned " + score + " Gold (Bank: " + ShopManager.bank + ")";

		// Check if you got highscore
		bool gotHighScore = false;
		int atLoc = HighScoreManager.highscore.Length;
		for (int i = 0; i<HighScoreManager.highscore.Length; i++) {
			if (score > HighScoreManager.highscore[i] && !gotHighScore) {
				gotHighScore = true;
				atLoc = i;
			}
		}

		// did you reach a highscore?
		if (gotHighScore) {
			HighScoreManager.insertHighScore(atLoc, score);
		}
	}
}
