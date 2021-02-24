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
    public CamFollow cam;
    


    [Header("Movement")]
    private float horizontalInput;      
        
    public float dashPower = 40;        
    public float jumpForce = 400f;
    public float wallBounce = 10;
    public float doubleJumpForce = 400f; 

    bool canDoubleJump = false;        
    bool coyoteJump = true;      
    float coyoteTimer = 0.1f;    
  
    [Header("Wall Sliding")]
    bool wallSliding;
    public float wallSlidingSpeed = 20;    // max speed character slides down walls

    [Header("Combat Upgrades")]
    public GameObject bullet;
    private GameObject newBullet;
    public bool ranged;
    public bool slam;
    bool slamming;
    public float slamRadius;

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
        inputController.Player.Interact.performed += ctx => Interact();

        inputController.Player.Camup.performed += ctx => cam.cameraUp = true;
        inputController.Player.CamDown.performed += ctx => cam.cameraDown = true;

        inputController.Player.Camup.canceled += ctx => cam.cameraUp = false;
        inputController.Player.CamDown.canceled += ctx => cam.cameraDown = false;
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
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamFollow>();
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

        if (slamming && controller.isGrounded)
        {
            SlamDown();
            gameObject.GetComponent<Rigidbody2D>().gravityScale /= 5f;
            slamming = false;
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalInput * Time.fixedDeltaTime);
        WallSlide();

    }

    

    public void PlayerMovement(float direction)
    {
        horizontalInput =  direction * playerManager.moveSpeed;
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
        }
    }

    

    IEnumerator PlayerDash()
    {

        rb.AddForce(new Vector2((horizontalInput * dashPower), 0));
        playerManager.moveSpeed += 20;
        yield return new WaitForSeconds(0.5f);
        playerManager.moveSpeed = 40;
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

        if (playerManager.RollCritical() == true)
        {
            strike.AttackR(playerManager.strikeDamage * 3); //critical mulitplier can be changed
        }
        else
        {
            strike.AttackR(playerManager.strikeDamage);
        }
        if (ranged && gameObject.transform.localScale.x < 0f)
        {
            newBullet = Instantiate(bullet, new Vector3(gameObject.transform.position.x - 1f, gameObject.transform.position.y, 0f), Quaternion.identity);
            newBullet.GetComponent<Bullet>().speed = -500f;
        }
        else if (ranged && gameObject.transform.localScale.x > 0)
        {
            newBullet = Instantiate(bullet, new Vector3(gameObject.transform.position.x + 1f, gameObject.transform.position.y, 0f), Quaternion.identity);
            newBullet.GetComponent<Bullet>().speed = 500f;
        }

        if (!controller.isGrounded)
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale *= 5f;
            slamming = true;
        }

    }

    public void UpAttack()
    {
        print("up strike");
        if (playerManager.RollCritical() == true)
        {
            strike.AttackUp(playerManager.strikeDamage * 3);
        }
        else
        {
            strike.AttackUp(playerManager.strikeDamage);
        }
        
    }

    public void DownAttack()
    {
        print("down strike");
        if (playerManager.RollCritical() == true)
        {
            strike.AttackDown(playerManager.strikeDamage * 3);
        }
        else
        {
            strike.AttackDown(playerManager.strikeDamage);
        }
        
    }


    IEnumerator AttackAni()
    {
        Debug.Log("hit");
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(.5f);
        animator.SetBool("Attack", false);
    }

    void SlamDown() {
        //Collider2D[] = Physics2D.OverlapCircle(gameobject.transform.position, slamRadius,1<<LayerMask.NameToLayer("Enemy"));
    }

    

}

