using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [Header("Basics")]
    public int maxHealth = 3;
    public int currentHealth;

    [Tooltip("This number = % Chance of enemy dropping an upgrade point on death")]  
    static int spawnChance = 80;

    [Header("Components")]
    public Slider healthSlider;
    public GameObject statDrop;

    private Skulls skulls; 
    private bool isSkull;

   
    private int poiDam;
    int dothit;
    float dotDelay;

    private void Awake()
    {
        healthSlider = gameObject.GetComponentInChildren<Slider>();
        CheckEnemyType();
    }

    public void CheckEnemyType()
    {
        skulls = GetComponent<Skulls>();

        if (skulls != null)
        {
            isSkull = true;
        }
        else
        {
            isSkull = false;
        }
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
            if (isSkull)
            {
                skulls.removeSkulls();
            }
            Die();
        }
    }
    public void Die()
    {
        if (!isSkull)
        {
            SpawnChance();
        }

        Destroy(gameObject);
    }


    public void SpawnChance()
    {
        int rndm = Random.Range(1, 100);

        if (rndm <= spawnChance)
        {
            Instantiate(statDrop, transform.position, Quaternion.identity);
        }
        else
        {
            return;
        }

    }

}

