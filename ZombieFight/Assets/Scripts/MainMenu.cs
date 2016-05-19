using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {

		// Make the Shop Manager Exist
		ShopManager sm = new ShopManager();
		sm.Setup ();
		HighScoreManager.LoadScores();

		// RESET ALL DATA
		//ShopManager.resetUpgrades();
		//Achieivement.resetAchieved();
		//HighScoreManager.resetScores();
	}
}
