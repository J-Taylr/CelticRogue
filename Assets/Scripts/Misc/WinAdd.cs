using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAdd : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D check) {
        if (check.tag == "Player")
        {
            check.GetComponent<WinCheck>().points++;
            check.GetComponent<WinCheck>().Check();
        }
    }
}
