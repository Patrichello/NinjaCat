using UnityEngine;

public class Star : MonoBehaviour
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
            PlayerPrefs.SetInt("GetStar", 1);
            PlayerPrefs.Save();

            int coinIndex = GetStarIndex();
            gameProgress.CollectCoin(coinIndex);

            scoreManager.GetStar();
            Destroy(gameObject);
        }
    }

    private int GetStarIndex()
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
