using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    PlayerMovement moveScript;
    PlayerAnimations animationsScript;

    public float dashTime;
    public float dashSpeed;

    [HideInInspector] public PlayerInputActions inputActions;


    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<PlayerMovement>();
        animationsScript = GetComponent<PlayerAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        var gamepad = Gamepad.current;
        if(gamepad != null)
        {
            if (inputActions.PlayerControls.Dodge.triggered && !gamepad.leftShoulder.isPressed)
            {
                StartCoroutine(Dash());
                animationsScript.PlayDodgeAnimation();
            }
        }
        else
        {
            if (inputActions.PlayerControls.Dodge.triggered)
            {
                StartCoroutine(Dash());
                animationsScript.PlayDodgeAnimation();
            }
        }
    }
    
    IEnumerator Dash()
    {
        //can dash infinitely
        float startTime = Time.time;

        moveScript.velocity.y = 0;

        while(Time.time < startTime + dashTime)
        {
            moveScript.controller.Move(moveScript.moveDir.normalized * dashSpeed * Time.deltaTime);

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
