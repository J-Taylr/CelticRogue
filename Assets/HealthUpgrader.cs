using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUpgrader : MonoBehaviour
{
    PlayerManager playerManager;

    public Button button;
    public Animator anim;
    public Image spriteHolder;
    public Image fadeHolder;
    public Sprite[] Sprites;


    public int currentstage = 0;

    public bool isHealth;


    private void Start()
    {
        button = GetComponentInChildren<Button>();
        anim = GetComponent<Animator>();
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    private void Update()
    {
        if (currentstage >= 10)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void CheckPoints()
    {
        if (playerManager.UpgradePoints > 0 && currentstage < 10)
        {
            if (isHealth)
            {
                anim.SetTrigger("HealthUpgrade");
            }
            else
            {
                anim.SetTrigger("DamageUpgrade");
            }

        }

    }

    public void ChangeSprite()
    {

        UpgradePlayer();
        spriteHolder.sprite = Sprites[currentstage];
        fadeHolder.sprite = Sprites[currentstage];

        playerManager.UpgradePoints--;
        currentstage++;

    }


    public void UpgradePlayer()
    {
        if (isHealth)
        {
            playerManager.maxHealth += 1;
            playerManager.currentHealth = playerManager.maxHealth;
        }
        else
        {
            playerManager.strikeDamage += 1;
        }
    }
}
