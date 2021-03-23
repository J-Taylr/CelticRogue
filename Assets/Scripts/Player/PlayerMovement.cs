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
    public Rigidbody2D rb;
    public Strike strike;
    public Animator animator;
    public UpgradeController upgrades;


    [Header("Movement")]
    private float horizontalInput;
    public float dashPower = 40;

    public float jumpForce = 400f;
    public float wallBounce = 10;
    public float doubleJumpForce = 400f;

    public bool stickIsVertical = false;
    public bool isHoldingJump = false;

    bool canDoubleJump = false;
    bool coyoteJump = true;
    float coyoteTimer = 0.1f;

    public bool dashing;

    [Header("Wall Sliding")]
    bool wallSliding;
    public float wallSlidingSpeed = 20;    // max speed character slides down walls

    [Header("Gravity")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.0f;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<CharacterController2D>();
        animator = GetComponentInChildren<Animator>();
        strike = GetComponent<Strike>();
        
        dashing = false;
    }

    void Update()
    {
        CoyoteCheck(); // timer for coyote time
        SlideCheck(); // checks if player is sliding on a wall
       

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
        controller.Move(horizontalInput * Time.fixedDeltaTime);
        WallSlide();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

   

    public void MovePlayer(float direction)
    {
        
        horizontalInput = direction * playerManager.moveSpeed;
    }
    public void PlayerJump() // all inputs for the players; Jump, Double Jump, Coyote Jump, WallJump
    {
        animator.SetTrigger("Jumping");

        if (!controller.isGrounded && canDoubleJump && playerManager.doubleUnlock) //double jump
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

        if (controller.isOnWall && playerManager.wallJumpUnlock) //wall jumps
        {
            controller.isOnWall = false;
            Vector2 yVelocity = new Vector2(0, 0);
            rb.velocity = new Vector2(rb.velocity.x, yVelocity.y);



            if (controller.facingRight)
            {
                rb.AddForce(new Vector2(-wallBounce, jumpForce), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(--wallBounce, jumpForce), ForceMode2D.Impulse);

            }

            StartCoroutine(upgrades.WallJumpBuffs());
            coyoteJump = false;
            canDoubleJump = false;
        }
    }

    public void Interact()
    {
        playerManager.isInteracting = true;
    }
    public void StartDash()
    {
        if (playerManager.dashUnlock)
        {
            StartCoroutine(PlayerDash());
            dashing = true;
            print("dashStart"); ;
        }
    }



    IEnumerator PlayerDash()
    {
        rb.AddForce(new Vector2((horizontalInput * dashPower), 0));
        playerManager.moveSpeed += 20;
        yield return new WaitForSeconds(0.5f);
        playerManager.moveSpeed = 40;
        dashing = false;
        print("dashend");
        if (upgrades.dashEndExplosion)
        {
            upgrades.SlamAttack();
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
   

    IEnumerator AttackAni()
    { 
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(.5f);
        animator.SetBool("Attack", false);
    }
}

