using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public KeyQuest keyQuest;
    private Animator anim;
    public GameObject finish;
    private bool finishActive;

    void Start()
    {
        anim = GetComponent<Animator>();
        finish.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N) && finishActive == true)
        {
            finish.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && keyQuest.questComplete == true)
        {
            anim.SetBool("isOpen", true);
            finishActive = true;
        }
    }
  
}
