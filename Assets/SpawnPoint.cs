using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    PlayerManager playerManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerManager = collision.gameObject.GetComponent<PlayerManager>();

            if (playerManager.isInteracting)
            {
                SetSpawn();
            }

        }
    }



    public void SetSpawn() 
    { 
        
    }

}
