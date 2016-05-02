using UnityEngine;
using System.Collections;

public class ShopManager : MonoBehaviour {

	//Score
	public static int AMT_LABELS = 5;
	public TextMesh[] dmgLabels = new TextMesh[AMT_LABELS];
	
	// Use this for initialization
	void Start () {
		float[] val = new float[AMT_LABELS];
		for (int i=0; i < AMT_LABELS; i++) {
			val[i] = 0;
			dmgLabels [i].text = "Dmg: "+i;
		}
		LoadValues (val);
		UpdateLabels (val);
	}
	
	void LoadScores (float[] val) {
		for (int i=0; i < AMT_LABELS; i++) {
			val [i] = PlayerPrefs.GetFloat ("Score " + i);
		}
	}
	
	void UpdateLabels (float[] val) {
		for (int i=0; i < AMT_LABELS; i++) {
			dmgLabels [i].text = (i+1)+": "+val [i];
		}
	}
}
