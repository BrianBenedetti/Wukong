using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Wukong_AirbornState : StateMachineBehaviour
{
    PlayerController baseScript;

    readonly int JumpTrigger = Animator.StringToHash("Jump");
    readonly int PrimaryAttackTrigger = Animator.StringToHash("PrimaryAttack");
    readonly int SecondaryAttackTrigger = Animator.StringToHash("SecondaryAttack");

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<PlayerController>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //constantly checks for input and moves the player accordingly
        baseScript.Move(baseScript.direction);

        //checks double jump
        if (baseScript.inputActions.PlayerControls.Jump.triggered && baseScript.canDoubleJump)
        {
            var gamepad = Gamepad.current;

            //if Left Shoulder is being held, return.
            if (gamepad != null)
            {
                if (gamepad.leftShoulder.isPressed)
                {
                    return;
                }
            }
            animator.SetTrigger(JumpTrigger);
            baseScript.canDoubleJump = false;
        }
        //checks light attack
        if (baseScript.inputActions.PlayerControls.PrimaryAttack.triggered)
        {
            var gamepad = Gamepad.current;

            //if Left Shoulder is being held, return.
            if (gamepad != null)
            {
                if (gamepad.leftShoulder.isPressed)
                {
                    return;
                }
            }
            animator.SetTrigger(PrimaryAttackTrigger);
        }
        //checks heavy attack
        if (baseScript.inputActions.PlayerControls.SecondaryAttack.triggered)
        {
            var gamepad = Gamepad.current;

            //if Left Shoulder is being held, return.
            if (gamepad != null)
            {
                if (gamepad.leftShoulder.isPressed)
                {
                    return;
                }
            }
            animator.SetTrigger(SecondaryAttackTrigger);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(JumpTrigger);
        animator.ResetTrigger(PrimaryAttackTrigger);
        animator.ResetTrigger(SecondaryAttackTrigger);
    }
}
