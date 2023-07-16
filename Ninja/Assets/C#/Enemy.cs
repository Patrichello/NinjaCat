using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public int maxHealth;
    private int currentHealth;

    public float speed;
    private float stopTime;
    public float startStopTime;
    private float normalSpeed;
    public int damage;

    private HealthController healthController;
    private Rigidbody2D rb;
    private Animator anim;

    private bool isWalking;
    private bool isAttacking = false;
    public bool canAttack = false;

    public float attackDuration = 0.5f;
    public float attackRange;

    public LayerMask player;
    private ScoreManager scoreManager;
    public Transform attackPos;

    public Transform[] moveSpots;
    private int randomSpot;
    private float waitTime;
    public float startWaitTime;

    private bool isFacingRight = true;
    private float movementInputDirection;

    public float detectionRange;
    private float distanceToPlayer;

    private Transform playerPos;
    private bool isPatrol;
    private bool damageSet;

    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        healthController = FindAnyObjectByType<HealthController>();
        scoreManager = FindObjectOfType<ScoreManager>();

        currentHealth = maxHealth;
        normalSpeed = speed;

        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    private void Update()
    {
        CheckMovementDirection();
        UpdateSpeed();
        UpdateAttack();

        anim.SetBool("isWalking", isWalking);
        distanceToPlayer = Vector2.Distance(transform.position, playerPos.position);
    }

    private void FixedUpdate()
    {

        if (!canAttack && distanceToPlayer > detectionRange)
        {
            Patrol();
            isPatrol = true;
        }
        if (!canAttack && distanceToPlayer < detectionRange)
        {
            Atack();
            isPatrol = false;
        }

        damageSet = false;
        if(damageSet) rb.velocity = Vector2.zero;
     
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void TakeDamage(int damage)
    {
        damageSet = true;
        stopTime = startStopTime;
        currentHealth -= damage;

        anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            scoreManager.Kill();
            Die();
        }
    }

    void Die()
    {
        anim.SetBool("isDead", true);

        GetComponent<Collider2D>().enabled = false;
        Destroy(GetComponent<Rigidbody2D>());
        this.enabled = false;
        StartCoroutine(DeleteEnemy());
    }

    void UpdateSpeed()
    {
        if (stopTime <= 0)
        {
            speed = normalSpeed;
        }
        else
        {
            speed = 0;
            stopTime -= Time.deltaTime;
        }
    }

    void UpdateAttack()
    {
        if (!isAttacking && canAttack)
        {
            if (timeBtwAttack <= 0)
            {
                StartCoroutine(AttackCoroutine());
                timeBtwAttack = startTimeBtwAttack;
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canAttack = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canAttack = false;
        }
    }

    private void CheckMovementDirection()
    {
        Vector2 direction = (moveSpots[randomSpot].position - transform.position).normalized;
        movementInputDirection = direction.x;

        if (isFacingRight && movementInputDirection < 0 && isPatrol)
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirection > 0 && isPatrol)
        {
            Flip();
        }

        isWalking = Mathf.Abs(rb.velocity.x) >= 0.01f ? true : false;

    }

    private IEnumerator DeleteEnemy()
    {
        yield return new WaitForSeconds(3.5f);
        Destroy(gameObject);
    }

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(attackDuration);
        healthController.GetComponent<HealthController>().TakeDamage(damage);
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPos == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    void Patrol()
    {
        Vector2 direction = (moveSpots[randomSpot].position - transform.position).normalized;
        rb.velocity = direction * speed;

        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) <= 0.2f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    void Atack()
    {
        if (playerPos.position.x > transform.position.x && !isFacingRight)
        {
            Flip();
        }
        else if (playerPos.position.x < transform.position.x && isFacingRight)
        {
            Flip();
        }
        speed *= 2;
        rb.velocity = new Vector2(speed * GetFacingDirection(), rb.velocity.y);

    }

    float GetFacingDirection()
    {
        return isFacingRight ? 1f : -1f;
    }

}
