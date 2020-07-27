﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullDemonKing_DieState : StateMachineBehaviour
{
    BullDemonKing baseScript;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<BullDemonKing>();
        baseScript.Die();
    }
}
