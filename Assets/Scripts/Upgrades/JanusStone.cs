using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanusStone : MonoBehaviour
{
    public enum Upgradable {DOUBLE,WALL,DASH}
    public Upgradable upgrade;

    



    
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
                case Upgradable.DOUBLE:
                    player.doubleUnlock = true;
                    
                    break;

                case Upgradable.WALL:
                    player.wallJumpUnlock = true;
                    break;
                case Upgradable.DASH:
                    player.dashUnlock = true;
                    break;
            }
        }
    }

}
