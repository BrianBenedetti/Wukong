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

    [Header("General")]
    public int healthMax;
    public int currentHealth;
    public int rageDuration;

    public float speed = 5;

    public bool Enraged;

    [Header ("Input")]
    Vector2 movementInput;
    Vector2 cameraInput;

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

        inputActions.PlayerControls.Camera.performed += ctx => cameraInput = ctx.ReadValue<Vector2>();
        inputActions.PlayerControls.Camera.canceled += ctx => cameraInput = Vector2.zero;

        inputActions.PlayerControls.Jump.performed += ctx => animator.SetTrigger(JumpTrigger);

        inputActions.PlayerControls.PrimaryAttack.performed += ctx => animator.SetTrigger(PrimaryAttackTrigger);

        inputActions.PlayerControls.SecondaryAttack.performed += ctx => animator.SetTrigger(SecondaryAttackTrigger);

        inputActions.PlayerControls.SpecialAttack.performed += ctx => animator.SetTrigger(SpecialAttackTrigger);

        inputActions.PlayerControls.Interact.performed += ctx => animator.SetTrigger(InteractTrigger);

        inputActions.PlayerControls.Dodge.performed += ctx => animator.SetTrigger(DodgeTrigger);

        inputActions.PlayerControls.Rage.performed += ctx => Enraged = true;

        inputActions.PlayerControls.NormalForm.performed += ctx => myElement = ElementalForms.normal;
        inputActions.PlayerControls.FireForm.performed += ctx => myElement = ElementalForms.fire;
        inputActions.PlayerControls.WaterForm.performed += ctx => myElement = ElementalForms.water;
        inputActions.PlayerControls.AirForm.performed += ctx => myElement = ElementalForms.air;
    }
    void Start()
    {
        myElement = ElementalForms.normal;

        JumpTrigger = Animator.StringToHash("Jump");
        PrimaryAttackTrigger = Animator.StringToHash("Attack1");
        SecondaryAttackTrigger = Animator.StringToHash("Attack2");
        SpecialAttackTrigger = Animator.StringToHash("Attack3");
        InteractTrigger = Animator.StringToHash("Interact");
        DodgeTrigger = Animator.StringToHash("Dodge");
    }

    void Update()
    {
        //float horizontal = movementInput.x;
        //float vertical = movementInput.y;

        //Vector3 direction = new Vector3(horizontal, 0, vertical);

        //Debug.Log(direction);
    }

    #region Player Actions
    public void Move(Vector3 direction)
    {
        
    }
    public void Jump()
    {

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
    #endregion
    #region Coroutines
    public IEnumerator Rage()
    {
        healthMax += healthMax;
        currentHealth += currentHealth;
        speed += speed;

        yield return new WaitForSecondsRealtime(rageDuration);
        Enraged = false;
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
