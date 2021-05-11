using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    public GameManager gameManager;
    public int spawnPointNumber;

    void start() {
        FindGameManager();
    }

    public void FindGameManager() {
        GameObject test = GameObject.FindGameObjectWithTag("GameMan");
        gameManager = test.GetComponent<GameManager>();
        print("Yes");

        switch (spawnPointNumber)
        {
            case 1:
                gameManager.dashSpawn = gameObject.transform;
                break;

            case 2:
                gameManager.spawnerSpawn = gameObject.transform;
                break;
            default:
                break;
        }
    }
}
