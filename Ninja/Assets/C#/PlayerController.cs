using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementInputDirection;
    public float movementSpeed = 10f;

    public float jumpForce;
    public int amountOfJumps;
    public int amountOfJumpsLeft;

    private bool isFacingRight = true;
    private bool isWalking;
    public  bool isGrounded;
    public bool canJump;
    private bool isTouchingWall;
    private bool isWallSliding;

    public float groundCheckRadius;
    public float wallCheckDistance;
    public float wallSlideSpeed;
    public float movementForceInAir;
    public float airDragMultiplier;
    public float variableJumpHeightMultiplier;
   
    public LayerMask whatIsGround;

    private Rigidbody2D rb;
    private Animator anim;

    public Transform groundCheck;
    public Transform wallCheck;

    //for jumping between walls
    public bool wallJump;
    private int jumpsOnTheWall;
    public int JumpsOnTheWallLeft = 0;
    //
    public float knockbackForce;
    private bool isCooldownObstacle;

    private HealthController healthController;
    private GameProgress gameProgress;
    private GameOver gameOver;
    private PlayerDash playerDash;

    private bool canInput = true;

    private AudioSource audioSource;
    public AudioClip jumpSound;
 
    void Start()
    {
        amountOfJumpsLeft = amountOfJumps;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        healthController = GetComponent<HealthController>();
        gameProgress = FindObjectOfType<GameProgress>();
        gameOver = FindObjectOfType<GameOver>();
        playerDash = GetComponent<PlayerDash>();
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (isCooldownObstacle)
        {
            return;
        }

        CheckInput();
        CheckMovementDirection();
        CheckIfCanJump();
        CheckIfWallSliding();
        UpdateAnimations();
        if (healthController.playerDead) movementSpeed = 0;
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    public void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        canJump = isGrounded || isTouchingWall;
    }

    private void CheckMovementDirection()
    {
        if (isFacingRight && movementInputDirection < 0)
            {
                Flip();
            }
            else if (!isFacingRight && movementInputDirection > 0)
            {
                Flip();
            }
            isWalking = Mathf.Abs(rb.velocity.x) >= 0.01f ? true : false;

        
     

    }
  
    private void Flip()
    {
        if (!isWallSliding)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void CheckInput()
    {
        if (CanInput())
        {
            movementInputDirection = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }

            if (Input.GetButtonUp("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
            }
        }
       
    }
    private bool CanInput()
    {
        return canInput && !healthController.playerDead && !playerDash.isDashing;
    }


    public void Jump()
    {
        if (canJump && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
            audioSource.PlayOneShot(jumpSound);

        }
        if (wallJump && jumpsOnTheWall > 0 || canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpsOnTheWall--;
            // amountOfJumpsLeft = 0;
            amountOfJumpsLeft--;
            audioSource.PlayOneShot(jumpSound);

        }
    }

    private void CheckIfCanJump()
    {
        if (isGrounded && rb.velocity.y <= 0)
        {
            amountOfJumpsLeft = amountOfJumps;
            jumpsOnTheWall = JumpsOnTheWallLeft;
        }
        canJump = (amountOfJumpsLeft > 0);
        //canJump = (amountOfJumpsLeft <= 0) ? false : true;
        if (PlayerPrefs.HasKey("GetDoubleJump"))
        {
            amountOfJumps = 3;
        }
        else
        {
            amountOfJumps = 1;
        }
    }

    private void CheckIfWallSliding()
    {
        if ((isTouchingWall && !isGrounded && rb.velocity.y < 0) && Input.GetKey(KeyCode.LeftShift))
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    public void ApplyMovement()
    {
        if (isGrounded)
        {
                rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);

        }

        else if (!isGrounded && !isWallSliding && movementInputDirection != 0)
            {
                Vector2 forceToAdd = new Vector2(movementForceInAir * movementInputDirection, 0);
                rb.AddForce(forceToAdd);

                if (Mathf.Abs(rb.velocity.x) > movementSpeed)
                {
                    rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
                }
            }
            else if (!isGrounded && !isWallSliding && movementInputDirection == 0)
            {
                rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
            }

            if (isWallSliding)
            {
                if (rb.velocity.y < -wallSlideSpeed)
                {
                    rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
                }
            }
       
    }

    private void OnDrawGizmos()
    {
        if (wallCheck == null) return;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawWireSphere(wallCheck.position, wallCheckDistance);

       // Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }

    private void UpdateAnimations()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!healthController.playerDead)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                wallJump = true;
                jumpsOnTheWall = JumpsOnTheWallLeft;
            }

            //
            if (collision.gameObject.CompareTag("Obstacle"))
            {
               anim.SetTrigger("damageObstacle");
            }
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            wallJump = false;
        }
    }

}
