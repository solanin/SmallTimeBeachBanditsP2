using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System;

public class HighScoreManager : MonoBehaviour {

	//Score
	public static int AMT_SAVED = 10;
	public TextMesh[] highscoreLabels = new TextMesh[AMT_SAVED];

	// Use this for initialization
	void Start () {
		float[] highscore = new float[AMT_SAVED];
		for (int i=0; i < AMT_SAVED; i++) {
			highscore[i] = 0;
			highscoreLabels [i].text = (i+1)+": "+0;
		}
		LoadScores (highscore);
		UpdateLabels (highscore);
	}

	void LoadScores (float[] highscore) {
		for (int i=0; i < AMT_SAVED; i++) {
			highscore [i] = PlayerPrefs.GetFloat ("Score " + i);
		}
	}

	void UpdateLabels (float[] highscore) {
		for (int i=0; i < AMT_SAVED; i++) {
			highscoreLabels [i].text = (i+1)+": "+highscore [i];
		}
	}

	public static void insertHighScore(float[] highscore, int insertAt, int score)
	{
		//Debug.Log("INSERT " + score + " AT " + (insertAt+1));
		for (int i = highscore.Length-1; i >= insertAt; i--)
		{
			highscore[i] = highscore[i - 1];
			//Debug.Log("EDITING " + i);
		}
		highscore[insertAt] = score;
	}
}
