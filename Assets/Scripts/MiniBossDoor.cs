using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossDoor : MonoBehaviour
{

    public GameManager gameManager;
    public string doorType;
    // Start is called before the first frame update
    void Start()
    {
        GameObject test = GameObject.FindGameObjectWithTag("GameMan");
        gameManager = test.GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag =="Player")
        {
            gameManager.MiniBoss(doorType);
            
        }
    }
}
