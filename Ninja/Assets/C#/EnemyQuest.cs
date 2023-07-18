using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyQuest : MonoBehaviour
{
    public Enemy enemy;
    public bool enemyQuestDone;

    public GameObject finish;
    public GameObject textFinish;

    private bool finishActive;

    void Start()
    {
        finish.SetActive(false);
        textFinish.SetActive(false);
    }

    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.E) && finishActive == true)
        {
            finish.SetActive(true);
        }
        enemyQuestDone = enemy.isDead;
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && enemyQuestDone == true)
        {
            finishActive = true;
            textFinish.SetActive(true);
        }
    }
}
