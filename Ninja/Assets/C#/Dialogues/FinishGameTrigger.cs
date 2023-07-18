using UnityEngine;

public class FinishGameTrigger : MonoBehaviour
{
    public GameObject finish;
    private DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        finish.SetActive(false);
    }

    void Update()
    {
        if(dialogueManager.finishGame == true)
        {
            finish.SetActive(true);
        }
    }
}
