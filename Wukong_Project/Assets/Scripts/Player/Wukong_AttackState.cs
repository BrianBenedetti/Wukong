using UnityEngine;
using UnityEngine.InputSystem;

public class Wukong_AttackState : StateMachineBehaviour
{
    PlayerController baseScript;

    readonly int PrimaryAttackTrigger = Animator.StringToHash("PrimaryAttack");
    readonly int SecondaryAttackTrigger = Animator.StringToHash("SecondaryAttack");

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseScript = animator.GetComponent<PlayerController>();

        baseScript.rb.useGravity = false;

        //for now, this will be on the actual animation
        baseScript.Attack(baseScript.attackPoint, baseScript.attackRange, baseScript.attackPoint.rotation, baseScript.enemyMask, baseScript.primaryAttackDamage);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //checks if next attack is light
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
        //checks if next attack is heavy
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
        animator.ResetTrigger(PrimaryAttackTrigger);
        animator.ResetTrigger(SecondaryAttackTrigger);
        baseScript.rb.useGravity = true;
    }
}
