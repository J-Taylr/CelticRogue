using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundsCheck : MonoBehaviour
{
    public CamFollow cam;
    

    float camSpeedOrig;
    private void Awake()
    {
        cam = GetComponentInParent<CamFollow>();
        camSpeedOrig = cam.camSpeed;
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cam.camSpeed *=2 ;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cam.camSpeed = camSpeedOrig;
        }
    }


}
