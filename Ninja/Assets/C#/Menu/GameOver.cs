using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public bool gameOver;

    public GameObject gameOverMenu;

    private PlayerController playerController;


    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    public void GameOverMenu()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("ExitGame");
    }
}
