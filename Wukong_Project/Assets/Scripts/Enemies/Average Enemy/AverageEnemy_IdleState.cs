using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AverageEnemy_IdleState : StateMachineBehaviour
{
    AverageEnemy baseScript;

    float distanceToTarget;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<AverageEnemy>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distanceToTarget = Vector3.Distance(baseScript.target.position, animator.transform.position);
        if(distanceToTarget >= baseScript.retreatDistance && distanceToTarget <= baseScript.agent.stoppingDistance)
        {
            animator.SetBool("isShooting", true);
        }else if (distanceToTarget < baseScript.retreatDistance)
        {
            int random = Random.Range(1, 3);
            if(random == 1)
            {
                animator.SetTrigger("Slam");
            }
            else
            {
                animator.SetBool("isRetreating", true);
            }
        }else if(distanceToTarget > baseScript.agent.stoppingDistance)
        {
            animator.SetBool("isChasing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isIdle", false);
        animator.ResetTrigger("Slam");
    }
}
