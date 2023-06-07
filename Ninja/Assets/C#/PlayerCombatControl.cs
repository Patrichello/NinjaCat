using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatControl : MonoBehaviour
{
    private Animator anim;
    public float attackRate = 2f;
    public float nextAttackTime = 0f;

    public Transform attackPos;
    public LayerMask enemy;
    public float attackRange;
    public int attackDamage;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
             if (Input.GetKeyDown(KeyCode.M))
             {
                 Attack();
                nextAttackTime = Time.time + 1f / attackRate;
             }

        }

    }
    void Attack()
    {
        anim.SetTrigger("Attack");
        
        StartCoroutine(SwordAttack());
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPos == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    private IEnumerator SwordAttack()
    {
        yield return new WaitForSeconds(0.25f);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

    }
}
