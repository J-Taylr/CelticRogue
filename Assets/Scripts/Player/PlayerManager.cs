using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Core")]
    public int maxHealth = 10;
    public int currentHealth;
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

}
