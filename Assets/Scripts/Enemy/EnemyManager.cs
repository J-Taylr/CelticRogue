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


    private int poiDam;
    int dothit;
    float dotDelay;


    private void Awake()
    {
        healthSlider = gameObject.GetComponentInChildren<Slider>();
    }


    void Start()
    {
        healthSlider.maxValue = maxHealth;

        currentHealth = maxHealth;
    }

    public void DOT(int ticks,int dotDamage ,float tickDelay) {
        poiDam = dotDamage;
        dothit = ticks;
        dotDelay = tickDelay;
        TickDamage();
 
    }
    void TickDamage() {
        print("Hit");
        TakeDamage(poiDam);
        dothit--;
        if (dothit > 0)
        {
            Invoke("TickDamage", dotDelay);
        }
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
