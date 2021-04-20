using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAnimationEvents : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerManager playerManager;

    private void Awake()
    {
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
}
