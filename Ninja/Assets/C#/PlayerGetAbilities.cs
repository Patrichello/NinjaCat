using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetAbilities : MonoBehaviour
{
    private PlayerDash playerDashScript;
    private PlayerCombatControl playerCombatControlScript;
    private Weapon weaponScript;
    private PlayerController playerControllerScript;

    void Start()
    {
        playerDashScript = GetComponent<PlayerDash>();
        playerCombatControlScript = GetComponent<PlayerCombatControl>();
        weaponScript = GetComponent<Weapon>();
        playerControllerScript = GetComponent<PlayerController>();

        if (!PlayerPrefs.HasKey("GetDash"))
        {
            playerDashScript.enabled = false;
        }
        if (!PlayerPrefs.HasKey("GetSword"))
        {
            playerCombatControlScript.enabled = false;
        }
        if (!PlayerPrefs.HasKey("GetShuriken"))
        {
            weaponScript.enabled = false;
        }
     
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DashSign"))
        {
            playerDashScript.enabled = true;
            PlayerPrefs.SetInt("GetDash", 1);
            PlayerPrefs.Save();
        }
        if (collision.gameObject.CompareTag("SwordSign"))
        {
            playerCombatControlScript.enabled = true;
            PlayerPrefs.SetInt("GetSword", 1);
            PlayerPrefs.Save();
        }
        if (collision.gameObject.CompareTag("ShurikenSign"))
        {
            playerCombatControlScript.enabled = true;
            PlayerPrefs.SetInt("GetShuriken", 1);
            PlayerPrefs.Save();
        }
        if (collision.gameObject.CompareTag("DoubleJump"))
        {
            playerControllerScript.amountOfJumps = 3;
            PlayerPrefs.SetInt("GetDoubleJump", 1);
            PlayerPrefs.Save();

        }
    }
}
   