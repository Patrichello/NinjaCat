using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 200f;
    private Rigidbody2D rb;
    public int damageShuriken = 40;
    public GameObject impactEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void Update()
    {
        // Вращение пули вокруг оси Z
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damageShuriken);
        }

        // Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
