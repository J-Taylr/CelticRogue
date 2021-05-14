using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUpgrader : MonoBehaviour
{
    PlayerManager playerManager;

    public Image spriteHolder;
    public Image fadeHolder;
    public Sprite[] Sprites;
   

    public int spriteChoice = 0;

    public bool isHealth;


    private void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    public void ChangeSprite()
    {
        spriteHolder.sprite = Sprites[spriteChoice];
        fadeHolder.sprite = Sprites[spriteChoice];

        
        spriteChoice++;
    }


    public void UpgradePlayer()
    {
        if (isHealth)
        {
            playerManager.maxHealth += 1;
        }
        else
        {
            playerManager.strikeDamage += 1;
        }
    }
}
