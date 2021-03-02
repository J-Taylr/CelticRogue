using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLazer : MonoBehaviour
{
    public int damage;
    public BoxCollider2D col;
    public SpriteRenderer im;
    public float startDelay, lazerTime, standardDelay;
    public Color death, safe;

    void Start() {
        Invoke("EnableLazer", startDelay); ;
    }

    void EnableLazer()
    {
        col.enabled = true;
        im.color = death;
        Invoke("DisableLazer", lazerTime);
    }
    void DisableLazer() {
        col.enabled = false;
        im.color = safe;
        Invoke("EnableLazer",standardDelay);
    }

    void OnTriggerEnter2D(Collider2D check) {
        if (check.tag == "Player")
        {
           check.GetComponent<PlayerManager>().TakeDamage(damage,gameObject);
        }
    }
}
