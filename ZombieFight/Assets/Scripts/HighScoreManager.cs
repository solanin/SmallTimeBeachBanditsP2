using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System;

public class HighScoreManager : MonoBehaviour {

	// Score
	public static int AMT_SAVED = 10;
	public TextMesh[] highscoreLabels = new TextMesh[AMT_SAVED];
	public static int[] highscore = new int[AMT_SAVED];

	// Use this for initialization
	void Start () {
		LoadScores ();
		UpdateLabels ();
	}

	public static void LoadScores () {
		for (int i=0; i < AMT_SAVED; i++) {
			highscore [i] = PlayerPrefs.GetInt ("Score " + i);
		}
	}

	public static void SaveScores () {
		for (int i=0; i < AMT_SAVED; i++) {
			PlayerPrefs.SetInt ("Score " + i, highscore[i]);
		}
		PlayerPrefs.Save ();
		Debug.Log("SAVE");
	}

	void UpdateLabels () {
		for (int i=0; i < AMT_SAVED; i++) {
			highscoreLabels [i].text = (i + 1) + ": " + highscore [i];
		}
	}

	public static void insertHighScore(int insertAt, int score) {
		Debug.Log("INSERT " + score + " AT " + (insertAt+1));

		for (int i = highscore.Length-1; i > insertAt; i--) {
			if ((i-1) > 0 ) highscore[i] = highscore[i - 1];
			Debug.Log("EDITING " + i);
		}
		highscore[insertAt] = score;

		SaveScores ();
	}

	public static void resetScores() {
		for (int i = 0; i < AMT_SAVED; i++) {
			highscore[i] = 0;
		}
		SaveScores ();
	}
}
