using System.Collections;
using UnityEngine;

public class FastEnemy_DieState : StateMachineBehaviour
{
    FastEnemy baseScript;

    IEnumerator DieCoroutine;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<FastEnemy>();
        DieCoroutine = baseScript.Die();
        baseScript.StartCoroutine(DieCoroutine);
    }
}
