using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullDemonKing_LeapAirbornState : StateMachineBehaviour
{
    BullDemonKing baseScript;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<BullDemonKing>();

        baseScript.agent.enabled = false;

        baseScript.leapStart = new Vector3(animator.transform.position.x, animator.transform.position.y, animator.transform.position.z);
        baseScript.leapDestination = new Vector3(baseScript.target.position.x, baseScript.leapStart.y, baseScript.target.position.z);

        baseScript.StartCoroutine("LeapAttack");
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Land");
        baseScript.StopCoroutine("LeapAttack");
    }
}
