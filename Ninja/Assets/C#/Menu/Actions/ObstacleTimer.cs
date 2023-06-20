using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTimer : MonoBehaviour
{
    public ButtonAction buttonAction;
    private Animator anim;
    private bool actionAnim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(buttonAction.startAction == true)
        {
            anim.SetBool("isActive", false);
        }
        else
        {
            anim.SetBool("isActive", true);
        }

    }
   
}
