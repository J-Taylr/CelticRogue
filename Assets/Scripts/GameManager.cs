using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



    public GameObject player;
    public Transform spawn;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawn = GameObject.FindGameObjectWithTag("Spawn").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RespawnPlayer()
    {
        player.transform.position = spawn.position;
    }
}
