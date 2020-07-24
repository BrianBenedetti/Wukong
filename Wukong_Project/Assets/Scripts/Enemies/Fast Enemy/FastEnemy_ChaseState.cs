using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy_ChaseState : StateMachineBehaviour
{
    FastEnemy baseScript;

    float distanceToTarget;
    float timer;
    int randomAction;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<FastEnemy>();
        timer = 10;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript.ChaseTarget();

        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            //choose something at random
            randomAction = Random.Range(1, 5); //max has to be 1 more than actual max

            if(randomAction == 1)
            {
                animator.SetTrigger("Heavy Attack");
            }
            else
            {
                animator.SetBool("isIdle", true);
            }
        }

        //check to patrol or attack
        distanceToTarget = Vector3.Distance(baseScript.target.position, animator.transform.position);
        if (distanceToTarget > baseScript.lookRadius)
        {
            animator.SetBool("isPatrolling", true);
        }else if(distanceToTarget <= baseScript.agent.stoppingDistance){ //if enemy is in attack range
            animator.SetTrigger("Light Attack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isChasing", false);
        animator.ResetTrigger("Light Attack");
        animator.ResetTrigger("Heavy Attack");
    }
}
