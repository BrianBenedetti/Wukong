using UnityEngine;

public class AverageEnemy_IdleState : StateMachineBehaviour
{
    AverageEnemy baseScript;

    float distanceToTarget;

    readonly int ShootBool = Animator.StringToHash("isShooting");
    readonly int RetreatBool = Animator.StringToHash("isRetreating");
    readonly int ChaseBool = Animator.StringToHash("isChasing");
    readonly int IdleBool = Animator.StringToHash("isIdle");
    readonly int SlamTrigger = Animator.StringToHash("Slam");

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<AverageEnemy>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distanceToTarget = Vector3.Distance(baseScript.target.position, animator.transform.position);
        if(distanceToTarget >= baseScript.retreatDistance && distanceToTarget <= baseScript.agent.stoppingDistance)
        {
            animator.SetBool(ShootBool, true);
<<<<<<< HEAD
        }else if (distanceToTarget < baseScript.retreatDistance)
=======
        }
        else if (distanceToTarget < baseScript.retreatDistance)
>>>>>>> 64429ae0c6f70c4ba99a9f0d1069938577b46d9a
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
<<<<<<< HEAD
        }else if(distanceToTarget > baseScript.agent.stoppingDistance)
=======
        }
        else if(distanceToTarget > baseScript.agent.stoppingDistance)
>>>>>>> 64429ae0c6f70c4ba99a9f0d1069938577b46d9a
        {
            animator.SetBool(ChaseBool, true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(IdleBool, false);
        animator.ResetTrigger(SlamTrigger);
    }
}
