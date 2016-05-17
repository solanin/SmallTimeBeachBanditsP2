using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    GameObject[] enemies = new GameObject[5];
    GameObject player;
    int totalAllowedEnemies = 5;
    int killedEnemies = 0;
    TextMesh score;
    bool changeArray;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        score = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<UI>().score;
        Physics.IgnoreLayerCollision(9, 13, true);
        Physics.IgnoreLayerCollision(11, 13, true);
        Physics.IgnoreLayerCollision(12, 13, true);
        Physics.IgnoreLayerCollision(14, 13, true);
        Physics.IgnoreLayerCollision(9, 14, true);
        Physics.IgnoreLayerCollision(9, 9, true);
        Physics.IgnoreLayerCollision(11, 14, true);
        Physics.IgnoreLayerCollision(12, 14, true);
        for (int i = 0; i < 5; i++)
        {
            GenerateEnemy(i);
        }
	}

    public void GenerateEnemy(int index)
    {
        int type = Random.Range(0, 20);
        if (score.text == "25" || 
            score.text == "50" || 
            score.text == "75" || 
            score.text == "100" || 
            score.text == "150" || 
            score.text == "200" || 
            score.text == "250" || 
            score.text == "300" || 
            score.text == "400" || 
            score.text == "500")
        {
            type = 20;
            score.text += ".";
        }
        int pos = Random.Range(0, 2);
        GameObject enemy;
        string name = "";
        switch (type)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
                name = "Prefabs\\EnemySlow";
                break;
            case 5:
            case 6:
            case 7:
            case 8:
                name = "Prefabs\\EnemyFast";
                break;
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
                name = "Prefabs\\EnemyNorm";
                break;
            case 15:
            case 16:
            case 17:
            case 14:
                name = "Prefabs\\EnemyJump";
                break;
            case 18:
            case 19:
                name = "Prefabs\\EnemyObstacle";
                break;
            case 20:
                name = "Prefabs\\EnemyBoss";
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
        if (type == 20)
        {
            enemy.transform.position += new Vector3(0.0f, 3.0f);
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
            if (enemies[i] == null)
            {
                GenerateEnemy(i);
                killedEnemies++;
            }
            Enemy enemy = enemies[i].GetComponent<Enemy>();
            if (enemy.alive == true)
            {
                GenerateEnemy(i);
                killedEnemies++;
            }
        }

        switch (score.text)
        {
            case "0":
                totalAllowedEnemies = 5;
                changeArray = true;
                break;
            case "25":
                totalAllowedEnemies = 6;
                changeArray = true;
                break;
            case "50":
                totalAllowedEnemies = 7;
                changeArray = true;
                break;
            case "75":
                totalAllowedEnemies = 8;
                changeArray = true;
                break;
            case "100":
                totalAllowedEnemies = 10;
                changeArray = true;
                break;
            case "150":
                totalAllowedEnemies = 12;
                changeArray = true;
                break;
            case "200":
                totalAllowedEnemies = 14;
                changeArray = true;
                break;
            case "250":
                totalAllowedEnemies = 16;
                changeArray = true;
                break;
            case "300":
                totalAllowedEnemies = 18;
                changeArray = true;
                break;
            case "400":
                totalAllowedEnemies = 20;
                changeArray = true;
                break;
            case "500":
                totalAllowedEnemies = 25;
                changeArray = true;
                break;
        }

        if (changeArray)
        {
            GameObject[] temo = new GameObject[totalAllowedEnemies];
            int make = totalAllowedEnemies;
            int old = enemies.Length;
            int enem = make - old;
            for (int i = 0; i < enemies.Length; i++)
            {
                temo[i] = enemies[i];
            }
            enemies = new GameObject[totalAllowedEnemies];
            for (int i = 0; i < temo.Length; i++)
            {
                enemies[i] = temo[i];
            }
            for (int i = 0; i < enem; i++)
            {
                GenerateEnemy(i + enem);
            }
            changeArray = false;
        }
    }
}
