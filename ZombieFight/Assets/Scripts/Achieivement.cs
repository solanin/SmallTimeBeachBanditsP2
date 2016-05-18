using UnityEngine;
using System.Collections;

public class Achieivement : MonoBehaviour {

	static void ShowAchievement(string message, TextMesh achievement, ref float achieveMessageCountDown)
	{
		achievement.text = "Achievement: " + message;
		achieveMessageCountDown = 1.5f;
	}

	static void GetAchievement(string key, int reward, string message, TextMesh achievement, ref float achieveMessageCountDown) {
		PlayerPrefs.SetString(key, "ACHIEVED");
		ShowAchievement(message+", +"+reward+"gold", achievement, ref achieveMessageCountDown);
		ShopManager.AddBank(reward);
	}

	public static bool CheckAchieved (int i) {
		string gotIt = "";
		switch (i) {
		case 0:
			gotIt = PlayerPrefs.GetString("HEALTH ONCE");
			break;
		case 1:
			gotIt = PlayerPrefs.GetString("HEALTH MAX");
			break;
		case 2:
			gotIt = PlayerPrefs.GetString("WEAPON ONE MAX");
			break;
		case 3:
			gotIt = PlayerPrefs.GetString("WEAPON ALL MAX");
			break;
		case 4:
			gotIt = PlayerPrefs.GetString("UNLOCK ALL");
			break;
		case 5:
			gotIt = PlayerPrefs.GetString("KILL 50");
			break;
		case 6:
			gotIt = PlayerPrefs.GetString("KILL 100");
			break;
		case 7:
			gotIt = PlayerPrefs.GetString ("KILL 500");
			break;
		case 8:
			gotIt = PlayerPrefs.GetString("KILL 1000");
			break;
		case 9:
			gotIt = PlayerPrefs.GetString("KILL 5000");
			break;
		case 10:
			gotIt = PlayerPrefs.GetString("KILL 500 IN ONE");
			break;
		case 11:
			gotIt = PlayerPrefs.GetString("KILL 1000 IN ONE");
			break;
		}

		return gotIt == "ACHIEVED";
	}

	public static void Achieve(int i, TextMesh achievement, ref float achieveMessageCountDown) {
		switch (i) {
		case 0:
			GetAchievement ("HEALTH ONCE", 100, "Upgraded health once", achievement, ref achieveMessageCountDown);
			break;
		case 1:
			GetAchievement ("HEALTH MAX", 200, "Maxed out health", achievement, ref achieveMessageCountDown);
			break;
		case 2:
			GetAchievement ("WEAPON ONE MAX", 100, "Maxed out one weapon", achievement, ref achieveMessageCountDown);
			break;
		case 3:
			GetAchievement ("WEAPON ALL MAX", 200, "Maxed out all weapons", achievement, ref achieveMessageCountDown);
			break;
		case 4:
			GetAchievement ("UNLOCK ALL", 100, "Unlocked all weapons", achievement, ref achieveMessageCountDown);
			break;
		case 5:
			GetAchievement ("KILL 50", 5, "Killed 50 in total", achievement, ref achieveMessageCountDown);
			break;
		case 6:
			GetAchievement ("KILL 100", 10, "Killed 100 in total", achievement, ref achieveMessageCountDown);
			break;
		case 7:
			GetAchievement ("KILL 500", 50, "Killed 500 in total", achievement, ref achieveMessageCountDown);
			break;
		case 8:
			GetAchievement ("KILL 1000", 100, "Killed 1000 in total", achievement, ref achieveMessageCountDown);
			break;
		case 9:
			GetAchievement ("KILL 5000", 500, "Killed 5000 in total", achievement, ref achieveMessageCountDown);
			break;
		case 10:
			GetAchievement ("KILL 500 IN ONE", 200, "Killed 500 in one game", achievement, ref achieveMessageCountDown);
			break;
		case 11:
			GetAchievement ("KILL 1000 IN ONE", 500, "Killed 1000 in one game", achievement, ref achieveMessageCountDown);
			break;
		}
	}

	public static void resetAchieved () {
		PlayerPrefs.SetInt("TOTAL KILL", 0);
		PlayerPrefs.SetString("HEALTH ONCE", "UNACHIEVED");
		PlayerPrefs.SetString("HEALTH MAX", "UNACHIEVED");
		PlayerPrefs.SetString("WEAPON ONE MAX", "UNACHIEVED");
		PlayerPrefs.SetString("WEAPON ALL MAX", "UNACHIEVED");
		PlayerPrefs.SetString("UNLOCK ALL", "UNACHIEVED");
		PlayerPrefs.SetString("KILL 50", "UNACHIEVED");
		PlayerPrefs.SetString("KILL 100", "UNACHIEVED");
		PlayerPrefs.SetString ("KILL 500", "UNACHIEVED");
		PlayerPrefs.SetString("KILL 1000", "UNACHIEVED");
		PlayerPrefs.SetString("KILL 5000", "UNACHIEVED");
		PlayerPrefs.SetString("KILL 500 IN ONE", "UNACHIEVED");
		PlayerPrefs.SetString("KILL 1000 IN ONE", "UNACHIEVED");
	}
}
