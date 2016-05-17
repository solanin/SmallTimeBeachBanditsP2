using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

	public TextMesh amo;
	public TextMesh score;
    public TextMesh achievement;
	public GameObject weapon;
	private int scoreAmt = 0;
    private int totalKill = 0;
    private int currentKill = 0;
    private float achieveMessageCountDown = 0.0f;

    public int getScore()
    {
        return scoreAmt;
    }
    public void AddToScore() {
		scoreAmt++;
	}
	public void AddToScore(int num) {
		scoreAmt += num;
	}

	// Use this for initialization
	void Start()
	{
		scoreAmt = 0;
		ChangeWeapon(0);
		UpdateAmo(-1.0f);
		UpdateScore();

        if (PlayerPrefs.HasKey("TOTAL KILL"))
        {
            totalKill = PlayerPrefs.GetInt("TOTAL KILL");
        }
	}

    void Update()
    {
        if (achieveMessageCountDown > 0.0f)
        {
            achieveMessageCountDown -= Time.deltaTime;
            if (achieveMessageCountDown <= 0.0f)
            {
                achievement.text = "";
            }
        }
    }

	public void ChangeWeapon(int current) {

		string name = "";
		switch (current) {
			case 0:
				name = "gun";
				break;
			case 1:
				name = "machine";
				break;
			case 2:
				name = "fire";
				break;
			case 3:
				name = "laser";
				break;
            case 4:
                name = "sniper";
                break;
            case 5:
                name = "shotgun";
                break;
            case 6:
                name = "grenade";
                break;

        }
		weapon.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/"+name);
	}

	public void UpdateAmo(float a) {
		if (a < 0) {
			amo.text = "∞";
		} else {
			amo.text = ""+a;
		}
	}
    
    public void AddToKill()
    {
        totalKill++;
        currentKill++;

        if (totalKill == 50 && !PlayerPrefs.HasKey("KILL 50"))
        {
            PlayerPrefs.SetString("KILL 50", "ACHIEVED");
            ShowAchievement("Killed 50 in total, +5 gold");
            PlayerPrefs.SetInt("Bank", PlayerPrefs.GetInt("Bank") + 5);
        }
        if (totalKill == 100 && !PlayerPrefs.HasKey("KILL 100"))
        {
            PlayerPrefs.SetString("KILL 100", "ACHIEVED");
            ShowAchievement("Killed 100 in total, +10 gold");
            PlayerPrefs.SetInt("Bank", PlayerPrefs.GetInt("Bank") + 10);
        }
        if (totalKill == 500 && !PlayerPrefs.HasKey("KILL 500"))
        {
            PlayerPrefs.SetString("KILL 500", "ACHIEVED");
            ShowAchievement("Killed 500 in total, +50 gold");
            PlayerPrefs.SetInt("Bank", PlayerPrefs.GetInt("Bank") + 50);
        }
        if (totalKill == 1000 && !PlayerPrefs.HasKey("KILL 1000"))
        {
            PlayerPrefs.SetString("KILL 1000", "ACHIEVED");
            ShowAchievement("Killed 1000 in total, +100 gold");
            PlayerPrefs.SetInt("Bank", PlayerPrefs.GetInt("Bank") + 100);
        }
        if (totalKill == 5000 && !PlayerPrefs.HasKey("KILL 5000"))
        {
            PlayerPrefs.SetString("KILL 5000", "ACHIEVED");
            ShowAchievement("Killed 5000 in total, +500 gold");
            PlayerPrefs.SetInt("Bank", PlayerPrefs.GetInt("Bank") + 500);
        }
        if (currentKill >= 500 && !PlayerPrefs.HasKey("KILL 500 IN ONE"))
        {
            PlayerPrefs.SetString("KILL 500 IN ONE", "ACHIEVED");
            ShowAchievement("Killed 500 in one game, +200 gold");
            PlayerPrefs.SetInt("Bank", PlayerPrefs.GetInt("Bank") + 200);
        }

        if (currentKill >= 1000 && !PlayerPrefs.HasKey("KILL 1000 IN ONE"))
        {
            PlayerPrefs.SetString("KILL 1000 IN ONE", "ACHIEVED");
            ShowAchievement("Killed 1000 in one game, +500 gold");
            PlayerPrefs.SetInt("Bank", PlayerPrefs.GetInt("Bank") + 200);
        }

        PlayerPrefs.SetInt("TOTAL KILL", totalKill);
    }

    public void UpdateScore() {
		score.text = ""+scoreAmt;
	}

    public void ShowAchievement(string message)
    {
        achievement.text = "Achievement: " + message;
        achieveMessageCountDown = 1.5f;
    }
}
