using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    PlayerMovement movementScript;

    Animator anim;

    readonly int speedFloat = Animator.StringToHash("Speed");
    readonly int verticalSpeedFloat = Animator.StringToHash("Vertical Speed");
    readonly int jumpTrigger = Animator.StringToHash("Jump");
    readonly int dodgeTrigger = Animator.StringToHash("Dodge");
    readonly int nextAttackInteger = Animator.StringToHash("Next Attack");
    readonly int hurtTrigger = Animator.StringToHash("Hurt");
    readonly int respawnTrigger = Animator.StringToHash("Respawn");
    readonly int isGroundedBool = Animator.StringToHash("isGrounded");
    readonly int isDeadBool = Animator.StringToHash("isDead");

    // Start is called before the first frame update
    void Start()
    {
        movementScript = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetBool(isGroundedBool, movementScript.isGrounded);
        anim.SetFloat(verticalSpeedFloat, movementScript.velocity.y);
    }

    //Functions that trigger animations
    public void PlayMovementAnimation(float directionMagnitude)
    {
        anim.SetFloat(speedFloat, directionMagnitude);
    }

    public void PlayLightAttack()
    {
        anim.SetInteger(nextAttackInteger, 1);
    }

    public void PlayHeavyAttack()
    {
        anim.SetInteger(nextAttackInteger, 2);
    }

    public void PlayNoCombo()
    {
        anim.SetInteger(nextAttackInteger, 0);
    }

    public void PlayJumpAnimation()
    {
        anim.SetTrigger(jumpTrigger);
    }

    public void PlayDodgeAnimation()
    {
        anim.SetTrigger(dodgeTrigger);
    }

    public void PlayHurtAnimation()
    {
        anim.SetTrigger(hurtTrigger);
    }

    public void PlayDeadAnimation()
    {
        anim.SetBool(isDeadBool, true);
    }

    public void PlayRespawnAnimation()
    {
        anim.SetBool(isDeadBool, false);
        anim.SetTrigger(respawnTrigger);
    }
}
