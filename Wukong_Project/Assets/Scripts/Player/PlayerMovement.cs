using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    PlayerAnimations playerAnimationsScript;

    float turnSmoothVelocity; //don't touch
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 2.1f;
    public float fallMultiplier = 1.5f;

    [HideInInspector] public bool isGrounded;
    bool canDoubleJump;

    public Transform cam;
    public Transform groundCheck;

    public LayerMask groundMask;

    [HideInInspector] public PlayerInputActions inputActions;
    Vector2 movementInput;

    [HideInInspector] public Vector3 direction;
    [HideInInspector] public Vector3 velocity;
    [HideInInspector] public Vector3 moveDir;

    private void Awake()
    {
        playerAnimationsScript = GetComponent<PlayerAnimations>();

        inputActions = new PlayerInputActions();

        inputActions.PlayerControls.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputActions.PlayerControls.Move.canceled += ctx => movementInput = Vector2.zero;
    }

    private void Start()
    {
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        //checks if player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //checks if player is falling to increase gravity for a less floaty jump
        if (velocity.y < 0)
        {
            velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }

        //refreshes double jump on land
        if (isGrounded)
        {
            canDoubleJump = true;
        }

        //stops gravity from increasing while grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1;
        }

        //takes input from keyboard or gamepad and makes into a direction for movement
        direction = new Vector3(movementInput.x, 0, movementInput.y);
        direction = Vector3.ClampMagnitude(direction, 1);

        //moves player according to input
        if(direction.magnitude >= 0.05f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 camForward = cam.transform.forward;
            Vector3 camRight = cam.transform.right;

            moveDir = camForward * direction.z + camRight * direction.x;
            moveDir.y = 0;
            moveDir = Vector3.ClampMagnitude(moveDir, 1);

            controller.Move(moveDir * speed * Time.deltaTime);
        }

        //animates player locomotion
        playerAnimationsScript.PlayMovementAnimation(direction.magnitude);

        var gamepad = Gamepad.current;

        if(gamepad != null)
        {
            if (inputActions.PlayerControls.Jump.triggered && isGrounded && !gamepad.leftShoulder.isPressed)
            {
                Jump(); //mechanical jump
                playerAnimationsScript.PlayJumpAnimation(); //plays animation
            }
            //and double jump
            else if (inputActions.PlayerControls.Jump.triggered && canDoubleJump && !gamepad.leftShoulder.isPressed)
            {
                DoubleJump(); //mechanical jump
                playerAnimationsScript.PlayJumpAnimation(); //plays animation
            }
        }
        else
        {
            //handles jumps
            if (inputActions.PlayerControls.Jump.triggered && isGrounded)
            {
                Jump(); //mechanical jump
                playerAnimationsScript.PlayJumpAnimation(); //plays animation
            }
            //and double jump
            else if (inputActions.PlayerControls.Jump.triggered && canDoubleJump)
            {
                DoubleJump(); //mechanical jump
                playerAnimationsScript.PlayJumpAnimation(); //plays animation
            }
        }

        //applies regular gravity to player
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime); //multiply delta time twice because physics
    }

    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }

    void DoubleJump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        canDoubleJump = false;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
    }
}
