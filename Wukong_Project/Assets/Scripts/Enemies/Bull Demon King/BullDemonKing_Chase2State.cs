using UnityEngine;

public class BullDemonKing_Chase2State : StateMachineBehaviour
{
    BullDemonKing baseScript;

    float distanceToTarget;
    float timer;

    int randomAction;
    readonly int ChaseBool = Animator.StringToHash("isChasing");
    readonly int LightTrigger = Animator.StringToHash("Light Attack");
    readonly int HeavyTrigger = Animator.StringToHash("Heavy Attack");
    readonly int SummonTrigger = Animator.StringToHash("Summon");
    readonly int LeapTrigger = Animator.StringToHash("Leap");

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<BullDemonKing>();
        timer = 10;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript.FaceTarget();
        baseScript.ChaseTarget();

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if(Random.Range(0, 11) <= 5)
            {
                animator.SetTrigger(SummonTrigger);
            }
            else
            {
                animator.SetTrigger(LeapTrigger);
            }
        }

        //check to attack
        distanceToTarget = Vector3.Distance(baseScript.target.position, animator.transform.position);
        if (distanceToTarget <= baseScript.agent.stoppingDistance)
        { //if enemy is in attack range
            randomAction = Random.Range(0, 11); //max has to be 1 more than actual max

            if (randomAction <= 5)
            {
                animator.SetTrigger(LightTrigger);
            }
            else
            {
                animator.SetTrigger(HeavyTrigger);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(ChaseBool, false);
        animator.ResetTrigger(LightTrigger);
        animator.ResetTrigger(HeavyTrigger);
        animator.ResetTrigger(LeapTrigger);
        animator.ResetTrigger(SummonTrigger);
    }
}
