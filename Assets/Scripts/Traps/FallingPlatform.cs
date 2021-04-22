using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    
    public BoxCollider2D platformCollider;
    public Animator animator;

    public bool platformActive = true;
    public float respawnDelay = 3;
    public float timeRemaining;

    private void Awake()
    {
        platformCollider = GetComponent<BoxCollider2D>();
        animator = GetComponentInChildren<Animator>();
        timeRemaining = respawnDelay;
        platformActive = true;
    }


    private void Update()
    {
        DelayTimer();
    }

    public void DelayTimer()
    {
        if (!platformActive)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                
                timeRemaining = 0;
                animator.SetBool("PlatformRegen", true);
                
            }
        }
    }

    public void StartAnimLoop()
    {
        animator.SetTrigger("PlatformFall");
    }


    public void TurnOffPlatform()
    {
        
        platformCollider.enabled = false;
        platformActive = false;
    }
   

    public void TurnOnPlatform()
    {
        animator.SetBool("PlatformRegen", false);
        platformCollider.enabled = true;
        platformActive = true;
        timeRemaining = respawnDelay;

    }




}
