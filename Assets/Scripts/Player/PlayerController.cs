using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    public PlayerManager playerManager;
    public CharacterController2D controller;
    public Rigidbody2D rb;
    public InputMaster inputController;
    public Strike strike;
    public Animator animator;

    

    [Header("Combat")]
    public int standardAttackDamage = 5;

    [Header("Movement")]
    private float horizontalInput;      
    public float moveSpeed = 40;        
    public float dashPower = 40;        
    public float jumpForce = 400f; 
    public float doubleJumpForce = 400f; 

    bool canDoubleJump = false;        
    bool coyoteJump = true;      
    float coyoteTimer = 0.1f;    
  
    [Header("Wall Sliding")]
    bool wallSliding;
    public float wallSlidingSpeed = 20;    // max speed character slides down walls

    private void Awake()
    {
        SetupControls();
    }

    public void SetupControls()
    {
        inputController = new InputMaster();

        //movement
        inputController.Player.Movement.performed += ctx => PlayerMovement(ctx.ReadValue<float>());
        inputController.Player.Movement.canceled += ctx => PlayerMovement(0);

        inputController.Player.Jump.performed += ctx => PlayerJump();
        inputController.Player.Dash.performed += ctx => StartDash();

        //attacks
        inputController.Player.Attack.performed += ctx => sideAttack();
        inputController.Player.AttackDown.performed += ctx => DownAttack();
        inputController.Player.AttackUp.performed += ctx => UpAttack();

        //other
        inputController.Player.Interact.performed += ctx => StartCoroutine(Interact());
    }

    private void OnEnable()
    {
        inputController.Enable();
    }

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<CharacterController2D>();
        animator = GetComponentInChildren<Animator>();
        strike = GetComponent<Strike>();
    }


    void Update()
    {
        CoyoteCheck(); // timer for coyote time
        SlideCheck(); // checks if player is sliding on a wall
        

        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
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
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);


            coyoteJump = false;
            canDoubleJump = true;
        }



    }

    IEnumerator Interact()
    {
        playerManager.isInteracting = true;
        yield return new WaitForSeconds(1);
        playerManager.isInteracting = false;
    }
    public void StartDash()
    {
        if (playerManager.dashUnlock)
        {
            StartCoroutine(PlayerDash());
        }
    }

    

    IEnumerator PlayerDash()
    {

        rb.AddForce(new Vector2((horizontalInput * dashPower), 0));
        moveSpeed += 20;
        yield return new WaitForSeconds(0.5f);
        moveSpeed = 40;
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

    //PLAYER ATTACKS

    public void sideAttack()
    {
        StartCoroutine("AttackAni");
        print("strike");
        strike.AttackR(standardAttackDamage);
    }

    public void UpAttack()
    {
        print("up strike");
        strike.AttackUp(standardAttackDamage);
    }

    public void DownAttack()
    {
        print("down strike");
        strike.AttackDown(standardAttackDamage);
    }
    IEnumerator AttackAni()
    {
        Debug.Log("hit");
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(1);
        animator.SetBool("Attack", false);
    }

}

