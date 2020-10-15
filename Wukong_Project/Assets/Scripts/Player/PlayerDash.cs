﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    PlayerMovement moveScript;
    PlayerAnimations animationsScript;

    bool canDash;

    public float dashTime;
    public float dashSpeed;
    public float dashCooldown;
    float currentCooldown;

    [HideInInspector] public PlayerInputActions inputActions;


    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    // Start is called before the first frame update
    void Start()
    {
        canDash = true;

        moveScript = GetComponent<PlayerMovement>();
        animationsScript = GetComponent<PlayerAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        var gamepad = Gamepad.current;
        if(gamepad != null)
        {
            if (inputActions.PlayerControls.Dodge.triggered && !gamepad.leftShoulder.isPressed && canDash && moveScript.direction.magnitude > 0)
            {
                StartCoroutine(Dash());
                animationsScript.PlayDodgeAnimation();
            }
        }
        else
        {
            if (inputActions.PlayerControls.Dodge.triggered && canDash && moveScript.direction.magnitude > 0)
            {
                StartCoroutine(Dash());
                animationsScript.PlayDodgeAnimation();
            }
        }

        if (canDash)
        {
            currentCooldown = dashCooldown;
        }
        else
        {
            currentCooldown -= Time.deltaTime;
            if(currentCooldown <= 0)
            {
                canDash = true;
            }
        }
    }
    
    IEnumerator Dash()
    {
        canDash = false;

        float startTime = Time.time;

        moveScript.velocity.y = 0;

        while (Time.time < startTime + dashTime)
        {
            moveScript.controller.Move(transform.forward * dashSpeed * Time.deltaTime);

            yield return null;
        }
        moveScript.velocity.y = -2;
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
