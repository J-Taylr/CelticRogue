using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAnimationEvents : MonoBehaviour
{
    public PlayerMovement playerMovement;


    private void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }


    public void FinishLedgeClimb()
    {
        playerMovement.FinishLedgeClimb();
    }


}
