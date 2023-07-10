using System;
using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashSpeed;
    public float dashDuration = 1f;
    public float dashCoolDown = 1f;
    public TrailRenderer tr;

    public bool isDashing;

    private bool isDash;
    private bool canDash = true;
    private float originalSpeed;

    private Rigidbody2D rb;
    private PlayerController playerController;
    private HealthController healthController;
    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        healthController = GetComponent<HealthController>();
        tr = GetComponentInChildren<TrailRenderer>();

        originalSpeed = playerController.movementSpeed;
    }

    private void Update()
    {
        if (isDash)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.P) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        if (CanDash())
        {
            canDash = false;
            isDash = true;
            anim.SetTrigger("dash");
            playerController.movementSpeed = dashSpeed;
            isDashing = true;
            // rb.velocity = new Vector2(dashSpeed * playerController.movementInputDirection, rb.velocity.y);
            tr.emitting = true;
            yield return new WaitForSeconds(dashDuration);
            isDashing = false;
            playerController.movementSpeed = originalSpeed;
            isDash = false;
            tr.emitting = false;
            yield return new WaitForSeconds(dashCoolDown);
            canDash = true;
        }

    }
    private bool CanDash()
    {
        return canDash && playerController.isGrounded && !healthController.playerDead;

    }

}
