using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wukong_RespawnState : StateMachineBehaviour
{
    PlayerController baseScript;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<PlayerController>();

        baseScript.Respawn();
    }
}
