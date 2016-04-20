using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	private UI ui;

	// Use this for initialization
	void Start () {

		ui = GameObject.Find("UI").GetComponent<UI>();

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
		GameObject.Find("GO").transform.position = new Vector3(transform.position.x, 2.5f, -5);
		GameObject.Find("GO").GetComponent<MeshRenderer>().enabled = true;
		GameObject.Find("GOText").GetComponent<MeshRenderer>().enabled = true;
		GameObject.Find("btnReplay").GetComponent<SpriteRenderer>().enabled = true;
		GameObject.Find("btnReplay").GetComponent<BoxCollider2D>().enabled = true;
		GameObject.Find("btnBack").GetComponent<SpriteRenderer>().enabled = true;
		GameObject.Find("btnBack").GetComponent<BoxCollider2D>().enabled = true;
		
		int score = ui.getScore();
		
		// Load current scores
		float[] highscore = new float[HighScoreManager.AMT_SAVED];
		for (int i = 0; i<highscore.Length; i++) {
			highscore[i] = PlayerPrefs.GetFloat ("Score " + i);
		}
		
		// Check
		bool gotHighScore = false;
		int atLoc = highscore.Length;
		for (int i = 0; i<highscore.Length; i++) {
			if (score > highscore[i] && !gotHighScore) {
				gotHighScore = true;
				atLoc = i;
			}
		}
		
		// did you reach a highscore?
		if (gotHighScore && (atLoc<highscore.Length)) {
			HighScoreManager.insertHighScore(highscore, atLoc, score);
			
			//Save New
			for (int i = 0; i<HighScoreManager.AMT_SAVED; i++) {
				PlayerPrefs.SetFloat("Score " + i, highscore[i]);
			}
			PlayerPrefs.Save ();
			Debug.Log("SAVE");
		}
	}
}
