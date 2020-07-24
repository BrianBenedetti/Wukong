﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy_IdleState : StateMachineBehaviour
{
    FastEnemy baseScript;

    float distanceToTarget;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<FastEnemy>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //checks to chase, patrol, or attack (random between light and heavy)
        distanceToTarget = Vector3.Distance(baseScript.target.position, animator.transform.position);
        if (distanceToTarget <= baseScript.lookRadius && distanceToTarget > baseScript.agent.stoppingDistance)
        {
            animator.SetBool("isChasing", true); //immidiately switches, need to wait with exit time
        }
        else if (distanceToTarget <= baseScript.agent.stoppingDistance)
        {
            int rand = Random.Range(1, 3);
            if(rand == 1)
            {
                animator.SetTrigger("Light Attack");
            }
            else
            {
                animator.SetTrigger("Heavy Attack");
            }
        }
        else
        {
            animator.SetBool("isPatrolling", true); //immidiately switches, need to wait with exit time
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isIdle", false);
        animator.ResetTrigger("Light Attack");
        animator.ResetTrigger("Heavy Attack");
    }
}
