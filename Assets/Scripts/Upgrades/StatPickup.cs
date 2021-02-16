using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPickup : MonoBehaviour
{
    public enum upgradeType {HEALTH,DAMAGE,ATTACKSPD,NEGATE,CRITICAL,SPEED}
    public upgradeType upgrade;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           PlayerManager player = collision.GetComponent<PlayerManager>();

            

        }
    }



    
}
