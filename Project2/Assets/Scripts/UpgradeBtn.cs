using UnityEngine;
using System.Collections;

public class UpgradeBtn : MonoBehaviour {

	public int index;
	public TextMesh upgradeButtonText;

	// Use this for initialization
	void Update () {
		if (ShopManager.upgrades [index] == (ShopManager.AMT_UPGRADES-1)) {
			upgradeButtonText.text = "MAX";
		}
	}

	/// <summary>
	/// Raises the mouse down event.
	/// When clicked, loads level
	/// </summary>
	void OnMouseDown() {
		if (ShopManager.upgrades [index] < (ShopManager.AMT_UPGRADES-1)) {
			if (ShopManager.bank >= ShopManager.cost [ShopManager.upgrades [index]]) {
				ShopManager.bank -= ShopManager.cost [ShopManager.upgrades [index]];
				ShopManager.upgrades [index]++;
				Debug.Log ("Bought " + index + " upgrade " + ShopManager.upgrades [index] + " for " + ShopManager.cost [ShopManager.upgrades [index]-1]);
                ShopManager.CheckAchievement(index, ShopManager.upgrades[index]);
			}
			if (ShopManager.upgrades [index] == (ShopManager.AMT_UPGRADES-1)) {
				upgradeButtonText.text = "MAX";
			}
			ShopManager.SaveUpgrade (index, ShopManager.upgrades [index]);
		}
	}
}
