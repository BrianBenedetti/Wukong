using UnityEngine;

public class AverageEnemy_IdleState : StateMachineBehaviour
{
    AverageEnemy baseScript;

    float distanceToTarget;

    readonly int ShootBool = Animator.StringToHash("isShooting");
    readonly int PatrolBool = Animator.StringToHash("isPatrolling");
    readonly int RetreatBool = Animator.StringToHash("isRetreating");
    readonly int ChaseBool = Animator.StringToHash("isChasing");
    readonly int IdleBool = Animator.StringToHash("isIdle");
    readonly int SlamTrigger = Animator.StringToHash("Slam");

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<AverageEnemy>();
        baseScript.agent.isStopped = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distanceToTarget = Vector3.Distance(baseScript.target.position, animator.transform.position);
        if(distanceToTarget >= baseScript.retreatDistance && distanceToTarget <= 10)
        {
            animator.SetBool(ShootBool, true);
        }
        else if (distanceToTarget < baseScript.retreatDistance)
        {
            int random = Random.Range(0, 11);
            if(random <= 5)
            {
                animator.SetTrigger(SlamTrigger);
            }
            else
            {
                animator.SetBool(RetreatBool, true);
            }
        }
        else if(distanceToTarget < baseScript.lookRadius && distanceToTarget > 10)
        {
            animator.SetBool(ChaseBool, true);
        }
        else if(distanceToTarget > baseScript.lookRadius)
        {
            animator.SetBool(PatrolBool, true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(IdleBool, false);
        animator.ResetTrigger(SlamTrigger);
    }
}
