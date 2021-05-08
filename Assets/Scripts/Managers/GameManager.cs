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
    }
    //starthere//starthere//starthere//starthere//starthere//starthere//starthere//starthere//starthere//starthere//starthere//starthere//starthere


    public AudioManager audioManager;
    public GameObject player;
    public Transform spawnPoint;

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

    public void RespawnPlayer()
    {
        player.transform.position = spawnPoint.position;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

   

}
