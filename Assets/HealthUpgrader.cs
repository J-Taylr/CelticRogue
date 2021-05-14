using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUpgrader : MonoBehaviour
{
    public Image spriteHolder;
    public Image fadeHolder;
    public Sprite[] Sprites;
   

    public int spriteChoice = 0;
   
    
    public void ChangeSprite()
    {
        spriteHolder.sprite = Sprites[spriteChoice];
        fadeHolder.sprite = Sprites[spriteChoice];

        
        spriteChoice++;
    }
}
