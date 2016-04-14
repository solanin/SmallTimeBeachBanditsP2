using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    GameObject[] enemies = new GameObject[5];
    GameObject player;
    int totalAllowedEnemies = 5;
    int killedEnemies = 0;

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
        int type = Random.Range(0, 4);
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
            case 2:
                name = "Prefabs\\EnemyNorm";
                break;
            case 3:
                name = "Prefabs\\EnemyJump";
                break;
        }
        enemy = (GameObject)Resources.Load(name);
        switch (pos)
        {
            case 0:
                enemy.transform.position = new Vector2(player.transform.position.x - 15.0f,-0.5f);
                break;
            case 1:
                enemy.transform.position = new Vector2(player.transform.position.x + 15.0f,-0.5f);
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
            //print(enemies[i]);
            Enemy enemy = enemies[i].GetComponent<Enemy>();
            if(enemy == null)
            {
                GenerateEnemy(i);
                killedEnemies++;
            }
            else if (enemy.alive == true)
            {
                GenerateEnemy(i);
                killedEnemies++;
            }
        }
        if (killedEnemies >= 15)
        {
            totalAllowedEnemies++;
            killedEnemies = 0;
            GameObject[] temp = new GameObject[totalAllowedEnemies];
            for (int i = 0; i < enemies.Length; i++)
            {
                temp[i] = enemies[i];
            }
            enemies = new GameObject[totalAllowedEnemies];
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = temp[i];
            }
        }
	}
}
