using UnityEngine;

public class TankEnemy_SlamState : StateMachineBehaviour
{
    TankEnemy baseScript;

    float distanceToTarget;

    readonly int ChaseBool = Animator.StringToHash("isChasing");
    readonly int IdleBool = Animator.StringToHash("isIdle");

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<TankEnemy>();
        baseScript.agent.isStopped = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //checks if player is in range to start chasing
        distanceToTarget = Vector3.Distance(baseScript.target.position, animator.transform.position);
        if (distanceToTarget <= baseScript.lookRadius)
        {
            animator.SetBool(ChaseBool, true);
        }
        else
        {
            animator.SetBool(IdleBool, true);
        }
    }
}
