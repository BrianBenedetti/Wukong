using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy_HeavyAttackState : StateMachineBehaviour
{
    FastEnemy baseScript;

    float distanceToTarget;

    public Vector3 attackPos; //for now
    public Vector3 attackRange; //for now

    public Quaternion rotation; //for now

    public LayerMask whatIsEnemy; //for now

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<FastEnemy>();
        baseScript.Attack(attackPos, attackRange, rotation, whatIsEnemy); //for now
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //always faces player
        baseScript.faceTarget();

        //checks if player is in range to start chasing
        distanceToTarget = Vector3.Distance(baseScript.myStats.target.position, animator.transform.position);
        if (distanceToTarget <= baseScript.myStats.lookRadius)
        {
            animator.SetBool("isChasing", true);
        }
        else
        {
            animator.SetBool("isIdle", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
