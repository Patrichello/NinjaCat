using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    public GameObject[] coins;
    private List<int> collectedCoins;

    private void Start()
    {
        collectedCoins = new List<int>();

        if (PlayerPrefs.HasKey("collectedCoins"))
        {
            LoadProgress();
        }
    }

    private void LoadProgress()
    {
        string[] collectedCoinIndexes = PlayerPrefs.GetString("collectedCoins").Split(',');

        foreach (string coinIndex in collectedCoinIndexes)
        {
            int index = int.Parse(coinIndex);
            collectedCoins.Add(index);

            if (index >= 0 && index < coins.Length)
            {
                coins[index].SetActive(false);
            }
        }
    }

    private void SaveProgress()
    {
        string collectedCoinIndexes = string.Join(",", collectedCoins.ToArray());
        PlayerPrefs.SetString("collectedCoins", collectedCoinIndexes);
        PlayerPrefs.Save();
    }

    public void CollectCoin(int coinIndex)
    {
        if (coinIndex >= 0 && coinIndex < coins.Length && !collectedCoins.Contains(coinIndex))
        {
            coins[coinIndex].SetActive(false);
            collectedCoins.Add(coinIndex);
            SaveProgress();
        }
    }

    public void ResetCollectedCoins()
    {
        PlayerPrefs.DeleteKey("collectedCoins");
    }

    public void ActivateAllCoins()
    {
        foreach (GameObject coin in coins)
        {
            coin.SetActive(true);
        }
    }
}
