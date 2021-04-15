using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPickup : MonoBehaviour
{
    public PickupParticleController particleController;
   
  
    public bool activated = false;





    private void Awake()
    {
        particleController = GetComponent<PickupParticleController>();

        

       
        
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
        if (!activated)
        {

            player.UpgradePoints++;

            activated = true;
            particleController.StopParticle();
        }
                
    }


    /*

    public void UpgradeHealth(PlayerManager player)
    {
        int healthBuff = Random.Range(1, 5);
        player.maxHealth += healthBuff;
        player.currentHealth = player.maxHealth;

        print("Max health up " + healthBuff + "!");


        GameObject go = Instantiate(pickupText, player.transform.position, Quaternion.identity);
        go.GetComponent<UpgradeText>().ChangeText("Health", healthBuff);
    }

    public void UpgradeDamage(PlayerManager player)
    {
        int damageBuff = Random.Range(1, 5);
        player.strikeDamage += damageBuff;

        print("Damage up" + damageBuff + "!");

        GameObject go = Instantiate(pickupText, player.transform.position, Quaternion.identity);
        go.GetComponent<UpgradeText>().ChangeText("Damage", damageBuff);

    }


    public void UpgradeCritical(PlayerManager player)
    {
        int critBuff = Random.Range(1, 5);

        if (critBuff < 50)
        {
            player.critChance += critBuff;
            

            GameObject go = Instantiate(pickupText, player.transform.position, Quaternion.identity);
            go.GetComponent<UpgradeText>().ChangeText("Crit Chance", critBuff);
        }
        else
        {
            print("crit maxxed out!");
            

        }



    }


    public void UpgradeSpeed(PlayerManager player) 
    {
        float speedBuff = Random.Range(0.5f, 2);
        player.moveSpeed += speedBuff;



        GameObject go = Instantiate(pickupText, player.transform.position, Quaternion.identity);
        go.GetComponent<UpgradeText>().ChangeText("speed", speedBuff);


    }*/

}
