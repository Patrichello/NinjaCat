using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject shurikenPrefab;
    public Animator anim;
    public PlayerController playerController;
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && playerController.canJump)
        {
            Shoot();
        } 
    }

    void Shoot()
    {
      
        anim.SetTrigger("AttackShuriken");
        StartCoroutine(ShurikenAttackAnim());
        
        
    }
    private IEnumerator ShurikenAttackAnim()
    {
        yield return new WaitForSeconds(0.25f);
        Instantiate(shurikenPrefab, firePoint.position, firePoint.rotation);

    }
}
