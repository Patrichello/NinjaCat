using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAnimator : MonoBehaviour
{
    public Animator startAnim;
    public Animator senseiAnim;
    public DialogueManager dm;

    public GameObject objectToDisappear;
    private DialogueManager dialogueManager;
    public bool senseiFinish;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void Update()
    {
        senseiFinish = dialogueManager.senseiFinishTalk;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        startAnim.SetBool("startOpen", true);
        senseiAnim.SetBool("talk", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            startAnim.SetBool("startOpen", false);
            senseiAnim.SetBool("talk", false);

            if (objectToDisappear != null && senseiFinish == true)
            {
                dm.EndDialogue();
                objectToDisappear.SetActive(false);
            }
        }
    }

}

