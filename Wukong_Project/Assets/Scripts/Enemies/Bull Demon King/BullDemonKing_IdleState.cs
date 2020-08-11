using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullDemonKing_IdleState : StateMachineBehaviour
{
    BullDemonKing baseScript;

    float distanceToTarget;

    readonly int ChaseBool = Animator.StringToHash("isChasing");
    readonly int IdleBool = Animator.StringToHash("isIdle");
    readonly int LightTrigger = Animator.StringToHash("Light Attack");
    readonly int HeavyTrigger = Animator.StringToHash("Heavy Attack");

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<BullDemonKing>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //checks to chase, patrol, or attack (random between light and heavy)
        distanceToTarget = Vector3.Distance(baseScript.target.position, animator.transform.position);
        if (distanceToTarget > baseScript.agent.stoppingDistance)
        {
            animator.SetBool(ChaseBool, true); //immidiately switches, need to wait with exit time
        }
        else if (distanceToTarget <= baseScript.agent.stoppingDistance)
        {
            int rand = Random.Range(1, 6);
            if (rand <= 3)
            {
                animator.SetTrigger(LightTrigger);
            }
            else
            {
                animator.SetTrigger(HeavyTrigger);
            }
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(IdleBool, false);
        animator.ResetTrigger(LightTrigger);
        animator.ResetTrigger(HeavyTrigger);
    }
}
