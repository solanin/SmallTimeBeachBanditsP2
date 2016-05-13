﻿using UnityEngine;
using System.Collections;

public class ShopManager : MonoBehaviour {

	//Score
	public static int AMT_LABELS = 8;
	public static int bank = 1000;
	static double[,] dmg = new double[8,6];
	public static int[] upgrades = new int[8];
	public static int[] cost = new int[5];
	public TextMesh[] dmgLabels = new TextMesh[AMT_LABELS];

	// Use this for initialization
	void Start () {
		// Cost
		cost [0] = 100;
		cost [1] = 200;
		cost [2] = 300;
		cost [3] = 400;
		cost [4] = 500;

		// Health
		dmg[0,0] = 100;
		dmg[0,1] = 150;
		dmg[0,2] = 200;
		dmg[0,3] = 250;
		dmg[0,4] = 300;
		dmg[0,5] = 350;

		// Revolver
		dmg[1,0] = 0.30;
		dmg[1,1] = 0.37;
		dmg[1,2] = 0.50;
		dmg[1,3] = 0.44;
		dmg[1,4] = 0.55;
		dmg[1,5] = 0.60;

		// M Gun
		dmg[2,0] = 0.30;
		dmg[2,1] = 0.37;
		dmg[2,2] = 0.50;
		dmg[2,3] = 0.44;
		dmg[2,4] = 0.55;
		dmg[2,5] = 0.60;

		// Fireball
		dmg[3,0] = 0.10;
		dmg[3,1] = 0.12;
		dmg[3,2] = 0.14;
		dmg[3,3] = 0.16;
		dmg[3,4] = 0.18;
		dmg[3,5] = 0.20;

		// Laser
		dmg[4,0] = 0.10;
		dmg[4,1] = 0.12;
		dmg[4,2] = 0.14;
		dmg[4,3] = 0.16;
		dmg[4,4] = 0.18;
		dmg[4,5] = 0.20;

		// Snipe
		dmg[5,0] = 1.50;
		dmg[5,1] = 1.90;
		dmg[5,2] = 2.20;
		dmg[5,3] = 2.50;
		dmg[5,4] = 2.70;
		dmg[5,5] = 3.00;

		// Grenade
		dmg[6,0] = 1.00;
		dmg[6,1] = 1.30;
		dmg[6,2] = 1.50;
		dmg[6,3] = 1.65;
		dmg[6,4] = 1.90;
		dmg[6,5] = 2.00;

		// Shot Gun
		dmg[7,0] = 0.70;
		dmg[7,1] = 0.80;
		dmg[7,2] = 1.90;
		dmg[7,3] = 1.00;
		dmg[7,4] = 1.20;
		dmg[7,5] = 1.40;

		// Show
		LoadUpgrades();
		ShowUpgrades();
	}

	void Update () {
		GameObject.Find ("bank").GetComponent<TextMesh> ().text = "Gold: " + bank;
		ShowUpgrades ();
	}

	public void LoadUpgrades () {
		for (int i=0; i < AMT_LABELS; i++) {
			upgrades[i] = PlayerPrefs.GetInt ("Upgrade " + i);
		}
	}
	
	public void SaveUpgrades () {
		for (int i=0; i < AMT_LABELS; i++) {
			PlayerPrefs.SetInt ("Upgrade " + i, upgrades[i]);
		}
	}

	public void ShowUpgrades () {
		for (int i = 0; i < AMT_LABELS; i++) {
			dmgLabels [i].text = "" + dmg [i, upgrades[i]];
		}
	}
}