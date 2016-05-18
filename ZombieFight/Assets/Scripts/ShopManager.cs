using UnityEngine;
using System.Collections;

public class ShopManager : MonoBehaviour
{

    //Score
    public static int AMT_LABELS = 8;
    public static int AMT_UPGRADES = 6;
    public static int bank = 0;
    public static double[,] dmg = new double[AMT_LABELS, AMT_UPGRADES];
    public static int[] upgrades = new int[AMT_LABELS];
	public static int[] cost = new int[AMT_LABELS];
	public TextMesh[] dmgLabels = new TextMesh[AMT_LABELS];
	public GameObject[] starLabels = new GameObject[AMT_LABELS];
    public static TextMesh achievement;
    private static float achieveMessageCountDown = 0.0f;
	public static bool grenade;
	public static bool shotgun;
	public static bool sniper;

	public void Setup() {
		// Health
		dmg[0, 0] = 100;
		dmg[0, 1] = 150;
		dmg[0, 2] = 200;
		dmg[0, 3] = 250;
		dmg[0, 4] = 300;
		dmg[0, 5] = 350;

		// Revolver
		dmg[1, 0] = 0.30;
		dmg[1, 1] = 0.37;
		dmg[1, 2] = 0.50;
		dmg[1, 3] = 0.44;
		dmg[1, 4] = 0.55;
		dmg[1, 5] = 0.60;

		// M Gun
		dmg[2, 0] = 0.30;
		dmg[2, 1] = 0.37;
		dmg[2, 2] = 0.50;
		dmg[2, 3] = 0.44;
		dmg[2, 4] = 0.55;
		dmg[2, 5] = 0.60;

		// Fireball
		dmg[3, 0] = 0.10;
		dmg[3, 1] = 0.12;
		dmg[3, 2] = 0.14;
		dmg[3, 3] = 0.16;
		dmg[3, 4] = 0.18;
		dmg[3, 5] = 0.20;

		// Laser
		dmg[4, 0] = 0.10;
		dmg[4, 1] = 0.12;
		dmg[4, 2] = 0.14;
		dmg[4, 3] = 0.16;
		dmg[4, 4] = 0.18;
		dmg[4, 5] = 0.20;

		// Snipe
		dmg[5, 0] = 1.50;
		dmg[5, 1] = 1.90;
		dmg[5, 2] = 2.20;
		dmg[5, 3] = 2.50;
		dmg[5, 4] = 2.70;
		dmg[5, 5] = 3.00;

		// Grenade
		dmg[6, 0] = 1.00;
		dmg[6, 1] = 1.30;
		dmg[6, 2] = 1.50;
		dmg[6, 3] = 1.65;
		dmg[6, 4] = 1.90;
		dmg[6, 5] = 2.00;

		// Shot Gun
		dmg[7, 0] = 0.70;
		dmg[7, 1] = 0.80;
		dmg[7, 2] = 1.90;
		dmg[7, 3] = 1.00;
		dmg[7, 4] = 1.20;
		dmg[7, 5] = 1.40;

		// Cost
		cost[0] = 100;
		cost[1] = 200;
		cost[2] = 300;
		cost[3] = 400;
		cost[4] = 500;
		cost[5] = 350;
		cost[6] = 550;
		cost[7] = 750;
	}


    // Use this for initialization
    void Start()
    {
		achievement = GameObject.Find ("achievement").GetComponent<TextMesh> ();

        // Show
        LoadUpgrades();
        ShowUpgrades();
    }

    void Update()
    {
        GameObject.Find("bank").GetComponent<TextMesh>().text = "Gold: " + bank;
        ShowUpgrades();

        if (achieveMessageCountDown > 0.0f)
        {
            achieveMessageCountDown -= Time.deltaTime;
            if (achieveMessageCountDown <= 0.0f)
            {
                achievement.text = "";
            }
        }
    }

    public void LoadUpgrades()
    {
        for (int i = 0; i < AMT_LABELS; i++)
        {
            upgrades[i] = PlayerPrefs.GetInt("Upgrade " + i);
        }
        bank = PlayerPrefs.GetInt("Bank");

		if (PlayerPrefs.GetInt ("Unlock 1") == 0) {
			sniper = false;
		} else { sniper = true; }

		if (PlayerPrefs.GetInt ("Unlock 2") == 0) {
			grenade = false;
		} else { grenade = true; }

		if (PlayerPrefs.GetInt ("Unlock 3") == 0) {
			shotgun = false;
		} else { shotgun = true; }
    }
	
	public static void SaveUnlock(int index)
	{
		PlayerPrefs.SetInt("Unlock " + index, 1);
		AddBank(0);
		PlayerPrefs.Save ();
		Debug.Log("SAVE");
	}

    public static void SaveUpgrade(int index, int amt)
    {
        PlayerPrefs.SetInt("Upgrade " + index, amt);
        AddBank(0);
		PlayerPrefs.Save ();
		Debug.Log("SAVE");
    }

