using UnityEngine;
using System.Collections;

public class LoadAchievements : MonoBehaviour {

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 12; i++) {
			if (Achieivement.CheckAchieved (i))
				GameObject.Find ("star"+i).GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/star_filled");
		}
    }
}
