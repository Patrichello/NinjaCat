using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashActive : MonoBehaviour
{
    public static bool dashActive;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dashActive = true;
            PlayerPrefs.SetInt("GetDashScript", 1);
            PlayerPrefs.Save();
        }
    }
}

