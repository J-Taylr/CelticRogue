using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public CharacterController2D controller;
    public Rigidbody2D rb;

    [SerializeField] private float jumpForce = 400f; // Amount of force added when the player jumps.
    [SerializeField] private float doubleJumpForce = 400f; // Amount of force added when the player jumps.

    float horizontalMove = 0f;
    public float moveSpeed = 40;

    bool canDoubleJump = false;
   public bool coyoteJump = true; // specifically for coyote time jumps not for regular
   public  float coyoteTimer = 0.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<CharacterController2D>();
    }


    void Update()
    {
        PlayerMovement(); //takes in player input and passes into fixed update
        CoyoteCheck(); // timer for coyote time
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime);        
    }

    public void PlayerMovement()
    {        
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        PlayerJump();
    }

    public void PlayerJump()
    {
        if (!controller.isGrounded && canDoubleJump == true && Input.GetButtonDown("Jump")) // double jump 
        {
            Vector2 yVelocity = new Vector2(0, 0);
            rb.velocity = new Vector2(rb.velocity.x, yVelocity.y); //set current Y velocity to 0
           
            rb.AddForce(new Vector2(0f, doubleJumpForce), ForceMode2D.Impulse);

            canDoubleJump = false;
        }

        if (controller.isGrounded && Input.GetButtonDown("Jump")) //single jump
        {
            Vector2 yVelocity = new Vector2(0, 0);
            rb.velocity = new Vector2(rb.velocity.x, yVelocity.y);

            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

            coyoteJump = false;
            canDoubleJump = true;
        }

        if (coyoteJump && !controller.isGrounded && Input.GetButtonDown("Jump")) //coyote jump 
        {

            Vector2 yVelocity = new Vector2(0, 0);
            rb.velocity = new Vector2(rb.velocity.x, yVelocity.y);
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

            coyoteJump = false;
            canDoubleJump = true;
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
            coyoteTimer = 0.2f;
        }
    }

}
