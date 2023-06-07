using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float movementInputDirection;
    public float movementSpeed = 10f;

    public float jumpForce = 1f;
    public int amountOfJumps = 1;
    private int amountOfJumpsLeft;
   
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
    public float dashSpeed;
    public float dashDuration = 1f;
    public float dashCoolDown = 1f;
    public bool isDash;
    private bool canDash = true;
    public TrailRenderer tr;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        tr = GetComponentInChildren<TrailRenderer>();
        amountOfJumpsLeft = amountOfJumps;
    }

    void Update()
    {
       if (isDash)
        {
            return;
        }
       
        CheckInput();
        CheckMovementDirection();
        CheckIfCanJump();
        CheckIfWallSliding();
        UpdateAnimations();

    }

    private void FixedUpdate()
    {
        if (isDash)
        {
            return;
        }
        ApplyMovement();
        CheckSurroundings();
    }
   
    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

    }

    private void CheckMovementDirection()
    {
        if(isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if(!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }
   
       if(Mathf.Abs(rb.velocity.x) >= 0.01f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
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
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();

        }

        if (Input.GetButtonUp("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }
        if (Input.GetKeyDown(KeyCode.P) && canDash)
        {
            StartCoroutine(Dash());
        }
       
    }
    
    private void Jump()
    {
        if (canJump && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
        }
        if (wallJump && jumpsOnTheWall > 0 && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpsOnTheWall--;
        }
    }

    private void CheckIfCanJump()
    {
        if (isGrounded && rb.velocity.y <= 0)
        {
            amountOfJumpsLeft = amountOfJumps;
            jumpsOnTheWall = JumpsOnTheWallLeft;
        }
        if (amountOfJumpsLeft <= 0)
        {
            canJump = false;
        }
        else
        {
            canJump = true;
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

    private void ApplyMovement()
    {
        if (isGrounded)
        {
             rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
        }

        else if (!isGrounded && !isWallSliding && movementInputDirection !=0)
        {
            Vector2 forceToAdd = new Vector2(movementForceInAir * movementInputDirection, 0);
            rb.AddForce(forceToAdd);

            if(Mathf.Abs(rb.velocity.x) > movementSpeed)
            {
                rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
            }
        }
        else if(!isGrounded && !isWallSliding && movementInputDirection == 0)
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
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
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
        if (collision.gameObject.CompareTag("Wall"))
        {
            wallJump = true;
            jumpsOnTheWall = JumpsOnTheWallLeft;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            wallJump = false;
        }
    }

    private IEnumerator Dash()
    {
        if (isGrounded)
        {
            canDash = false;
            isDash = true;
            rb.velocity = new Vector2(dashSpeed * movementInputDirection, rb.velocity.y);
            tr.emitting = true;
            yield return new WaitForSeconds(dashDuration);
            isDash = false;
            tr.emitting = false;
            yield return new WaitForSeconds(dashCoolDown);
            canDash = true;

        }
    }
 
}
