using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAnimationEvents : MonoBehaviour
{
    public AudioManager audioManager;
    public PlayerMovement playerMovement;
    public PlayerManager playerManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        playerMovement = GetComponentInParent<PlayerMovement>();
        playerManager = GetComponentInParent<PlayerManager>();
    }


    public void FinishLedgeClimb()
    {
        playerMovement.FinishLedgeClimb();
    }


    public void EndImmunity()
    {
        playerManager.isImmune = false;
    }


    public void PlayFootStep()
    {
        print("footstep");
        audioManager.Footstep();
    }

    public void PlayAttack()
    {
        audioManager.PlayerAttack();
    }

    public void PlayDash()
    {
        audioManager.PlayerDash();
    }

    public void PlayDamage()
    {
        audioManager.TakeDamage();
    }

    public void PlayDeath()
    {
        audioManager.PlayerDeath();
    }

    public void KillCharacter()
    {

    }

}
