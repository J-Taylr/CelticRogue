using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Components")]
    public InputMaster inputController;
    public PlayerMovement playerMovement;
    public PlayerManager playerManager;
    public Strike strike;
    public CamFollow cam;
    

    public bool TestIsJumping;
    private void Awake()
    {
        SetupControls();
        playerMovement = GetComponent<PlayerMovement>();
        playerManager = GetComponent<PlayerManager>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamFollow>();
        strike = GetComponent<Strike>();

    }

    public void SetupControls()
    {
        
            inputController = new InputMaster();
            inputController.Enable();
            //movement
            inputController.Player.HorizontalMovement.performed += ctx => playerMovement.MovePlayer(ctx.ReadValue<float>());
            inputController.Player.HorizontalMovement.canceled += ctx => playerMovement.MovePlayer(0);

            inputController.Player.VerticalMovement.performed += ctx => strike.stickIsVertical = true;
            inputController.Player.VerticalMovement.canceled += ctx => strike.stickIsVertical = false;


            inputController.Player.Jump.performed += ctx => playerMovement.PlayerJump();
            inputController.Player.Jump.performed += ctx => playerMovement.isHoldingJump = true;
            inputController.Player.Jump.canceled += ctx => playerMovement.isHoldingJump = false;



            inputController.Player.Dash.performed += ctx => playerMovement.StartDash();

            //attacks
            inputController.Player.AttackDown.performed += ctx => strike.AttackDown();
            inputController.Player.AttackUp.performed += ctx => strike.AttackUp();
            inputController.Player.Attack.performed += ctx => strike.AttackSide();

            //other
            inputController.Player.Interact.performed += ctx => playerMovement.Interact();

            inputController.Player.Camup.performed += ctx => cam.cameraUp = true;
            inputController.Player.CamDown.performed += ctx => cam.cameraDown = true;

            inputController.Player.Camup.canceled += ctx => cam.CamReturnUp();
            inputController.Player.CamDown.canceled += ctx => cam.CamReturnDown();

             inputController.Player.Menu.performed += ctx => playerManager.ToggleStatMenu();
            inputController.Player.ToggleMap.performed += ctx => playerManager.ToggleMap();


    }


    
}
