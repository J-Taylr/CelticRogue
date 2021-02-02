using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public CharacterController2D controller;

    float horizontalMove = 0f;
    public bool jump = false;
    bool isGrounded = false;
    bool crouch = false;


    public float moveSpeed = 40;

    private void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }


    void Update()
    {
        PlayerMovement();
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
               
    }




    public void PlayerMovement()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;

        

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;

            if (!controller.m_Grounded && !jump)
            {
                jump = true;
            }

        }

        
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;

        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }


    }
}
