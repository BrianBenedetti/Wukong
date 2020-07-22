﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy_HurtState : StateMachineBehaviour
{
    FastEnemy baseScript;

    float distanceToTarget;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<FastEnemy>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Random.Range(1, 3) == 1)
        {
            animator.SetTrigger("Dodge");
        }
        else
        {
            animator.SetBool("isChasing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Hurt");
        animator.ResetTrigger("Dodge");
    }
}
