using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    //starthere//starthere//starthere//starthere//starthere//starthere//starthere//starthere//starthere//starthere//starthere//starthere//starthere


    public AudioManager audioManager;
    public GameObject player, dashDoor, spawnDoor;
    public Transform spawnPoint,dashSpawn,spawnerSpawn;
    public bool dashDead, spawnDead;

    public enum TerrainZone {FOREST, SWAMP, CAVE};
    public TerrainZone terrain;



    private void Start()
    {
        terrain = TerrainZone.FOREST;
        // Cursor.visible = false;
        print("remember to disable cursor in THIS script");

        player = GameObject.FindGameObjectWithTag("Player");

    }


    private void Update()
    {
        CheckPos();
    }

    public void CheckPos()
    {
        if (player.transform.position.y <= -80)
        {
            terrain = TerrainZone.CAVE;
        }


    }

    public void TerrainForest()
    {
        terrain = TerrainZone.FOREST;
    }

    public void TerrainSwamp()
    {
        terrain = TerrainZone.SWAMP;
    }

    public void RespawnPlayer()
    {
        player.transform.position = spawnPoint.position;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MiniBoss(string boss) {
        SceneManager.LoadScene(boss);
        Invoke("Restart", 0.01f);
    }

    public void Restart()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i].GetComponent<SpawnPointManager>().FindGameManager();
        }

        if (dashDead)
        {
            player.transform.position = dashSpawn.position;
        }
        
    }
}
