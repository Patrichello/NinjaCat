using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighScores : MonoBehaviour
{
    public Text[] highScoreText;
    HighScores highScoreManager;
   
    void Start()
    {
        for (int i = 0; i < highScoreText.Length; i++)
        {
            highScoreText[i].text = i + 1 + ". Fetching...";
        }

        highScoreManager = GetComponent<HighScores>();

         StartCoroutine("RefreshHighScore");

    }

    public void OnHighScoreDownloaded(HighScore[] highScoreList)
    {
        for (int i = 0; i < highScoreText.Length; i++)
        {
            highScoreText[i].text = i + 1 + ". ";
            if (highScoreList.Length > i)
            {
                highScoreText[i].text += highScoreList[i].username + " - " + highScoreList[i].score;
            }
        }

       
    }

    IEnumerator RefreshHighScore()
    {
        while (true)
        {
            highScoreManager.DownloadHighScore();
            yield return new WaitForSeconds(1);
        }
    }
}