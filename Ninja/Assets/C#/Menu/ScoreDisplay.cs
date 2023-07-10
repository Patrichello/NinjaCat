using UnityEngine;
using TMPro;


public class ScoreDisplay : MonoBehaviour
{
    public TMP_Text scoreText;

    public void UpdateScoreDisplay(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
