using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    public FallingPlatform platform;
    
    void Start()
    {
        platform = GetComponentInParent<FallingPlatform>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && platform.platformActive == true)
        {
            platform.StartAnimLoop();
            
        }
    }

   
}
