using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullDemonKing_LeapStartState : StateMachineBehaviour
{
    BullDemonKing baseScript;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<BullDemonKing>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript.FaceTarget(); 
    }
}
