using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullDemonKing_SummonState : StateMachineBehaviour
{
    BullDemonKing baseScript;

    readonly int ChaseBool = Animator.StringToHash("isChasing");

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<BullDemonKing>();
        baseScript.SummonMinions();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetBool(ChaseBool, true);
    }
}
