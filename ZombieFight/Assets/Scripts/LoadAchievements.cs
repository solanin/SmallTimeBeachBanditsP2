using UnityEngine;
using System.Collections;

public class LoadAchievements : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("KILL 50"))
            GameObject.Find("star01").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/star_filled");
        if (PlayerPrefs.HasKey("KILL 100"))
            GameObject.Find("star02").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/star_filled");
        if (PlayerPrefs.HasKey("KILL 500"))
            GameObject.Find("star03").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/star_filled");
        if (PlayerPrefs.HasKey("KILL 1000"))
            GameObject.Find("star04").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/star_filled");
        if (PlayerPrefs.HasKey("KILL 5000"))
            GameObject.Find("star05").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/star_filled");
        if (PlayerPrefs.HasKey("KILL 500 IN ONE"))
            GameObject.Find("star06").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/star_filled");
        if (PlayerPrefs.HasKey("KILL 1000 IN ONE"))
            GameObject.Find("star07").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/star_filled");
        if (PlayerPrefs.HasKey("UNLOCK ALL"))
            GameObject.Find("star08").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/star_filled");
        if (PlayerPrefs.HasKey("WEAPON ONE MAX"))
            GameObject.Find("star09").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/star_filled");
        if (PlayerPrefs.HasKey("WEAPON ALL MAX"))
            GameObject.Find("star10").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/star_filled");
        if (PlayerPrefs.HasKey("HEALTH ONCE"))
            GameObject.Find("star11").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/star_filled");
        if (PlayerPrefs.HasKey("HEALTH MAX"))
            GameObject.Find("star12").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/star_filled");
    }
}
