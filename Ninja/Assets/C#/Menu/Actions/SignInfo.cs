using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignInfo : MonoBehaviour
{
    public GameObject signTextInfo;

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            signTextInfo.SetActive(true);
           
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            signTextInfo.SetActive(false);

           
        }
    }
}
