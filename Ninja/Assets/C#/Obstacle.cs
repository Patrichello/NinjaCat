using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private HealthController healthController;
    public int damage;

    private bool isPlayerOnObstacle;

    private void Start()
    {
        healthController = FindObjectOfType<HealthController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    HealthController health = collision.gameObject.GetComponent<HealthController>();
        //    if (health != null)
        //    {
        //        health.TakeDamage(damage);
        //    }
        //}
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnObstacle = true;
            healthController.TakeDamage(damage);
            StartCoroutine(RepeatDamage());
        }
    }

    private IEnumerator RepeatDamage()
    {
       while (isPlayerOnObstacle)
       {
           yield return new WaitForSeconds(3);

              if (isPlayerOnObstacle)
              {
                  healthController.TakeDamage(damage);

              }


       }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnObstacle = false;
        }
    }

}
