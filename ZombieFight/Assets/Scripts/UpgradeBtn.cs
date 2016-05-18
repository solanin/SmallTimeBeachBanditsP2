using UnityEngine;
using System.Collections;

public class UpgradeBtn : MonoBehaviour {

	public int index;
	public TextMesh upgradeButtonText;

	// Use this for initialization
	void Update () {
		if (ShopManager.upgrades [index] == (ShopManager.AMT_UPGRADES - 1)) {
			upgradeButtonText.text = "MAX";
		} else {
			upgradeButtonText.text = "$" + ShopManager.cost [ShopManager.upgrades [index]];
		}

		if ((index == 5 && !ShopManager.sniper) || (index == 6 && !ShopManager.grenade) || (index == 7 && !ShopManager.shotgun)) {
			upgradeButtonText.text = "$" + ShopManager.cost [index];
		} 
	}

	/// <summary>
	/// Raises the mouse down event.
	/// When clicked, loads level
	/// </summary>
	void OnMouseDown() {
		if ((index < 5) || (index == 5 && ShopManager.sniper) || (index == 6 && ShopManager.grenade) || (index == 7 && ShopManager.shotgun)) {
			if (ShopManager.upgrades [index] < (ShopManager.AMT_UPGRADES - 1)) {
				if (ShopManager.bank >= ShopManager.cost [ShopManager.upgrades [index]]) {
					ShopManager.bank -= ShopManager.cost [ShopManager.upgrades [index]];
					ShopManager.upgrades [index]++;
					Debug.Log ("Bought " + index + " upgrade " + ShopManager.upgrades [index] + " for " + ShopManager.cost [ShopManager.upgrades [index] - 1]);
					ShopManager.CheckAchievement (index, ShopManager.upgrades [index]);
				}
				if (ShopManager.upgrades [index] == (ShopManager.AMT_UPGRADES - 1)) {
					upgradeButtonText.text = "MAX";
				} else {
					upgradeButtonText.text = "$" + ShopManager.cost [ShopManager.upgrades [index]];
				}
				ShopManager.SaveUpgrade (index, ShopManager.upgrades [index]);
			}
		} else {
			if (index == 5 && ShopManager.bank >= ShopManager.cost [5]) {
				ShopManager.bank -= ShopManager.cost [5];
				ShopManager.sniper = true;
				Debug.Log ("Unlocked Sniper for " + ShopManager.cost [5]);
				ShopManager.SaveUnlock (1);
			} else if (index == 6 && ShopManager.bank >= ShopManager.cost [6]) {
				ShopManager.bank -= ShopManager.cost [6];
				ShopManager.grenade = true;
				Debug.Log ("Unlocked Grenade for " + ShopManager.cost [6]);	
				ShopManager.SaveUnlock (2);
			} else if (index == 7 && ShopManager.bank >= ShopManager.cost [7]) {
				ShopManager.bank -= ShopManager.cost [7];
				ShopManager.shotgun = true;
				Debug.Log ("Unlocked Shot Gun for " + ShopManager.cost [7]);
				ShopManager.SaveUnlock (3);				
			}
		}
	}
}
