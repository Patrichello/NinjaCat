using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public bool isShaking = false;
    private Vector2 pos;
    private float shake = 0.1f;

    [SerializeField]
    int health = 2;

    [SerializeField]
    Object destructable;

    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
         if(isShaking == true)
            {
                transform.position = pos + UnityEngine.Random.insideUnitCircle * shake;
            }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shuriken"))
        {
            Debug.Log("shake");
            isShaking = true;
            health--;

            if(health <= 0)
            {
                ExplodeTheObject();
            }
            Invoke("StopShaking", 0.2f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "WallCheck")
        {
            Debug.Log("shake");
            isShaking = true;
            Invoke("StopShaking", 0.5f);
        }
    }
    void StopShaking()
    {
        isShaking = false;
        transform.position = pos;
    }
    void ExplodeTheObject()
    {
        GameObject destruct = (GameObject)Instantiate(destructable);
        destruct.transform.position = transform.position;

        Destroy(gameObject);
    }
}
