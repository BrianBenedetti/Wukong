using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy_SwipeState : StateMachineBehaviour
{
    TankEnemy baseScript;

    float distanceToTarget;

    Transform attackOrigin; //for now

    public float attackRadius; //for now

    public LayerMask whatIsEnemy; //for now

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<TankEnemy>();
        attackOrigin = baseScript.attackOrigin;
        baseScript.Attack(attackOrigin, attackRadius, whatIsEnemy); //for now
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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
