using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameProgress gameProgress;
    private ScoreManager scoreManager;

    void Start()
    {
        gameProgress = GameObject.FindObjectOfType<GameProgress>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int coinIndex = GetCoinIndex();
            gameProgress.CollectCoin(coinIndex);

            scoreManager.GetCoin();
            gameObject.SetActive(false);
        }
    }

    private int GetCoinIndex()
    {
        for (int i = 0; i < gameProgress.coins.Length; i++)
        {
            if (gameObject == gameProgress.coins[i])
            {
                return i;
            }
        }

        return -1;
    }
}
