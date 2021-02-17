using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLazer : MonoBehaviour
{
    public int damage;
    public BoxCollider2D col;
    public float startDelay, lazerTime, standardDelay;

    void Start() {
        Invoke("EnableLazer", startDelay); ;
    }

    void EnableLazer()
    {
        col.enabled = true;
        Invoke("DisableLazer", lazerTime);
    }
    void DisableLazer() {
        col.enabled = false;
        Invoke("EnableLazer",standardDelay);
    }

    void OnTriggerEnter2D(Collider2D check) {
        if (check.tag == "Player")
        {
           check.GetComponent<PlayerManager>().currentHealth -= damage;
        }
    }
}
