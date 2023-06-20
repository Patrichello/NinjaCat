using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderScript : MonoBehaviour
{
    public string nextSceneName;

    private void Start()
    {
        LoadNextScene();
    }
    public void LoadNextScene()
    {
        
        PlayerPrefs.SetInt("currentScene", SceneManager.GetActiveScene().buildIndex);

        SceneManager.LoadScene(nextSceneName);
    }
}
