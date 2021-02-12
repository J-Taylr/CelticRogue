using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [Header("Basics")]
    public int maxHealth = 10;
    public int currentHealth;

    [Header("Components")]
    public Slider healthSlider;

    private void Awake()
    {
        healthSlider = gameObject.GetComponentInChildren<Slider>();
    }


    void Start()
    {
        healthSlider.maxValue = maxHealth;
        
        currentHealth = maxHealth;
    }

    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            print(gameObject.name + " is dead");
            gameObject.SetActive(false);
        }

    }
}
