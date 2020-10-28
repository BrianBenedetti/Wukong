using UnityEngine;

public class FastEnemy_PatrolState : StateMachineBehaviour
{
    FastEnemy baseScript;

    float distanceToTarget;

    readonly int ChaseBool = Animator.StringToHash("isChasing");
    readonly int PatrolBool = Animator.StringToHash("isPatrolling");

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<FastEnemy>();
        baseScript.agent.isStopped = false;
        baseScript.StartPatrol();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //checks to idle at end of patrol
        baseScript.CheckPatrol();

        //checks if player is in range to start chasing
        distanceToTarget = Vector3.Distance(baseScript.target.position, animator.transform.position);
        if (distanceToTarget < baseScript.lookRadius)
        {
            animator.SetBool(ChaseBool, true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(PatrolBool, false);
    }
}
