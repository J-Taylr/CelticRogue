using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupParticleController : MonoBehaviour
{
    public ParticleSystem pickupParticle;
    private ParticleSystem.MainModule mainModule;
    private ParticleSystem.EmissionModule emission;
    private ParticleSystem.VelocityOverLifetimeModule velocity;

    private void Awake()
    {
        mainModule = pickupParticle.main;
        emission = pickupParticle.emission;
        velocity = pickupParticle.velocityOverLifetime;
    }

    private void Start()
    {

        
    }

    public void StopParticle() 
    {
        print("stop particle");
        mainModule.loop = false;

        Destroy(gameObject, 10);
    }


}
