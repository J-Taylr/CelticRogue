using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    public PlayerManager playerManager;
    public CharacterController2D controller;
    public PlayerFX playerFX;
    public Rigidbody2D rb;
    public Strike strike;
    public Animator animator;
    public UpgradeController upgrades;
   

    [Header("Movement")]
    public bool canMove = true;
    public float moveSpeed = 1;
    public bool isHoldingJump = false;

    private float horizontalInput;
    [Header("Jumping")]
    public float jumpForce = 400f;
    public float doubleJumpForce = 400f;
    float coyoteTimer = 0.1f;

    bool canDoubleJump = false;
    bool coyoteJump = true;

    [Header("Dashing")]
    public float dashDuration = 0.5f;
    public float dashPower = 40;
    public float dashCooldown = 2;
    public bool canDash = true;
    public bool dashCooldownActive = false;
    public bool dashing;

    public float ledgeClimbXOffset1;
    public float ledgeClimbYOffset1;
    public float ledgeClimbXOffset2;
    public float ledgeClimbYOffset2;

    [Header("Wall Movement")]
    public bool wallSliding;
    public float wallSlidingSpeed = 20;    // max speed character slides down walls
    public float wallBounce = 10;
    private bool slideAnim = false;

    [Header("Gravity")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.0f;
    public bool inGravityField = false;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        playerFX = GetComponent<PlayerFX>();
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<CharacterController2D>();
        animator = GetComponentInChildren<Animator>();
        strike = GetComponent<Strike>();

        dashing = false;
        print(rb.gravityScale);
    }

    void Update()
    {
        AdjustGravity();
        CoyoteCheck(); // timer for coyote time
        SlideCheck(); // checks if player is sliding on a wall
        DashCheck(); //checks for when player lands on floor to recharge boost


        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        if (controller.isGrounded)
        {
            animator.SetBool("OnGround", true);

        }
        else
        {
            animator.SetBool("OnGround", false);
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            controller.Move(horizontalInput * Time.fixedDeltaTime);
        }
        CheckWallSlide();
        CheckLedgeClimb();
        CheckCanMove();
    }


    public void AdjustGravity()
    {
        if (rb.velocity.y < 0 && !wallSliding && !dashing && !inGravityField) // if player is falling
        {

            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !isHoldingJump && !wallSliding && !dashing && !inGravityField)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

    }



    public void MovePlayer(float direction)
    {

        horizontalInput = direction * playerManager.moveSpeed;
    }
    public void PlayerJump() // all inputs for the players; Jump, Double Jump, Coyote Jump, WallJump
    {
        if (canMove)
        {
            

            if (!controller.isGrounded && canDoubleJump && playerManager.doubleUnlock) //double jump
            {
                
                animator.SetTrigger("Jumping");
                Vector2 yVelocity = new Vector2(0, 0);
                rb.velocity = new Vector2(rb.velocity.x, yVelocity.y); //set current Y velocity to 0

                rb.AddForce(new Vector2(0f, doubleJumpForce), ForceMode2D.Impulse);

                canDoubleJump = false;
            }

            if (controller.isGrounded) //single jump
            {
                print("jump");
                animator.SetTrigger("Jumping");
                Vector2 yVelocity = new Vector2(0, 0);
                rb.velocity = new Vector2(rb.velocity.x, yVelocity.y);
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                coyoteJump = false;
                canDoubleJump = true;
            }

            if (coyoteJump) //coyote jump 
            {
                animator.SetTrigger("Jumping");
                Vector2 yVelocity = new Vector2(0, 0);
                rb.velocity = new Vector2(rb.velocity.x, yVelocity.y);
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

                coyoteJump = false;
                canDoubleJump = true;
            }

            if (controller.isOnWall && playerManager.wallJumpUnlock) //wall jumps
            {
                controller.isOnWall = false;
                Vector2 yVelocity = new Vector2(0, 0);
                rb.velocity = new Vector2(rb.velocity.x, yVelocity.y);
                animator.SetTrigger("WallJump");



                if (controller.facingRight)
                {
                    rb.AddForce(new Vector2(wallBounce, jumpForce), ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce(new Vector2(-wallBounce, jumpForce), ForceMode2D.Impulse);

                }

                //StartCoroutine(upgrades.WallJumpBuffs());
                coyoteJump = false;
                canDoubleJump = false;
            }
        }
    }

    public void Interact()
    {
        playerManager.isInteracting = true;
    }
    public void StartDash()
    {
        if (playerManager.dashUnlock && canDash && !dashCooldownActive && canMove && horizontalInput !=0)
        {
            StartCoroutine(PlayerDash());
            dashing = true;
            print("dashStart");
        }
    }

    


    IEnumerator PlayerDash()
    {
        gameObject.layer = 11; // player does not collide with enemies
        canDash = false;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        animator.SetTrigger("Dash");
        playerFX.PlayDashParticle();
        rb.AddForce(new Vector2((horizontalInput * dashPower), 0));
        playerManager.moveSpeed += 40;
        yield return new WaitForSeconds(dashDuration);
        StartCoroutine(DashCoolDown());
        rb.gravityScale = 4;
        playerManager.moveSpeed = 40;
        gameObject.layer = 8; // player collides normally again
        dashing = false;
        print("dashend");
        //if (upgrades.dashEndExplosion)
        //{
        //    upgrades.SlamAttack();
        //}
    }


    IEnumerator DashCoolDown()
    {
        dashCooldownActive = true;
        yield return new WaitForSeconds(dashCooldown);
        dashCooldownActive = false;
    }


    public void CheckWallSlide()
    {
        if (wallSliding)
        {
           
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
    }

    public void CheckCanMove()
    {
        if (!canMove)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    public void CheckLedgeClimb()
    {
        if (controller.ledgeDetected && !controller.canClimbLedge)
        {
            controller.canClimbLedge = true;


            if (controller.facingRight)
            {
                controller.ledgePosOne = new Vector2(Mathf.Floor(controller.ledgePosBot.x + controller.k_GroundedRadius) - ledgeClimbXOffset1, Mathf.Floor(controller.ledgePosBot.y) + ledgeClimbYOffset1);
                controller.ledgePosTwo = new Vector2(Mathf.Floor(controller.ledgePosBot.x + controller.k_GroundedRadius) + ledgeClimbXOffset2, Mathf.Floor(controller.ledgePosBot.y) + ledgeClimbYOffset2);
            }
            else
            {
                controller.ledgePosOne = new Vector2(Mathf.Ceil(controller.ledgePosBot.x - controller.k_GroundedRadius) + ledgeClimbXOffset1, Mathf.Floor(controller.ledgePosBot.y) + ledgeClimbYOffset1);
                controller.ledgePosTwo = new Vector2(Mathf.Ceil(controller.ledgePosBot.x - controller.k_GroundedRadius) - ledgeClimbXOffset2, Mathf.Floor(controller.ledgePosBot.y) + ledgeClimbYOffset2);
            }

            canMove = false;
            gameObject.layer = 12;
            animator.SetBool("CanClimbLedge", controller.canClimbLedge);
        }

        if (controller.canClimbLedge)
        {
            transform.position = controller.ledgePosOne;
            
        }
    }

    public void FinishLedgeClimb()
    {
        print("TEST");
        controller.canClimbLedge = false;
        transform.position = controller.ledgePosTwo;
        gameObject.layer = 8;
        canMove = true;
        controller.ledgeDetected = false;
        animator.SetBool("CanClimbLedge", controller.canClimbLedge);
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

    public void SlideCheck()
    {
        if (controller.isOnWall && !controller.isGrounded && horizontalInput != 0 && !controller.canClimbLedge)
        {
            callSlideAnim();
            wallSliding = true;
        }
        else
        {
            animator.SetTrigger("OffWall");
            wallSliding = false;
            slideAnim = false;
        }
    }

    public void callSlideAnim()
    {
        if (slideAnim == false)
        {
            animator.SetTrigger("OnWall");
            slideAnim = true;
        }
    }


    public void DashCheck()
    {
        if (controller.isGrounded)
        {
            canDash = true;
        }
    }



    



}

