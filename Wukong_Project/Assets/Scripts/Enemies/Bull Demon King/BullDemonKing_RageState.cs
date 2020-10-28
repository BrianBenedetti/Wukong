﻿using UnityEngine;

public class BullDemonKing_RageState : StateMachineBehaviour
{ 
    BullDemonKing baseScript;

    readonly int ChaseBool = Animator.StringToHash("isChasing");

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<BullDemonKing>();
        baseScript.isVulnerable = false;
        baseScript.Rage();
        animator.SetBool(ChaseBool, true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript.isVulnerable = true;
    }
}
