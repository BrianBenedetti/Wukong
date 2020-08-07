using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AverageEnemy_SlamState : StateMachineBehaviour
{
    AverageEnemy baseScript;

    float distanceToTarget;
    public float attackRadius; //for now

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<AverageEnemy>();
        baseScript.Attack(animator.transform, attackRadius, baseScript.whatIsEnemy, baseScript.slamDamage); //for now
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distanceToTarget = Vector3.Distance(baseScript.target.position, animator.transform.position);
        if (distanceToTarget < baseScript.lookRadius && distanceToTarget > baseScript.agent.stoppingDistance)
        {
            animator.SetBool("isChasing", true);
        }
        else if(distanceToTarget < baseScript.retreatDistance)
        {
            animator.SetBool("isRetreating", true);
        }
        else if(distanceToTarget > baseScript.retreatDistance && distanceToTarget < baseScript.agent.stoppingDistance)
        {
            animator.SetBool("isShooting", true);
        }
    }
}
