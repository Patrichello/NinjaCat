using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    Vector2 currentPos;
    private bool moveInBack;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPos = transform.position;
    }
    private void Update()
    {
        if(moveInBack == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentPos, 20f * Time.deltaTime);
        }

        if(transform.position.y == currentPos.y)
        {
            moveInBack = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && moveInBack == false)
        {
            Invoke("FallPlatform", 0.5f);
        }
    }
    void FallPlatform()
    {
        rb.isKinematic = false;
        Invoke("BackPlatform", 2f);
    }
    void BackPlatform()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        moveInBack = true;
    }
}
