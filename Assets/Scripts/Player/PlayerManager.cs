using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [Header("Components")]
    public Animator animator;
    public Animator healthUI;
    public Animator damageUI;
    public GameObject mapUI;
    public GameObject pauseMenuUI;

    [Header("Core")]
    public int UpgradePoints = 0;

    public int maxHealth = 10;
    public int currentHealth;
    public float moveSpeed = 40;
    public int strikeDamage = 5;
    public int critChance = 1;
    public int damageResist = 0;

    public bool isImmune = false;
    public bool isInteracting = false;
    
    public UpgradeController upgrades;

    [Header("Progression")]
    public bool doubleUnlock;
    public bool wallJumpUnlock;
    public bool dashUnlock;

    public bool invincible;

    [Header("Menus")]
    public bool pauseMenuActive = false;
    public bool statMenuActive = false;
    public bool mapActive = false;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        
        mapUI.SetActive(false);
        
    }


    private void Start()
    {
        
        currentHealth = maxHealth;
        invincible = false;
    }

    private void Update()
    {
        
    }

    public void TakeDamage()
    {

        isImmune = true;
        animator.SetTrigger("TakeDamage");
        print("damage animation");
        currentHealth--;
        CheckHealth();


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


    public void ToggleStatMenu()
    {
        statMenuActive = !statMenuActive;

        if (statMenuActive)
        {
            healthUI.SetTrigger("UiON");
            damageUI.SetTrigger("UiON");
        }
        else
        {
            healthUI.SetTrigger("UiOff");
            damageUI.SetTrigger("UiOff");

        }

    }

    public void ToggleMap()
    {
        mapActive = !mapActive;

        if (mapActive)
        {
            mapUI.SetActive(true);
        }
        else
        {
            mapUI.SetActive(false);
        }

    }


    public void TogglePauseMenu()
    {
        pauseMenuActive = !pauseMenuActive;

        if (pauseMenuActive)
        {
            Cursor.visible = true;
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Cursor.visible = false;
            pauseMenuUI.SetActive(true);
            Time.timeScale = 1;
        }

    }
}
