using System.Collections;
using UnityEngine;

public class Wukong_DeathState : StateMachineBehaviour
{
    PlayerController baseScript;

    IEnumerator DieCoroutine;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<PlayerController>();

        DieCoroutine = baseScript.Die();
        baseScript.StartCoroutine(DieCoroutine);
    }
}
