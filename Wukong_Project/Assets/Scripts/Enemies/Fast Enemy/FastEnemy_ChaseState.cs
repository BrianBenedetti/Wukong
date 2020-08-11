using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy_ChaseState : StateMachineBehaviour
{
    FastEnemy baseScript;

    float distanceToTarget;
    float timer;

    int randomAction;
    readonly int ChaseBool = Animator.StringToHash("isChasing");
    readonly int PatrolBool = Animator.StringToHash("isPatrolling");
    readonly int IdleBool = Animator.StringToHash("isIdle");
    readonly int LightTrigger = Animator.StringToHash("Light Attack");
    readonly int HeavyTrigger = Animator.StringToHash("Heavy Attack");

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
                animator.SetTrigger(HeavyTrigger);
            }
            else
            {
                animator.SetBool(IdleBool, true);
            }
        }

        //check to patrol or attack
        distanceToTarget = Vector3.Distance(baseScript.target.position, animator.transform.position);
        if (distanceToTarget > baseScript.lookRadius)
        {
            animator.SetBool(PatrolBool, true);
        }
        else if(distanceToTarget <= baseScript.agent.stoppingDistance)
        {
            animator.SetTrigger(LightTrigger);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(ChaseBool, false);
        animator.ResetTrigger(LightTrigger);
        animator.ResetTrigger(HeavyTrigger);
    }
}
