using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPickup : MonoBehaviour
{
    public enum upgradeType {HEALTH,DAMAGE,CRITICAL,SPEED}
    public upgradeType upgrade;



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           PlayerManager player = collision.GetComponent<PlayerManager>();

            UpgradePlayer(player);
        }
    }

    public void UpgradePlayer(PlayerManager player)
    {
        if (player.isInteracting)
        {

            switch (upgrade)
            {
                case upgradeType.HEALTH: //player Health

                    break;
                case upgradeType.DAMAGE: //player Damage

                    break;
                case upgradeType.CRITICAL: //chance to critical

                    break;
                case upgradeType.SPEED: //player speed

                    break;
            }
        }
    }

    
}
