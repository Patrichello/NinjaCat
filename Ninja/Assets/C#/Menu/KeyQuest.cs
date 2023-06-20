using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyQuest : MonoBehaviour
{
    public int keyAmount;
    public TMP_Text keyAmountDisplay;
    public Image keyImage;
    public int keyAmountLeft;
    public bool questComplete;

    private void Start()
    {
        keyImage.gameObject.SetActive(false);
    }
    void Update()
    {
        keyAmountDisplay.text = "Key Amount: " + keyAmount.ToString();

        if(keyAmount == 1)
        {
            keyImage.gameObject.SetActive(true);
            questComplete = true;
        }
    }
    
    public void GetKey()
    {
        keyAmount++;
    }
   
}
