using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFX : MonoBehaviour
{
    [Header("Components")]
    public ParticleSystem dashParticle;

  
        
    
    void Start()
    {
       
        dashParticle = GetComponentInChildren<ParticleSystem>();
    }

    public void PlayDashParticle()
    {
       
        dashParticle.Play();
    }

    

  
}
