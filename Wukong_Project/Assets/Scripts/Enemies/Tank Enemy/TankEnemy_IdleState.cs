using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy_IdleState : StateMachineBehaviour
{
    TankEnemy baseScript;

    float distanceToTarget;

    readonly int ChaseBool = Animator.StringToHash("isChasing");
    readonly int PatrolBool = Animator.StringToHash("isPatrolling");
    readonly int IdleBool = Animator.StringToHash("isIdle");
    readonly int SlamTrigger = Animator.StringToHash("Slam");
    readonly int SwipeTrigger = Animator.StringToHash("Swipe");

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<TankEnemy>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //checks to chase, patrol, or attack (random between light and heavy)
        distanceToTarget = Vector3.Distance(baseScript.target.position, animator.transform.position);
        if (distanceToTarget < baseScript.lookRadius && distanceToTarget > baseScript.agent.stoppingDistance)
        {
            animator.SetBool(ChaseBool, true); //immidiately switches, need to wait with exit time
        }
        else if (distanceToTarget < baseScript.agent.stoppingDistance)
        {
            int rand = Random.Range(0, 11);
            if (rand <= 5)
            {
                animator.SetTrigger(SwipeTrigger);
            }
            else
            {
                animator.SetTrigger(SlamTrigger);
            }
        }
        else
        {
            animator.SetBool(PatrolBool, true); //immidiately switches, need to wait with exit time
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(IdleBool, false);
        animator.ResetTrigger(SwipeTrigger);
        animator.ResetTrigger(SlamTrigger);
    }
}
