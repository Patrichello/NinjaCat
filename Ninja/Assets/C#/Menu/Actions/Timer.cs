using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeStart;
    public TMP_Text timerText;

    private void Start()
    {
        UpdateTimerText();
      //  timerText.text = timeStart.ToString();
    }
    private void Update()
    {
        timeStart += Time.deltaTime;
        UpdateTimerText();
       // timerText.text = Mathf.Round(timeStart).ToString();
    }
    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeStart / 60);
        int seconds = Mathf.FloorToInt(timeStart % 60);

        string timeString = string.Format("{0}:{1:00}", minutes, seconds);

        timerText.text = timeString;
    }
}
