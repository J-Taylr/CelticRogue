﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public CharacterController2D controller;
    public Rigidbody2D rb;
    public BoxCollider2D topCollider;

    [SerializeField] private float jumpForce = 400f; // Amount of force added when the player jumps.
    [SerializeField] private float doubleJumpForce = 400f; // Amount of force added when the player jumps.

    private float horizontalInput;      //Input amount via player
    public float moveSpeed = 40;        //character speed
    public float dashPower = 40;        //speed the character dashes at 
    public float grindSpeedMax = 20;    // max speed character slides down walls

    bool canDoubleJump = false;         //if player can double jump set to true

    public bool coyoteJump = true;      // for when player walks off ledges, gives time to still jump 
    public float coyoteTimer = 0.1f;    // how long players have to jump after falling off ledge
    public float wallHoldTimer = 0.5f;  // how long the player grabs the wall before sliding down


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<CharacterController2D>();
        topCollider = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        PlayerMovement(); //takes in player input and passes into fixed update
        CoyoteCheck(); // timer for coyote time
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalInput * Time.fixedDeltaTime);
        WallSlide();


    }

    public void PlayerMovement()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal") * moveSpeed;
        PlayerJump();
        PlayerCrouch();

        if (Input.GetKeyDown(KeyCode.LeftShift)) // shift for players dash
        {
            StartCoroutine(PlayerDash());
        }
        
    }

    public void PlayerJump() // all inputs for the players; Jump, Double Jump, Coyote Jump, WallJump
    {
        if (Input.GetButtonDown("Jump"))
        {
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

    } 


    IEnumerator PlayerDash()
    {

        rb.AddForce(new Vector2((horizontalInput * dashPower), 0));
        moveSpeed += 20;
        yield return new WaitForSeconds(0.5f);
        moveSpeed = 40;
    }


    public void PlayerCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            topCollider.enabled = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            topCollider.enabled = true;
        }

    }



    public void WallSlide()
    {

        if (controller.isOnWall)
        {

            wallHoldTimer -= Time.fixedDeltaTime;
            rb.velocity = new Vector2(0, -rb.velocity.y);


            if (wallHoldTimer <= 0)
            {
                wallHoldTimer = 0;

                var mag = rb.velocity.magnitude;
                if (mag > grindSpeedMax)
                {


                    var velComp = mag - grindSpeedMax;
                    rb.velocity = new Vector2(0, velComp);
                    
                }
            }

            if (!controller.isOnWall)
            {
                wallHoldTimer = 1f;
            }

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

}
