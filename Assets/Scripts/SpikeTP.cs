using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTP : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D player) {
        if (player.tag == "Player")
        {
            print("Player is dead");
        }
    }
}
