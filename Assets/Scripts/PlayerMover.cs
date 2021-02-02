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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<CharacterController2D>();
    }


   

    void Update()
    {
        PlayerMovement();
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime);        
    }




    public void PlayerMovement()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if (!controller.isGrounded && canDoubleJump == true && Input.GetButtonDown("Jump")) // double jump 
        {
            print("double");
            rb.AddForce(new Vector2(0f, jumpForce));
            canDoubleJump = false;
        }


        if (controller.isGrounded && Input.GetButtonDown("Jump")) //single jump
        {
            // Add a vertical force to the player.
            controller.isGrounded = false;
            canDoubleJump = true;

            rb.AddForce(new Vector2(0f, jumpForce));//here for 
        }



    }
}
