using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwampKiller : MonoBehaviour
{
    public int damage;
    public float timeBetweenDamage;
    public PlayerManager player;

    private void OnTriggerEnter2D(Collider2D check)
    {
        if (check.tag == "Player")
        {
            player = check.GetComponent<PlayerManager>();
            InvokeRepeating("SwampDamage",0f,timeBetweenDamage);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = null;
            CancelInvoke();
        }
    }

    public void SwampDamage() {
        player.TakeDamage();
    }
}
