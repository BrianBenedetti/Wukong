using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy_DieState : StateMachineBehaviour
{
    TankEnemy baseScript;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<TankEnemy>();
        baseScript.StartCoroutine("Die");
    }
}
