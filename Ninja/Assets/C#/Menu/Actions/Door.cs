using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private KeyQuest keyQuest;
    private Animator anim;
    public GameObject finish;
    public GameObject textFinish;
    private bool finishActive;

    void Start()
    {
        keyQuest = GetComponent<KeyQuest>();
        anim = GetComponent<Animator>();
        finish.SetActive(false);
        textFinish.SetActive(false);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && finishActive == true)
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
            textFinish.SetActive(true);
        }
    }
  
}
