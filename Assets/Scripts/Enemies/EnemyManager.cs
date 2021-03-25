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
    public GameObject statDrop;

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


    private void Update()
    {
        CheckLife();
    }

    public void DOT(int ticks, int dotDamage, float tickDelay) {
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
    }

    public void CheckLife()
    {
        if (currentHealth <= 0)
        {
           
            Die();
        }
    }
    public void Die()
    {
        Instantiate(statDrop, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

