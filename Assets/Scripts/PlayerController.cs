﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public CharacterController2D controller;
    public Rigidbody2D rb;
    public InputMaster inputController;
    public Strike strike;
   



    [SerializeField] private float jumpForce = 400f; // Amount of force added when the player jumps.
    [SerializeField] private float doubleJumpForce = 400f; // Amount of force added when the player jumps.

    private float horizontalInput;      //Input amount via player
    public float moveSpeed = 40;        //character speed
    public float dashPower = 40;        //speed the character dashes at 

    bool canDoubleJump = false;         //if player can double jump set to true

    public bool coyoteJump = true;      // for when player walks off ledges, gives time to still jump 
    public float coyoteTimer = 0.1f;    // how long players have to jump after falling off ledge
  
    [Header("Wall Sliding")]
    bool wallSliding;
    public float wallSlidingSpeed = 20;    // max speed character slides down walls


    public Animator animator;

    private void Awake()
    {
        inputController = new InputMaster();

        SetupControls();

    }

    public void SetupControls()
    {
        //movement
        inputController.Player.Movement.performed += ctx => PlayerMovement(ctx.ReadValue<float>());
        inputController.Player.Movement.canceled += ctx => PlayerMovement(0);

        inputController.Player.Jump.performed += ctx => PlayerJump();
        inputController.Player.Dash.performed += ctx => StartDash();

        //attacks
        inputController.Player.Attack.performed += ctx => sideAttack();
        inputController.Player.AttackDown.performed += ctx => DownAttack();
        inputController.Player.AttackUp.performed += ctx => UpAttack();
    }

    private void OnEnable()
    {
        inputController.Enable();
    }

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<CharacterController2D>();
        strike = GetComponent<Strike>();
    }


    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        CoyoteCheck(); // timer for coyote time
        SlideCheck();

        if (controller.isGrounded == true)
        {
            animator.SetBool("Jumping", false);
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalInput * Time.fixedDeltaTime);
        WallSlide();

    }

    public void PlayerMovement(float direction)
    {
        horizontalInput =  direction * moveSpeed;
    }
    public void PlayerJump() // all inputs for the players; Jump, Double Jump, Coyote Jump, WallJump
    {
        print("jump");
        animator.SetBool("Jumping", true);
        if (!controller.isGrounded && canDoubleJump) //double jump
        {
            Vector2 yVelocity = new Vector2(0, 0);
            rb.velocity = new Vector2(rb.velocity.x, yVelocity.y); //set current Y velocity to 0

            rb.AddForce(new Vector2(0f, doubleJumpForce), ForceMode2D.Impulse);

            canDoubleJump = false;
        }

        if (controller.isGrounded) //single jump
        {
            Vector2 yVelocity = new Vector2(0, 0);
            rb.velocity = new Vector2(rb.velocity.x, yVelocity.y);
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            coyoteJump = false;
            canDoubleJump = true;
        }
        
        if (coyoteJump) //coyote jump 
        {

            Vector2 yVelocity = new Vector2(0, 0);
            rb.velocity = new Vector2(rb.velocity.x, yVelocity.y);
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

            coyoteJump = false;
            canDoubleJump = true;
        }

        if (controller.isOnWall) //wall jumps
        {
            controller.isOnWall = false;
            Vector2 yVelocity = new Vector2(0, 0);
            rb.velocity = new Vector2(rb.velocity.x, yVelocity.y);
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);


            coyoteJump = false;
            canDoubleJump = true;
        }



    }

    public void StartDash()
    {
        StartCoroutine(PlayerDash());
    }

    IEnumerator PlayerDash()
    {

        rb.AddForce(new Vector2((horizontalInput * dashPower), 0));
        moveSpeed += 20;
        yield return new WaitForSeconds(0.5f);
        moveSpeed = 40;
    }

    public void SlideCheck()
    {
        if (controller.isOnWall && !controller.isGrounded && horizontalInput != 0)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }


    }

    public void WallSlide()
    {
        if (wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
    }

    public void CoyoteCheck()
    {
        if (!controller.isGrounded)
        {

            coyoteTimer -= Time.deltaTime;

            if (coyoteTimer <= 0)
            {
                coyoteTimer = 0;

                coyoteJump = false;
            }

        }
        else if (controller.isGrounded)
        {
            coyoteJump = true;
            coyoteTimer = 0.1f;
        }
    }

    


    //PLAYER ATTACKS

    public void sideAttack()
    {
        print("strike");
        strike.AttackR();
    }

    public void UpAttack()
    {
        print("up strike");
        strike.AttackUp();
    }

    public void DownAttack()
    {
        print("down strike");
        strike.AttackDown();
    }


}

