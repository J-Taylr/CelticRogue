using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatUpgrader : MonoBehaviour
{
    [Header("Components")]
    PlayerManager playerManager;

    [Header("UI Elements")]
    public Slider healthSlider;
    public Slider damageSlider;
    public Slider criticalSlider;
    public Slider speedSlider;

    public TextMeshProUGUI upgradePointsText;


    private void Awake()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    private void Update()
    {
        upgradePointsText.text = playerManager.UpgradePoints.ToString();
    }

    public void UpgradeHealth()
    {
        if (playerManager.UpgradePoints > 0)
        {
            playerManager.UpgradePoints--;
            playerManager.maxHealth += 1;
            playerManager.currentHealth = playerManager.maxHealth;
            healthSlider.value += 1;

        }
    }

    public void UpgradeDamage()
    {
        if (playerManager.UpgradePoints > 0)
        {
            playerManager.UpgradePoints--;
            playerManager.strikeDamage += 1;

            damageSlider.value += 1;
        }
    }
}
