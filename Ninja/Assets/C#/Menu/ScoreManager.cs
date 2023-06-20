using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public TMP_Text scoreDisplay;
    void Update()
    {
        scoreDisplay.text = "Score: " + score.ToString();
    }
    public void Kill()
    {
        score += 10;
    }
    public void GetCoin()
    {
        score++;
    }
}
