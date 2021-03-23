using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    public int damage;
    public int rockHealth;

    public int destroyTimer;

    void OnCollisionEnter2D(Collision2D check) {

        if (rockHealth < 0)
        {
            Destroy(gameObject);
        }
        if (check.gameObject.tag =="Player")
        {
            check.gameObject.GetComponent<PlayerManager>().TakeDamage(damage,gameObject);

        }
        rockHealth--;
    }






}
