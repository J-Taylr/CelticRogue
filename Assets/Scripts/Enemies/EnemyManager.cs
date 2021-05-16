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
    static int spawnChance = 20;

    [Header("Components")]
   
    public GameObject statDrop;

    private Skulls skulls;
    private SkullsMiniBoss BossSkulls;
    public bool isSkull;
    public bool MSkull;

   
    private int poiDam;
    int dothit;
    float dotDelay;

    private void Awake()
    {
        
        CheckEnemyType();
    }

    public void CheckEnemyType()
    {
        skulls = GetComponent<Skulls>();
        BossSkulls = GetComponent<SkullsMiniBoss>();
       
    }


    void Start()
    {
        


        

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
    }

    public void CheckLife()
    {
        if (currentHealth <= 0)
        {
            if (isSkull)
            {
                skulls.removeSkulls();
            }
            if (MSkull)
            {
                BossSkulls.removeSkulls();
            }
            Die();
        }
    }
    public void Die()
    {
        Debug.Log("dieing");

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

