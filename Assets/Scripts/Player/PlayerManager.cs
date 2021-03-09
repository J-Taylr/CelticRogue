using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Core")]
    public int maxHealth = 10;
    public int currentHealth;
    public float moveSpeed = 40;
    public int strikeDamage = 5;
    public int critChance = 1;
    public int damageResist = 0;


    public bool isInteracting = false;

    public UpgradeController upgrades;

    [Header("Progression")]
    public bool doubleUnlock;
    public bool wallJumpUnlock;
    public bool dashUnlock;

    public bool invincible;

    private void Start()
    {
        currentHealth = maxHealth;
        invincible = false;
    }

    public void TakeDamage(int damage,GameObject attacker)
    {
        if (invincible)
        {
            print("invicible");
        }
        else
        {
            currentHealth -= damage- damageResist;
            CheckHealth();
        }
        upgrades.DamageResponse(attacker);
    }

    public void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
            GameManager.Instance.RespawnPlayer();
        }
    }



    public bool RollCritical()
    {
        int crit = Random.Range(1, 100);

        if (crit <= critChance)
        {
            //print(crit + "CRITICAL HIT");
            return true;
        }
        else
        {
            //print(crit + "normal hit");
            return false;
        }


    }
}
