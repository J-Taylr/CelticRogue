using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUpgrades : MonoBehaviour
{
    public enum Upgrade {ranged, rangedDot, slam, dashAttack, dashExplosion, wallJumpExplosion,wallJumpBuff,wallJumpInvincible,rage, attackDot,tempInvincible}
    public Upgrade combatUpgrade;

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            PlayerManager playerManager = hit.gameObject.GetComponent<PlayerManager>();
            UpgradeController upgrader = hit.gameObject.GetComponent<UpgradeController>();
            UpgradePlayer(playerManager, upgrader);
        }


    }

    public void UpgradePlayer(PlayerManager playerManager, UpgradeController upgrader )
    {
        if (playerManager.isInteracting == true)
        {
            switch (combatUpgrade)
            {
                case Upgrade.ranged:
                    upgrader.ranged = true;
                    break;
                case Upgrade.rangedDot:
                    upgrader.dOTRanged = true;
                    break;
                case Upgrade.slam:
                    upgrader.slam = true;
                    break;
                case Upgrade.dashExplosion:
                    upgrader.dashEndExplosion = true;
                    break;
                case Upgrade.dashAttack:
                    upgrader.dashAttack = true;
                    break;
                case Upgrade.wallJumpExplosion:
                    upgrader.wallJumpExplosion = true;
                    break;
                case Upgrade.wallJumpBuff:
                    upgrader.walljumpDamageBoost = true;
                    break;
                case Upgrade.wallJumpInvincible:
                    upgrader.wallJumpInvincibility = true;
                    break;
                case Upgrade.rage:
                    upgrader.rage = true;
                    break;
                case Upgrade.attackDot:
                    upgrader.dOTAttacker = true;
                    break;
                case Upgrade.tempInvincible:
                    upgrader.tempInvincibility = true;
                    break;
                default:
                    break;
            }
            playerManager.isInteracting = false;
            Destroy(gameObject);
        }
    }


}
