using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager  Instance;

    public string playerName;
    public int score;

    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("GameManager");
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
