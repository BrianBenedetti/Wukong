using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullDemonKing_LeapLandState : StateMachineBehaviour
{
    BullDemonKing baseScript;

    public float radius; //for now

    public LayerMask whatIsEnemy; //for now

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<BullDemonKing>();
        baseScript.agent.enabled = true;
        baseScript.Attack(animator.transform, radius, whatIsEnemy); //for now
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetBool("isChasing", true);
    }
}
