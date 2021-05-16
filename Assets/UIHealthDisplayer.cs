using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthDisplayer : MonoBehaviour
{

    public Sprite[] sprites;
    public Image imageHolder;
    public Image maxHolder;

    private PlayerManager playerManager;

    private void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    private void Update()
    {
        CheckHealth();
        CheckMaxHealth();
    }

    public void CheckHealth()
    {
        switch (playerManager.currentHealth)
        {
            case 0:
                imageHolder.sprite = sprites[0];
                break;
            case 1:
                imageHolder.sprite = sprites[1];
                break;
            case 2:
                imageHolder.sprite = sprites[2];
                break;
            case 3:
                imageHolder.sprite = sprites[3];
                break;
            case 4:
                imageHolder.sprite = sprites[4];
                break;
            case 5:
                imageHolder.sprite = sprites[5];
                break;
            case 6:
                imageHolder.sprite = sprites[6];
                break;
            case 7:
                imageHolder.sprite = sprites[7];
                break;
            case 8:
                imageHolder.sprite = sprites[8];
                break;
            case 9:
                imageHolder.sprite = sprites[9];
                break;
            case 10:
                imageHolder.sprite = sprites[10];
                break;
            case 11:
                imageHolder.sprite = sprites[11];
                break;
            case 12:
                imageHolder.sprite = sprites[12];
                break;
            case 13:
                imageHolder.sprite = sprites[13];
                break;



        }
    }
    public void CheckMaxHealth()
    {
        switch (playerManager.maxHealth)
        {
            case 0:
                maxHolder.sprite = sprites[0];
                break;
            case 1:
                maxHolder.sprite = sprites[1];
                break;
            case 2:
                maxHolder.sprite = sprites[2];
                break;
            case 3:
                maxHolder.sprite = sprites[3];
                break;
            case 4:
                maxHolder.sprite = sprites[4];
                break;
            case 5:
                maxHolder.sprite = sprites[5];
                break;
            case 6:
                maxHolder.sprite = sprites[6];
                break;
            case 7:
                maxHolder.sprite = sprites[7];
                break;
            case 8:
                maxHolder.sprite = sprites[8];
                break;
            case 9:
                maxHolder.sprite = sprites[9];
                break;
            case 10:
                maxHolder.sprite = sprites[10];
                break;
            case 11:
                maxHolder.sprite = sprites[11];
                break;
            case 12:
                maxHolder.sprite = sprites[12];
                break;
            case 13:
                maxHolder.sprite = sprites[13];
                break;



        }
    }
}
