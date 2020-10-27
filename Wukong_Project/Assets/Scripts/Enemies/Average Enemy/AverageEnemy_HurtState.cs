﻿using UnityEngine;

public class AverageEnemy_HurtState : StateMachineBehaviour
{
    readonly int RetreatBool = Animator.StringToHash("isRetreating");
    readonly int SlamTrigger = Animator.StringToHash("Slam");
    readonly int HurtTrigger = Animator.StringToHash("Hurt");

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(RetreatBool, true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(HurtTrigger);
    }
}
