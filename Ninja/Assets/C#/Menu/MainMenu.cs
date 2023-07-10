using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private PlayerName playerName;

    public bool canStart = false;

    private void Start()
    {
        playerName = FindObjectOfType<PlayerName>();
    }

    public void NewPlayGame()
    {
        if (playerName.nameInput == true && canStart == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            ResetPlayerPrefs();
        }
    }

    private void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteKey("scoreKey");
        PlayerPrefs.DeleteKey("currentScene");
        PlayerPrefs.DeleteKey("currentScore");
        PlayerPrefs.SetString("playerNameKey", playerName.playerName);
        PlayerPrefs.DeleteKey("collectedCoins");
    }

    public void ConfirmInput()
    {
        canStart = true;

    }
    public void ContinueGame()
    {
        if (PlayerPrefs.HasKey("playerNameKey"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void ExitGame()
    {
        Debug.Log("ExitGame");
        Application.Quit();
    }

    public void CheckScore()
    {
        var highScores = new HighScores();
        string playerName = PlayerPrefs.GetString("playerNameKey");
        int score = PlayerPrefs.GetInt("currentScore");
       // HighScores.AddNewHighScore(playerName, score);
        highScores.AddNewHighScore(playerName, score);

    }
}
