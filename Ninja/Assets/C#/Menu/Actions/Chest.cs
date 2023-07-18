using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public KeyQuest keyQuest;
    private Animator anim;
    public GameObject key;

    private AudioSource audioSource;
    public AudioClip chestSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        key.SetActive(false);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && keyQuest.questComplete == true)
        {
            anim.SetBool("isOpen", true);
            audioSource.PlayOneShot(chestSound);


            if (!PlayerPrefs.HasKey("GetStar"))
            {
                key.SetActive(true);
            }
        }
    }
}
