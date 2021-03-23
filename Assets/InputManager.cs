using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Components")]
    public InputMaster inputController;
    public PlayerActions playerActions;
    public CamFollow cam;


    private void Awake()
    {
        SetupControls();
        playerActions = GetComponent<PlayerActions>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamFollow>();


    }

    public void SetupControls()
    {
        inputController = new InputMaster();

        //movement
        inputController.Player.HorizontalMovement.performed += ctx => playerActions.PlayerMovement(ctx.ReadValue<float>());
        inputController.Player.HorizontalMovement.canceled += ctx => playerActions.PlayerMovement(0);

        inputController.Player.VerticalMovement.performed += ctx => playerActions.stickIsVertical = true;
        inputController.Player.VerticalMovement.canceled += ctx => playerActions.stickIsVertical = false;


        inputController.Player.Jump.performed += ctx => playerActions.PlayerJump();
        inputController.Player.Dash.performed += ctx => playerActions.StartDash();

        //attacks
        inputController.Player.AttackDown.performed += ctx => playerActions.DownAttack();
        inputController.Player.AttackUp.performed += ctx => playerActions.UpAttack();
        inputController.Player.Attack.performed += ctx => playerActions.sideAttack();

        //other
        inputController.Player.Interact.performed += ctx => playerActions.Interact();

        inputController.Player.Camup.performed += ctx => cam.cameraUp = true;
        inputController.Player.CamDown.performed += ctx => cam.cameraDown = true;

        inputController.Player.Camup.canceled += ctx => cam.CamReturnUp();
        inputController.Player.CamDown.canceled += ctx => cam.CamReturnDown();

        inputController.Player.Pause.canceled += ctx => playerActions.ReturnToMenu();

    }
}
