using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public BoxCollider2D plat;
    public SpriteRenderer sprite;
    public Color nActive;
    public Color yActive;
    public float tbf; //Time before falling
    public float tbr; //time before reappearing

    void Start() {
        sprite.color = yActive;
    }

    void OnTriggerEnter2D(Collider2D player) {
        if (player.tag == "Player")
        {
            Invoke("PlatformDestroy", tbf);
        }
    }

    void OnTriggerExit2D(Collider2D play) {
        if (play.tag == "Player")
        {
            CancelInvoke("PlatformRegen");
            Invoke("PlatformRegen",tbr);
        }
    }

    void PlatformDestroy() {
        sprite.color = nActive;
        plat.enabled = false;
    }

    void PlatformRegen() {
        plat.enabled = true;
        sprite.color = yActive;
    }
   
}
