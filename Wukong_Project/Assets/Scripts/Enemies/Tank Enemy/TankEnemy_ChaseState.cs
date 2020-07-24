using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy_ChaseState : StateMachineBehaviour
{
    TankEnemy baseScript;

    float distanceToTarget;
    float timer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<TankEnemy>();
        timer = 8;
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

        //check to patrol
        distanceToTarget = Vector3.Distance(baseScript.target.position, animator.transform.position);
        if (distanceToTarget > baseScript.lookRadius)
        {
            animator.SetBool("isPatrolling", true);
        }
        else if (distanceToTarget < baseScript.agent.stoppingDistance)
        {
            int random = Random.Range(1, 3);
            if(random == 1)
            {
                animator.SetTrigger("Swipe");
            }
            else
            {
                animator.SetTrigger("Slam");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isChasing", false);
        animator.ResetTrigger("Slam");
        animator.ResetTrigger("Swipe");
    }
}
