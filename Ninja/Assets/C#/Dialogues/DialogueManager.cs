//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class DialogueManager : MonoBehaviour
//{

//    public Text dialogueText;
//    public Text nameText;

//    public Animator boxAnim;
//    public Animator startAnim;

//    private Queue<string> sentences;

//    private void Start()
//    {
//        sentences = new Queue<string>();
//    }

//    public void StartDialogue(Dialogue dialogue)
//    {
//        boxAnim.SetBool("boxOpen", true);
//        startAnim.SetBool("startOpen", false);

//        nameText.text = dialogue.name;
//        sentences.Clear();

//        foreach (string sentence in dialogue.sentences)
//        {
//            sentences.Enqueue(sentence);
//        }
//        DisplayNextSentence();
//    }
//    public void DisplayNextSentence()
//    {
//        if(sentences.Count == 0)
//        {
//            EndDialogue();
//            return;
//        }
//        string sentence = sentences.Dequeue();
//        StopAllCoroutines();
//        StartCoroutine(TypeSentence(sentence));
//    }

//    IEnumerator TypeSentence(string sentence)
//    {
//        dialogueText.text = "";
//        foreach (char letter in sentence.ToCharArray())
//        {
//            dialogueText.text += letter;
//            yield return null;
//        }
//    }

//    public void EndDialogue()
//    {
//        boxAnim.SetBool("boxOpen", false);
//    }
//}
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
    private bool nextSentenceIsStudent; // Флаг, указывающий, что следующая строка должна быть от студента


    public GameObject senseiToDisappear;
    public bool senseiFinishTalk;

    private void Start()
    {
        sentences = new Queue<string>();
        nextSentenceIsStudent = false;
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
            nameText.text = "Cat Ninja";
            nextSentenceIsStudent = false;
        }
        else
        {
            nameText.text = "Sensei";

            if (sentence.EndsWith("?"))
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
            senseiToDisappear.SetActive(false);
        }
    }
}
