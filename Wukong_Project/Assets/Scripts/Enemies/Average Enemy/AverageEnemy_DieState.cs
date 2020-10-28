using System.Collections;
using UnityEngine;

public class AverageEnemy_DieState : StateMachineBehaviour
{
    AverageEnemy baseScript;

    IEnumerator DieCoroutine;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<AverageEnemy>();
        baseScript.agent.isStopped = true;
        DieCoroutine = baseScript.Die();
        baseScript.StartCoroutine(DieCoroutine);
    }
}
