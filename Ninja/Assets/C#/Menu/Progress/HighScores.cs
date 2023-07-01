using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class HighScores : MonoBehaviour
{
    const string privateCode = "-TcJo33IlkiQXpYrglk5ug95ylwlOvxkO5OMSodjcxtg";
    const string publicCode = "6493cf978f40bb8fb05b46b8";
    const string webURL = "http://dreamlo.com/lb/";

    public HighScore[] highScoreList;
    static HighScores instance;
    DisplayHighScores highScoreDisplay;

   

    private void Awake()
    {
        instance = this;
        highScoreDisplay = GetComponent<DisplayHighScores>();
    }

    public static void AddNewHighScore(string username, int score)
    {
        instance.StartCoroutine(instance.UploadNewHighScore(username, score));
    }

    IEnumerator UploadNewHighScore(string username, int score)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            print("Upload Successful");
            yield return new WaitForSeconds(3f); //delay 3 sec
            DownloadHighScore();

        }
        else
        {
            print("Error uploading: " + www.error);
        }
    }

    public void DownloadHighScore()
    {
        StartCoroutine("DownloadHighScoreFromDataBase");
    }

    IEnumerator DownloadHighScoreFromDataBase()
    {
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighScore(www.text);
            // print(www.text);
            highScoreDisplay.OnHighScoreDownloaded(highScoreList);
        }
        else
        {
            print("Error downloading: " + www.error);
        }
    }

    void FormatHighScore(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        highScoreList = new HighScore[entries.Length];

        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);

            highScoreList[i] = new HighScore(username, score);
            print(highScoreList[i].username + ": " + highScoreList[i].score);
        }
    }
}

public struct HighScore
{
    public string username;
    public int score;

    public HighScore(string _username, int _score)
    {
        username = _username;
        score = _score;
    }
    
}