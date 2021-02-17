using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    public int damage;
    public int boulHealth;

    void OnCollisionEnter2D(Collision2D check) {

        if (boulHealth<0)
        {
            Destroy(gameObject);
        }
        if (check.gameObject.tag =="Player")
        {
            check.gameObject.GetComponent<PlayerManager>().currentHealth -= damage;
            check.gameObject.GetComponent<PlayerManager>().CheckHealth();

        }
        boulHealth--;
    }
}
