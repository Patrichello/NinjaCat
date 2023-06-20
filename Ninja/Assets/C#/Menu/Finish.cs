using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public string nextSceneName;

    public GameObject fadeOut;
    public Animator animator;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Finish");
            fadeOut.SetActive(true);
            StartCoroutine(ChangeScene());
        }
    }
    private IEnumerator ChangeScene()
    {

        yield return new WaitForSeconds(0.8f);

            PlayerPrefs.SetInt("currentScene", SceneManager.GetActiveScene().buildIndex);
            if (nextSceneName != null)
            SceneManager.LoadScene(nextSceneName);
       
    }
}
