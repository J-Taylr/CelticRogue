using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupParticleController : MonoBehaviour
{
    public ParticleSystem pickupParticle;
    private ParticleSystem.MainModule mainModule;

    private void Awake()
    {
        mainModule = pickupParticle.main;
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
