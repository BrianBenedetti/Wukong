using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum ElementalForms
    {
        normal,
        fire,
        water,
        air
    }

    PlayerInputActions inputActions;

    [Header("Variables")]
    public int healthMax = 100;
    public int currentHealth = 100;
    public int rageDuration = 15;

    public float speed = 6;
    public float jumpForce = 5;
    public float fallMultiplier = 2.5f;
    public float groundDistance = 0.4f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity; //don't touch.

    public bool Enraged = false;
    public bool isVulnerable = true;
    bool isGrounded;
    bool canDoubleJump = true;

    public Transform cam;
    public Transform groundCheck;

    Vector3 direction;

    public LayerMask groundMask;

    [Header ("Input")]
    Vector2 movementInput;

    [Header("Components")]
    Rigidbody rb;
    Animator animator;

    [Header("Animator")]
    int JumpTrigger;
    int PrimaryAttackTrigger;
    int SecondaryAttackTrigger;
    int SpecialAttackTrigger;
    int InteractTrigger;
    int DodgeTrigger;

    [SerializeField] ElementalForms myElement;


    private void Awake()
    {
        inputActions = new PlayerInputActions();

        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();

        inputActions.PlayerControls.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputActions.PlayerControls.Move.canceled += ctx => movementInput = Vector2.zero;

        //inputActions.PlayerControls.Jump.performed += ctx => animator.SetTrigger(JumpTrigger);

        //inputActions.PlayerControls.PrimaryAttack.performed += ctx => animator.SetTrigger(PrimaryAttackTrigger);

        //inputActions.PlayerControls.SecondaryAttack.performed += ctx => animator.SetTrigger(SecondaryAttackTrigger);

        //inputActions.PlayerControls.SpecialAttack.performed += ctx => animator.SetTrigger(SpecialAttackTrigger);

        //inputActions.PlayerControls.Interact.performed += ctx => animator.SetTrigger(InteractTrigger);

        //inputActions.PlayerControls.Dodge.performed += ctx => animator.SetTrigger(DodgeTrigger);

        //inputActions.PlayerControls.Rage.performed += ctx => Enraged = true;

        //inputActions.PlayerControls.NormalForm.performed += ctx => myElement = ElementalForms.normal;
        //inputActions.PlayerControls.FireForm.performed += ctx => myElement = ElementalForms.fire;
        //inputActions.PlayerControls.WaterForm.performed += ctx => myElement = ElementalForms.water;
        //inputActions.PlayerControls.AirForm.performed += ctx => myElement = ElementalForms.air;
    }
    void Start()
    {
        myElement = ElementalForms.normal;
        Enraged = false;
        isVulnerable = true;

        JumpTrigger = Animator.StringToHash("Jump");
        PrimaryAttackTrigger = Animator.StringToHash("Attack1");
        SecondaryAttackTrigger = Animator.StringToHash("Attack2");
        SpecialAttackTrigger = Animator.StringToHash("Attack3");
        InteractTrigger = Animator.StringToHash("Interact");
        DodgeTrigger = Animator.StringToHash("Dodge");
    }

    void Update()
    {
        float horizontal = movementInput.x;
        float vertical = movementInput.y;
        direction = new Vector3(horizontal, 0, vertical).normalized;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (inputActions.PlayerControls.Jump.triggered && isGrounded)
        {
            //add if SHIFT is being clicked, return.
            //animator.SetTrigger(JumpTrigger); //this works
            Jump();
        }else if(inputActions.PlayerControls.Jump.triggered && canDoubleJump)
        {
            Jump();
            canDoubleJump = false;
        }

        if (isGrounded)
        {
            canDoubleJump = true;
        }
    }

    private void FixedUpdate()
    {
        Move(direction);

        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    #region Player Actions
    public void Move(Vector3 direction)
    {
        if(direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            rb.MoveRotation(rotation);
            rb.MovePosition(transform.position + (moveDir.normalized * speed * Time.fixedDeltaTime));
        }
    }
    public void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.velocity = Vector3.up * jumpForce;
    }
    public void PrimaryAttack()
    {

    }
    public void SecondaryAttack()
    {

    }
    public void SpecialAttack()
    {

    }
    public void Interact()
    {

    }
    public void TakeDamage(int damage)
    {

    }
    public void Die()
    {

    }
    #endregion
    #region Coroutines
    public IEnumerator Rage()
    {
        speed *= 2;
        isVulnerable = false;

        yield return new WaitForSecondsRealtime(rageDuration);
        Enraged = false;
        speed /= 2;
        isVulnerable = true;
    }
    #endregion


    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
}
