using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPickup : MonoBehaviour
{
    public enum upgradeType { HEALTH, DAMAGE, CRITICAL, SPEED }
    public upgradeType upgrade;

    private void Awake()
    {
        int Randm = Random.Range(0, 3);

        this.upgrade = (upgradeType)Randm;
        
    }

    
    


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
                    UpgradeHealth(player);
                    
                    break;
                case upgradeType.DAMAGE: //player Damage
                    UpgradeDamage(player);
                    
                    break;
                case upgradeType.CRITICAL: //chance to critical
                    UpgradeCritical(player);
                    
                    break;
                case upgradeType.SPEED: //player speed
                    UpgradeSpeed(player);
                  
                    break;
            }

            Destroy(gameObject);
        }
    }


    public void UpgradeHealth(PlayerManager player)
    {
        int healthBuff = Random.Range(1, 5);
        player.maxHealth += healthBuff;
        player.currentHealth = player.maxHealth;

        print("Max health up " + healthBuff + "!");

        player.isInteracting = false;
    }

    public void UpgradeDamage(PlayerManager player)
    {
        int damageBuff = Random.Range(1, 5);
        player.strikeDamage += damageBuff;

        print("Damage up" + damageBuff + "!");

        player.isInteracting = false;
    }


    public void UpgradeCritical(PlayerManager player)
    {
        int critBuff = Random.Range(1, 5);

        if (critBuff < 50)
        {
            player.critChance += critBuff;
            print("crit chance up " + critBuff + "!");
            player.isInteracting = false;

        }
        else
        {
            print("crit maxxed out!");
            player.isInteracting = false;

        }



    }


    public void UpgradeSpeed(PlayerManager player) 
    {
        float speedBuff = Random.Range(0.5f, 2);
        player.moveSpeed += speedBuff;


        player.isInteracting = false;


    
    }

}
