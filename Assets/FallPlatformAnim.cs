using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatformAnim : MonoBehaviour
{
    public FallingPlatform fallingPlatform;


    private void Awake()
    {
        fallingPlatform = GetComponentInParent<FallingPlatform>();
    }

    public void TurnOffPlatform()
    {
        fallingPlatform.TurnOffPlatform();
    }

    public void TurnOnPlatform()
    {
        fallingPlatform.TurnOnPlatform();
    }

}
