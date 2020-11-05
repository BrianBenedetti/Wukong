using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    PlayerMovement movementScript;

    [HideInInspector] public Animator anim;

    public readonly int speedFloat = Animator.StringToHash("Speed");
    public readonly int verticalSpeedFloat = Animator.StringToHash("Vertical Speed");
    public readonly int jumpTrigger = Animator.StringToHash("Jump");
    public readonly int dodgeTrigger = Animator.StringToHash("Dodge");
    public readonly int hurtTrigger = Animator.StringToHash("Hurt");
    public readonly int respawnTrigger = Animator.StringToHash("Respawn");
    public readonly int specialAttackTrigger = Animator.StringToHash("Special Attack");
    public readonly int rageTrigger = Animator.StringToHash("Rage");
    public readonly int isGroundedBool = Animator.StringToHash("isGrounded");
    public readonly int isDeadBool = Animator.StringToHash("isDead");
    public readonly int lightAttack1Bool = Animator.StringToHash("Light Attack 1");
    public readonly int lightAttack2Bool = Animator.StringToHash("Light Attack 2");
    public readonly int lightAttack3Bool = Animator.StringToHash("Light Attack 3");
    public readonly int heavyAttack1Bool = Animator.StringToHash("Heavy Attack 1");
    public readonly int heavyAttack2Bool = Animator.StringToHash("Heavy Attack 2");
    public readonly int heavyAttack3Bool = Animator.StringToHash("Heavy Attack 3");
    public readonly int nimbusBool = Animator.StringToHash("Nimbus");

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

    public void SetAnimationBool(int boolToChange, bool value)
    {
        anim.SetBool(boolToChange, value);
    }

    public void ResetTrigger(int triggerToReset)
    {
        anim.ResetTrigger(triggerToReset);
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

    public void PlaySpecialAttackAnimation()
    {
        anim.SetTrigger(specialAttackTrigger);
    }

    public void PlayRageAnimation()
    {
        anim.SetTrigger(rageTrigger);
    }
}
