using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAction : MonoBehaviour
{
    private Animator anim;
    public bool startAction;
    public float timeStart;
    private float timeEnd;

    private void Start()
    {
        anim = GetComponent<Animator>();
        timeEnd = timeStart;
    }
    private void Update()
    {
        if(startAction == true)
        {
            timeStart -= Time.deltaTime;

            if(timeStart <= 0)
            {
                startAction = false;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Button"))
        {
            anim.SetBool("isAction", true);
            startAction = true;
            timeStart = timeEnd;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Button"))
        {
            anim.SetBool("isAction", false);
        }
    }
}
