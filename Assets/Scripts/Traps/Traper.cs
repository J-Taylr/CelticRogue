using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traper : MonoBehaviour
{
    public GameObject door;

    void OnTriggerEnter2D(Collider2D player) {
        if (player.tag == "Player")
        {
            door.SetActive(true);
            Destroy(gameObject);
        }
    }
}
