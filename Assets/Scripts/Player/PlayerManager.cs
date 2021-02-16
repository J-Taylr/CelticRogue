using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Core")]
    public int maxHealth = 10;
    public int currentHealth;
    public int moveSpeed = 40;
    public int strikeDamage = 5;
    public int critChance = 1;


    public bool isInteracting = false;

    [Header("Progression")]
    public bool doubleUnlock;
    public bool wallJumpUnlock;
    public bool dashUnlock;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            GameManager.Instance.RespawnPlayer();
        }
    }


    public bool RollCritical()
    {
        int crit = Random.Range(1, 100);

        if (crit <= critChance)
        {
            print(crit + "CRITICAL HIT");
            return true;
        }
        else
        {
            print(crit + "normal hit");
            return false;
        }


    }
}
