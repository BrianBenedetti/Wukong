using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullDemonKing_LightAttackState : StateMachineBehaviour
{
    BullDemonKing baseScript;

    Transform attackOrigin; //for now

    public float attackRadius; //for now

    public LayerMask whatIsEnemy; //for now

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<BullDemonKing>();
        attackOrigin = baseScript.attackPosition;
        baseScript.Attack(attackOrigin, attackRadius, whatIsEnemy); //for now
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isChasing", true);
    }
}
