using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject shurikenPrefab;
    private Animator anim;
    public PlayerController playerController;
    private HealthController healthController;
    private PlayerDash playerDash;
    public float attackRate = 2f;
    public float nextAttackTime = 0f;

    private bool canAttackShuriken = true;


    void Start()
    {
        anim = GetComponent<Animator>();
        healthController = GetComponent<HealthController>();
        playerDash = GetComponent<PlayerDash>();
    }
    void Update()
    {
        if (CanAttackShuriken())
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
    }

    private bool CanAttackShuriken()
    {
        return canAttackShuriken && !healthController.playerDead && !playerDash.isDashing;
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
