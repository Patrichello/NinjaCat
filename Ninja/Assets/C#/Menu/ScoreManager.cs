using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public int currentScore;

    private ScoreDisplay scoreDisplay;
    
    public TMP_Text nameDisplay;
    public string playerNN;

    private AudioSource audioSource;
    public AudioClip coinSound;
    public AudioClip starSound;

    private void Start()
    {
        scoreDisplay = FindObjectOfType<ScoreDisplay>();
        audioSource = GetComponent<AudioSource>();

        LoadScore();
        UpdateScoreDisplay();
    }

    void Update()
    {
        nameDisplay.text = "Name: " + playerNN;
    }

    public void Kill()
    {
        IncreaseScore(10);
    }

    public void GetCoin()
    {
        audioSource.PlayOneShot(coinSound);
        IncreaseScore(1);
    }

    public void GetStar()
    {
        audioSource.PlayOneShot(starSound);
        IncreaseScore(50);
    }

    private void IncreaseScore(int amount)
    {
        score += amount;
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
        scoreDisplay.UpdateScoreDisplay(score);
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
