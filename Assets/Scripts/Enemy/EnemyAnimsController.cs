using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimsController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool isRunning = animator.GetBool("isRunning");// recognises running animation from the player animation controller

        if (GameObject.Find("Enemy").GetComponent<AIController>().playerInAttackRange == false)
            // if the player isn't in attack range, then the running animation plays. This is played during patroling and chasing states.
        {
            animator.SetBool("isRunning", true);
        }

        if (GameObject.Find("Enemy").GetComponent<AIController>().playerInAttackRange == true)
        // if the player isn in attack range, then the running animation stops. Theoretically, the enemy attacks.
        {
            animator.SetBool("isRunning", false);
        }
    }
}
