using UnityEngine;
using System.Collections;

public class UpgradeBtn : MonoBehaviour {

	public int index;
	public TextMesh upgradeButtonText;

	/// <summary>
	/// Raises the mouse down event.
	/// When clicked, loads level
	/// </summary>
	void OnMouseDown(){
		if (ShopManager.upgrades [index] < 4) {
			if (ShopManager.bank > ShopManager.cost [ShopManager.upgrades [index]]) {
				ShopManager.bank -= ShopManager.cost [ShopManager.upgrades [index]];
				ShopManager.upgrades [index]++;
			}
		}
		else {
			upgradeButtonText.text = "MAX";
		}
	}
}
