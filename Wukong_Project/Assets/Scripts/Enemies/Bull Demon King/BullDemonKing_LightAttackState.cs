﻿using UnityEngine;

public class BullDemonKing_LightAttackState : StateMachineBehaviour
{
    BullDemonKing baseScript;

    public float attackRadius; //for now

    readonly int ChaseBool = Animator.StringToHash("isChasing");

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<BullDemonKing>();
        baseScript.Attack(baseScript.attackPosition, attackRadius, baseScript.whatIsEnemy, baseScript.lightAttackDamage); //for now
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(ChaseBool, true);
    }
}
