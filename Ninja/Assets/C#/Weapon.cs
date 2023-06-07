using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject shurikenPrefab;
    private Animator anim;
    public PlayerController playerController;

    public float attackRate = 2f;
    public float nextAttackTime = 0f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.L) && playerController.canJump)
            {
                Shoot();
                nextAttackTime = Time.time + 1f / attackRate;
            }
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
