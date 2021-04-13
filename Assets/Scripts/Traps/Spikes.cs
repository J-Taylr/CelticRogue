﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{

    public int damage;

    void OnTriggerEnter2D(Collider2D player) {
        if (player.tag == "Player")
        {
            player.GetComponent<PlayerManager>().TakeDamage(damage);
            player.GetComponent<PlayerMovement>().PlayerJump();
        }
    }

}
