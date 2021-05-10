using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorLock : MonoBehaviour
{
    public GameManager gameManager;
    public bool dashboss;
    // Start is called before the first frame update
    void Start()
    {
        if (gameManager.dashDead&&dashboss)
        {
            Destroy(gameObject);
        }
        else if (gameManager.spawnDead)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
