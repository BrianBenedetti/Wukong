using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullDemonKing_ChaseState : StateMachineBehaviour
{
    BullDemonKing baseScript;

    float distanceToTarget;
    float timer;
    int randomAction;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<BullDemonKing>();
        timer = 6;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript.FaceTarget();
        baseScript.ChaseTarget();

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            animator.SetBool("isIdle", true);
        }

        //check to attack
        distanceToTarget = Vector3.Distance(baseScript.target.position, animator.transform.position);
        if (distanceToTarget <= baseScript.agent.stoppingDistance)
        { //if enemy is in attack range
            randomAction = Random.Range(1, 6); //max has to be 1 more than actual max

            if(randomAction <= 3)
            {
                animator.SetTrigger("Light Attack");
            }
            else
            {
                animator.SetTrigger("Heavy Attack");
            }
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
