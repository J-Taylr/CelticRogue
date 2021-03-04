using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorPickup : MonoBehaviour
{
    public int pickupPower;

    void OnTriggerEnter2D(Collider2D hit) {
        if (hit.tag == "Player")
        {
            switch (pickupPower)
            {
                case 1:
                    hit.GetComponent<Upgardes>().ranged = true;
                    break;
                case 2:
                    hit.GetComponent<Upgardes>().dOTRanged = true;
                    break;
                case 3:
                    hit.GetComponent<Upgardes>().slam = true;
                    break;
                case 4:
                    hit.GetComponent<Upgardes>().dashEndExplosion = true;
                    break;
                case 5:
                    hit.GetComponent<Upgardes>().dashAttack = true;
                    break;
                case 6:
                    hit.GetComponent<Upgardes>().wallJumpExplosion = true;
                    break;
                case 7:
                    hit.GetComponent<Upgardes>().walljumpDamageBoost = true;
                    break;
                case 8:
                    hit.GetComponent<Upgardes>().wallJumpInvincibility = true;
                    break;
                case 9:
                    hit.GetComponent<Upgardes>().rage = true;
                    break;
                case 10:
                    hit.GetComponent<Upgardes>().dOTAttacker = true;
                    break;
                case 11:
                    hit.GetComponent<Upgardes>().tempInvincibility = true;
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }
    }
}
