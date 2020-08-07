using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy_HeavyAttackState : StateMachineBehaviour
{
    FastEnemy baseScript;

    float distanceToTarget;

    public float attackRadius; //for now

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<FastEnemy>();
        baseScript.Attack(baseScript.attackOrigin, attackRadius, baseScript.whatIsEnemy, baseScript.heavyAttackDamage); //for now
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //always faces player
        baseScript.FaceTarget();

        //checks if player is in range to start chasing
        distanceToTarget = Vector3.Distance(baseScript.target.position, animator.transform.position);
        if (distanceToTarget <= baseScript.lookRadius)
        {
            animator.SetBool("isChasing", true);
        }
        else
        {
            animator.SetBool("isIdle", true);
        }
    }
}
