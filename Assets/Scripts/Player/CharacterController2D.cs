using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{

	[Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;    // How much to smooth out the movement
	[SerializeField] private bool airControl = false;                           // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask whatIsGround;                            // A mask determining what is ground to the character
	[SerializeField] private Transform groundCheck;                         // A position marking where to check if the player is grounded.
	[SerializeField] private Transform wallCheck;                           //A position marking where to check if the player is on a wall
	[SerializeField] private Transform ledgeCheck;

	public float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	public bool isGrounded;            // Whether or not the player is grounded.
	public bool isOnWall;             // Whether or not the player is on a wall
	public bool isTouchingLedge;
	public bool canClimbLedge = false;
	

	public bool ledgeDetected;
	public Vector2 ledgePosBot;
	public Vector2 ledgePosOne;
	public Vector2 ledgePosTwo;
	
	private Rigidbody2D m_Rigidbody2D;
	public bool facingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;


	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();

	}
	private void OnDrawGizmosSelected()
	{

	}

	private void FixedUpdate()
	{
		CheckGround();
		CheckSurroundings();
		
	}


	public void Move(float move)
	{

		//only control the player if grounded or airControl is turned on
		if (isGrounded || airControl)
		{

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, movementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !facingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && facingRight)
			{
				// ... flip the player.
				Flip();
			}
		}


	}


	public void CheckGround()
    {
		bool wasGrounded = isGrounded;
		isGrounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, k_GroundedRadius, whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				isGrounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}

	public void CheckSurroundings()
    {
		isOnWall = false;
		isTouchingLedge = false;

		Collider2D[] wallColliders = Physics2D.OverlapCircleAll(wallCheck.position, k_GroundedRadius, whatIsGround);///
		for (int i = 0; i < wallColliders.Length; i++)
		{
			if (wallColliders[i].gameObject != gameObject)
			{
				isOnWall = true;
			}
		}

		Collider2D[] ledgeColliders = Physics2D.OverlapCircleAll(ledgeCheck.position, k_GroundedRadius, whatIsGround);///
		for (int i = 0; i < ledgeColliders.Length; i++)
		{
			if (ledgeColliders[i].gameObject != gameObject)
			{
				isTouchingLedge = true;
			}
		}

		if (isOnWall && !isTouchingLedge && !ledgeDetected)
        {
			ledgeDetected = true;
			ledgePosBot = wallCheck.position;
        }


	}

	

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