    public static void AddBank(int amt)
    {
        bank += amt;
        PlayerPrefs.SetInt("Bank", bank);
    }

    public void ShowUpgrades()
    {
        for (int i = 0; i < AMT_LABELS; i++)
        {
			if ((i < 5) || (i == 5 && sniper) || (i == 6 && grenade) || (i == 7 && shotgun)) {
				if (i != 0) dmgLabels[i].text = "Dmg: " + dmg[i, upgrades[i]];
				else dmgLabels[i].text = "" + dmg[i, upgrades[i]];
				ShowStars (i, upgrades [i]);
			} else {
				dmgLabels[i].text = "Locked";
				ShowStars (i, -1);
			}
        }
    }

	public void ShowStars(int index, int star)
	{
		switch (star) {
		case 0:
			starLabels [index].transform.FindChild("star1").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_empty");
			starLabels [index].transform.FindChild("star2").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_empty");
			starLabels [index].transform.FindChild("star3").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_empty");
			starLabels [index].transform.FindChild("star4").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_empty");
			starLabels [index].transform.FindChild("star5").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_empty");
			break;
		case 1:
			starLabels [index].transform.FindChild("star1").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_filled");
			starLabels [index].transform.FindChild("star2").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_empty");
			starLabels [index].transform.FindChild("star3").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_empty");
			starLabels [index].transform.FindChild("star4").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_empty");
			starLabels [index].transform.FindChild("star5").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_empty");
			break;
		case 2:
			starLabels [index].transform.FindChild("star1").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_filled");
			starLabels [index].transform.FindChild("star2").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_filled");
			starLabels [index].transform.FindChild("star3").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_empty");
			starLabels [index].transform.FindChild("star4").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_empty");
			starLabels [index].transform.FindChild("star5").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_empty");
			break;
		case 3:
			starLabels [index].transform.FindChild("star1").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_filled");
			starLabels [index].transform.FindChild("star2").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_filled");
			starLabels [index].transform.FindChild("star3").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_filled");
			starLabels [index].transform.FindChild("star4").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_empty");
			starLabels [index].transform.FindChild("star5").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_empty");
			break;
		case 4:
			starLabels [index].transform.FindChild("star1").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_filled");
			starLabels [index].transform.FindChild("star2").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_filled");
			starLabels [index].transform.FindChild("star3").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_filled");
			starLabels [index].transform.FindChild("star4").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_filled");
			starLabels [index].transform.FindChild("star5").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_empty");
			break;
		case 5:
			starLabels [index].transform.FindChild("star1").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_filled");
			starLabels [index].transform.FindChild("star2").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_filled");
			starLabels [index].transform.FindChild("star3").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_filled");
			starLabels [index].transform.FindChild("star4").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_filled");
			starLabels [index].transform.FindChild("star5").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_filled");
			break;
		default:
			starLabels [index].transform.FindChild("star1").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_empty/none");
			starLabels [index].transform.FindChild("star2").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_empty/none");
			starLabels [index].transform.FindChild("star3").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_empty/none");
			starLabels [index].transform.FindChild("star4").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_empty/none");
			starLabels [index].transform.FindChild("star5").GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/star_empty/none");
			break;
		}
	}

    public static void CheckAchievement(int index, int amt)
    {
		if (index == 0 && amt == 1 && !Achieivement.CheckAchieved(0))
        {
			Achieivement.Achieve (0, achievement, ref achieveMessageCountDown);
        }

		if (index == 0 && amt == AMT_UPGRADES - 1 && !Achieivement.CheckAchieved(1))
        {
			Achieivement.Achieve (1, achievement, ref achieveMessageCountDown);
        }

		if (index > 0 && amt == AMT_UPGRADES - 1 && !Achieivement.CheckAchieved(2))
        {
			Achieivement.Achieve (2, achievement, ref achieveMessageCountDown);
        }

		if (index > 0 && amt == AMT_UPGRADES - 1 && !Achieivement.CheckAchieved(3))
        {
            bool allMax = true;

            for (int i = 0; i < AMT_LABELS; i++)
            {
                if (upgrades[i] != AMT_UPGRADES - 1)
                {
                    allMax = false;
                }
            }

            if (allMax)
            {
				Achieivement.Achieve (3, achievement, ref achieveMessageCountDown);
            }
        }

		if (sniper && shotgun && grenade && !Achieivement.CheckAchieved(4))
        {
			Achieivement.Achieve (4, achievement, ref achieveMessageCountDown);
        }

    }

	public static void resetUpgrades()
	{
		for (int i = 0; i < AMT_LABELS; i++)
		{
			PlayerPrefs.SetInt("Upgrade " + i, 0);
		}

		PlayerPrefs.SetInt("Bank", 0);

		PlayerPrefs.SetInt ("Unlock 1", 0);
		PlayerPrefs.SetInt ("Unlock 2", 0);
		PlayerPrefs.SetInt ("Unlock 3", 0);

		PlayerPrefs.Save ();
		Debug.Log("SAVE");
	}
}
