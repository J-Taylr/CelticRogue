using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{

    public int damage;

    void OnTriggerEnter2D(Collider2D player) {
        if (player.tag == "Player")
        {
            if (!player.GetComponent<PlayerManager>().isImmune)
            {

            player.GetComponent<PlayerManager>().TakeDamage();
            player.GetComponent<PlayerMovement>().PlayerJump();
            }
            else
            {
                return;
            }
        }
    }

}
