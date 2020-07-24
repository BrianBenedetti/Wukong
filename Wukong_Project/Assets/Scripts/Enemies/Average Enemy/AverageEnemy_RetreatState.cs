using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AverageEnemy_RetreatState : StateMachineBehaviour
{
    AverageEnemy baseScript;

    float distanceToTarget;
    float timer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<AverageEnemy>();
        baseScript.agent.updateRotation = false;
        timer = 5;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript.Retreat();
        baseScript.FaceTarget();

        timer -= Time.deltaTime;
        //if retreat for too long, do slam
        if (timer <= 0)
        {
            animator.SetTrigger("Slam");
        }

        distanceToTarget = Vector3.Distance(baseScript.target.position, animator.transform.position);
        if (distanceToTarget > baseScript.retreatDistance && distanceToTarget < baseScript.agent.stoppingDistance)
        { //if reached safe distance, shoot
            animator.SetBool("isShooting", true);
        }
        else if(distanceToTarget > baseScript.agent.stoppingDistance)
        { //if enemy too far, chase
            animator.SetBool("isChasing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isRetreating", false);
        animator.ResetTrigger("Slam");
        baseScript.agent.updateRotation = true;
    }
}
