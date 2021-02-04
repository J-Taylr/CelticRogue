using System.Collections;
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

    bool canDoubleJump = false;         //if player can double jump set to true

    public bool coyoteJump = true;      // for when player walks off ledges, gives time to still jump 
    public float coyoteTimer = 0.1f;    // how long players have to jump after falling off ledge

    [Header("Wall Sliding")]
    bool wallSliding;
    public float wallSlidingSpeed = 20;    // max speed character slides down walls
    [Header("Wall Jumping")]
    bool wallJumping;
    public float xJumpForce;
    public float yJumpForce;
    public float wallJumpTime;

    public Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<CharacterController2D>();
        topCollider = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
       
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        PlayerMovement(); //takes in player input and passes into fixed update
        CoyoteCheck(); // timer for coyote time
        SlideCheck() ;
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
            if (controller.isGrounded == true)
            {
                animator.SetBool("Jumping", false);
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

}
