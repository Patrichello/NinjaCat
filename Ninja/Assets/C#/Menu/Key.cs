using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private KeyQuest keyQuest;

    private void Start()
    {
        keyQuest = FindObjectOfType<KeyQuest>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            keyQuest.GetKey();
            gameObject.SetActive(false);
           // Destroy(gameObject);
        }
    }
}
