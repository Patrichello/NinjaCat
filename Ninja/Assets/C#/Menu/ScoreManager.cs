using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public int currentScore;
    public TMP_Text scoreDisplay;

    public TMP_Text nameDisplay;
    public string playerNN;

    private void Start()
    {
        LoadScore();
        UpdateScoreDisplay();
    }

    void Update()
    {
        scoreDisplay.text = "Score: " + score.ToString();
        nameDisplay.text = "Name: " + playerNN;
    }

    public void Kill()
    {
        score += 10;
        UpdateScoreDisplay();
        SaveScore();

        if (currentScore < score)
        {
            currentScore = score;
            SaveCurrentScore();
        }
    }

    public void GetCoin()
    {
        score++;
        UpdateScoreDisplay();
        SaveScore();

        if (currentScore < score)
        {
            currentScore = score;
            SaveCurrentScore();
        }
    }

    private void UpdateScoreDisplay()
    {
        scoreDisplay.text = "Score: " + score.ToString();
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("scoreKey", score);
        PlayerPrefs.Save();
    }

    public void SaveCurrentScore()
    {
        PlayerPrefs.SetInt("currentScore", currentScore);
        PlayerPrefs.Save();
    }

    public void LoadScore()
    {
        score = PlayerPrefs.HasKey("scoreKey") ? PlayerPrefs.GetInt("scoreKey") : 0;
        playerNN = PlayerPrefs.HasKey("playerNameKey") ? PlayerPrefs.GetString("playerNameKey") : "";

        if (PlayerPrefs.HasKey("currentScore"))
        {
            currentScore = PlayerPrefs.GetInt("currentScore");
        }
        else
        {
            currentScore = score;
            SaveCurrentScore();
        }
    }
}
