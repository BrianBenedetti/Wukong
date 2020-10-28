using UnityEngine;

public class TankEnemy_ChaseState : StateMachineBehaviour
{
    TankEnemy baseScript;

    float distanceToTarget;
    float timer;

    readonly int ChaseBool = Animator.StringToHash("isChasing");
    readonly int PatrolBool = Animator.StringToHash("isPatrolling");
    readonly int IdleBool = Animator.StringToHash("isIdle");
    readonly int SlamTrigger = Animator.StringToHash("Slam");
    readonly int SwipeTrigger = Animator.StringToHash("Swipe");

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<TankEnemy>();
        baseScript.agent.isStopped = false;
        baseScript.agent.ResetPath();
        timer = 8;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript.FaceTarget();
        baseScript.ChaseTarget();

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            animator.SetBool(IdleBool, true);
        }

        //check to patrol
        distanceToTarget = Vector3.Distance(baseScript.target.position, animator.transform.position);
        if (distanceToTarget > baseScript.lookRadius)
        {
            animator.SetBool(PatrolBool, true);
        }
        else if (distanceToTarget < 4)
        {
            int random = Random.Range(0, 11);
            if(random <= 5)
            {
                animator.SetTrigger(SwipeTrigger);
            }
            else
            {
                animator.SetTrigger(SlamTrigger);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(ChaseBool, false);
        animator.ResetTrigger(SlamTrigger);
        animator.ResetTrigger(SwipeTrigger);
    }
}
