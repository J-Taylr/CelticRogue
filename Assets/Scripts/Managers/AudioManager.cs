using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    FMOD.Studio.EventInstance menuMusic;
    FMOD.Studio.EventInstance inGameMusic;
    FMOD.Studio.EventInstance bossMusic;

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

    [Tooltip("1 = Menu, 2 = Main Level")]
    public int scene = 0;
    private void Start()
    {
        inGameMusic.release();
        GetInstances();
        ChooseMusic();
    }

    private void Update()
    {
        CheckMusicPos();
        CheckFootstepPos();
    }

    public void ChooseMusic()
    {
        switch (scene)
        {
            case 1:
                menuMusic.start();
                break;
            case 2:
                inGameMusic.start();
                break;

           
        }

           
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

    

    public void CheckMusicPos()
    {
        switch (GameManager.Instance.terrain)
        {
            case GameManager.TerrainZone.FOREST:
                inGameMusic.setParameterByName("Areas", 0);

                break;
            case GameManager.TerrainZone.SWAMP:
                inGameMusic.setParameterByName("Areas", 1);

                break;
            case GameManager.TerrainZone.CAVE:
                inGameMusic.setParameterByName("Areas", 2);

                break;
            
        }
    }

    public void Footstep()
    {
        footStep.start();
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


    public void GetInstances()
    {
        menuMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/TitleScreenMusic");
        inGameMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Main Music");
        bossMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Boss Music");

        footStep = FMODUnity.RuntimeManager.CreateInstance("event:/Player/FootSteps");
        dash = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Dash");
        attack = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Attack");
        takeDamage = FMODUnity.RuntimeManager.CreateInstance("event:/Player/TakeDamage");
        death = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Death");


        skullSpawn = FMODUnity.RuntimeManager.CreateInstance("event:/Enemies/SkullSpawn");
        skullTakeDamage = FMODUnity.RuntimeManager.CreateInstance("event:/Enemies/SkullDamage");
        enemyDamage = FMODUnity.RuntimeManager.CreateInstance("event:/Enemies/TakeDamage");
    }

}
