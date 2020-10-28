﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialAttacks : MonoBehaviour
{
    PlayerInputActions inputActions;

    PlayerElementalForms elementalFormScript;
    PlayerAnimations animationsScript;

    PlayerCombat combatScript;

    PlayerMovement movementScript;

    public GameObject fireSpecialAttack;
    public GameObject waterSpecialAttack;
    public GameObject airSpecialAttack;

    private void Awake()
    {
        inputActions = new PlayerInputActions();

        elementalFormScript = GetComponent<PlayerElementalForms>();
        combatScript = GetComponent<PlayerCombat>();
        movementScript = GetComponent<PlayerMovement>();
        animationsScript = GetComponent<PlayerAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputActions.PlayerControls.SpecialAttack.triggered && movementScript.isGrounded && combatScript.currentMana == combatScript.maxMana)
        {
            switch (elementalFormScript.currentElement)
            {
                case PlayerElementalForms.ElementalForms.fire:
                    animationsScript.PlaySpecialAttackAnimation();
                    Instantiate(fireSpecialAttack, transform.position, Quaternion.identity);
                    //combatScript.currentMana = 0;
                    //combatScript.manaBar.SetValue(combatScript.currentMana);
                    break;

                case PlayerElementalForms.ElementalForms.water:
                    animationsScript.PlaySpecialAttackAnimation();
                    Instantiate(waterSpecialAttack, transform.position + transform.forward, transform.rotation);
                    //combatScript.currentMana = 0;
                    //combatScript.manaBar.SetValue(combatScript.currentMana);
                    break;

                case PlayerElementalForms.ElementalForms.air:
                    animationsScript.PlaySpecialAttackAnimation();
                    Instantiate(airSpecialAttack, transform.position, Quaternion.identity);
                    //combatScript.currentMana = 0;
                    //combatScript.manaBar.SetValue(combatScript.currentMana);
                    break;

                default:
                    break;
            }
        }
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
}
