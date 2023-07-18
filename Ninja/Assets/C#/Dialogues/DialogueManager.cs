using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public TMP_Text nameText;

    public Animator boxAnim;
    public Animator startAnim;

    private Queue<string> sentences;
    private bool nextSentenceIsStudent; // Flag indicating that the next string should be from a student


    public GameObject senseiToDisappear;
    public bool senseiFinishTalk;

    private ScoreManager scoreManager;
    public bool finishGame;
    public GameObject dialogBoxToDisappear;

    private void Start()
    {
        sentences = new Queue<string>();
        nextSentenceIsStudent = false;
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        boxAnim.SetBool("boxOpen", true);
        startAnim.SetBool("startOpen", false);

        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        if (nextSentenceIsStudent)
        {
            nameText.text = scoreManager.playerNN;//"Cat Ninja";
            nextSentenceIsStudent = false;
        }
        else
        {
            nameText.text = "Sensei";

            if (sentence.EndsWith("?") || sentence.EndsWith("!!"))
            {
                nextSentenceIsStudent = true;
            }
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        boxAnim.SetBool("boxOpen", false);
        Debug.Log("EndDialog");

        senseiFinishTalk = true;

        if (senseiToDisappear != null)
        {
            dialogBoxToDisappear.SetActive(false);
            senseiToDisappear.SetActive(false);
            finishGame = true;

        }
    }
}
