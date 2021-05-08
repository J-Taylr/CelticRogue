using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   // FMOD.Studio.EventInstance menuMusic;
   // FMOD.Studio.EventInstance inGameMusic;

    //Player sounds
    FMOD.Studio.EventInstance footStep;
    FMOD.Studio.EventInstance dash;
    FMOD.Studio.EventInstance attack;
    FMOD.Studio.EventInstance takeDamage;
    FMOD.Studio.EventInstance death;


    //enemy sounds
    FMOD.Studio.EventInstance skullSpawn;
    FMOD.Studio.EventInstance skullTakeDamage;
    FMOD.Studio.EventInstance enemyDamage;

    private void Start()
    {
        GetInstances();
    }

    private void Update()
    {
        CheckFootstepPos();
    }

    public void GetInstances()
    {
        //menuMusic = FMODUnity.RuntimeManager.CreateInstance("");
       // inGameMusic = FMODUnity.RuntimeManager.CreateInstance("");

        footStep = FMODUnity.RuntimeManager.CreateInstance("event:/Player/FootSteps");
        dash = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Dash");
        attack = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Attack");
        takeDamage = FMODUnity.RuntimeManager.CreateInstance("event:/Player/TakeDamage");
        death = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Death");


        skullSpawn = FMODUnity.RuntimeManager.CreateInstance("event:/Enemies/SkullSpawn");
        skullTakeDamage = FMODUnity.RuntimeManager.CreateInstance("event:/Enemies/SkullDamage");
        enemyDamage = FMODUnity.RuntimeManager.CreateInstance("event:/Enemies/TakeDamage");
    }


    public void Footstep()
    {
        footStep.start();
    }

    public void CheckFootstepPos()
    {
        if (GameManager.Instance.terrain == GameManager.TerrainZone.CAVE)
        {
            footStep.setParameterByName("FootSteps", 1);
        }
        else
        {
            footStep.setParameterByName("FootSteps", 0);
        }
    }

    public void PlayerAttack()
    {
        attack.start();
    }

    public void PlayerDash()
    {
        dash.start();
        
    }

    public void TakeDamage()
    {
        takeDamage.start();
    }

    public void PlayerDeath()
    {
        death.start();
    }
}
