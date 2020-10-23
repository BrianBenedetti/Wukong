using UnityEngine;

public class AverageEnemy_SlamState : StateMachineBehaviour
{
    AverageEnemy baseScript;

    float distanceToTarget;

    readonly int ShootBool = Animator.StringToHash("isShooting");
    readonly int RetreatBool = Animator.StringToHash("isRetreating");
    readonly int ChaseBool = Animator.StringToHash("isChasing");

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<AverageEnemy>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distanceToTarget = Vector3.Distance(baseScript.target.position, animator.transform.position);
        if (distanceToTarget < baseScript.lookRadius && distanceToTarget > baseScript.agent.stoppingDistance)
        {
            animator.SetBool(ChaseBool, true);
        }
        else if(distanceToTarget < baseScript.retreatDistance)
        {
            animator.SetBool(RetreatBool, true);
        }
        else if(distanceToTarget > baseScript.retreatDistance && distanceToTarget < baseScript.agent.stoppingDistance)
        {
            animator.SetBool(ShootBool, true);
        }
    }
}
