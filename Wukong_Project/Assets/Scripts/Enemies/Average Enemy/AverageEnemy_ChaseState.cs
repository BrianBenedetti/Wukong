using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AverageEnemy_ChaseState : StateMachineBehaviour
{
    AverageEnemy baseScript;

    float distanceToTarget;
    float timer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<AverageEnemy>();
        timer = 5;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript.ChaseTarget();

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            animator.SetBool("isIdle", true);
        }

        //check to patrol
        distanceToTarget = Vector3.Distance(baseScript.target.position, animator.transform.position);
        if (distanceToTarget > baseScript.lookRadius)
        {
            animator.SetBool("isPatrolling", true);
        }
        else if (distanceToTarget < baseScript.agent.stoppingDistance && distanceToTarget > baseScript.retreatDistance)
        {
            animator.SetBool("isShooting", true);
        }
        else if(distanceToTarget < baseScript.retreatDistance)
        {
            animator.SetBool("isRetreating", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isChasing", false);
    }
}
