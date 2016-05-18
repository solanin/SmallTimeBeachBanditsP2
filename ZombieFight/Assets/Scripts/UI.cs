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

		if (totalKill == 50 && !Achieivement.CheckAchieved(5))
        {
			Achieivement.Achieve (5, achievement, achieveMessageCountDown);
        }
		if (totalKill == 100 && !Achieivement.CheckAchieved(6))
        {
			Achieivement.Achieve (6, achievement, achieveMessageCountDown);
        }
		if (totalKill == 500 && !Achieivement.CheckAchieved(7))
        {
			Achieivement.Achieve (7, achievement, achieveMessageCountDown);
        }
		if (totalKill == 1000 && !Achieivement.CheckAchieved(8))
        {
			Achieivement.Achieve (8, achievement, achieveMessageCountDown);

        }
		if (totalKill == 5000 && !Achieivement.CheckAchieved(9))
        {
			Achieivement.Achieve (9, achievement, achieveMessageCountDown);
        }
		if (currentKill >= 500 && !Achieivement.CheckAchieved(10))
		{			
			Achieivement.Achieve (10, achievement, achieveMessageCountDown);
        }

		if (currentKill >= 1000 && !Achieivement.CheckAchieved(11))
        {
			Achieivement.Achieve (11, achievement, achieveMessageCountDown);
        }

        PlayerPrefs.SetInt("TOTAL KILL", totalKill);
    }

    public void UpdateScore() {
		score.text = ""+scoreAmt;
	}
}
