using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    GameObject[] enemies = new GameObject[5];
    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < 5; i++)
        {
            GenerateEnemy(i);
        }
        
	}

    public void GenerateEnemy(int index)
    {
        int type = Random.Range(0, 2);
        int pos = Random.Range(0, 2);
        GameObject enemy;
        string name = "";
        switch (type)
        {
            case 0:
                name = "Prefabs\\EnemySlow";
                break;
            case 1:
                name = "Prefabs\\EnemyFast";
                break;
        }
        enemy = (GameObject)Resources.Load(name);
        switch (pos)
        {
            case 0:
                enemy.transform.position = player.transform.position + new Vector3(-15.0f, 0.0f, 0.0f);
                break;
            case 1:
                enemy.transform.position = player.transform.position + new Vector3(15.0f, 0.0f, 0.0f);
                break;
        }
        enemy.GetComponent<Enemy>().index = index;
        Instantiate(enemy);
        enemies[index] = enemy;
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < enemies.Length; i++)
        {
            print(enemies[i]);
            Enemy enemy = enemies[i].GetComponent<Enemy>();
            if (enemy.alive == true)
            {
                GenerateEnemy(i);
            }
        }
	}
}
