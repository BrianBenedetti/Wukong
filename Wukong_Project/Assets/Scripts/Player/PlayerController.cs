using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable<int>, IKillable
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shrine"))
        {
            interactable = other.GetComponent<IInteractable>();
        }
        else if (other.CompareTag("Object"))
        {
            interactable = other.GetComponent<IInteractable>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Shrine"))
        {
            if (interactable != null)
            {
                interactable = null;
            }
        }
        else if (other.CompareTag("Object"))
        {
            if (interactable != null)
            {
                interactable = null;
            }
        }
    }

    enum ElementalForms
    {
        normal,
        fire,
        water,
        air
    }

    #region Variables
    [Header("Variables")]
    public int maxHealth = 100;
    public int rageDuration = 15;
    public int primaryAttackDamage = 25;
    public int secondaryAttackDamage = 50;
    public int specialAttackDamage = 100; //this has to change cause of different types of special attacks
    [SerializeField] int currentHealth = 0;

    public float speed = 6;
    public float jumpForce = 5;
    public float fallMultiplier = 2.5f;
    public float attackRate = 2;
    public float groundDistance = 0.4f;
    public float turnSmoothTime = 0.1f;
    public float dashTime;
    public float dashSpeed;
    //public float distanceBetweenGhosts; //if we do ghost images, would be cool
    public float dashCooldown;
    float turnSmoothVelocity; //don't touch.
    float nextAttackTime = 0; //don't touch
    float dashTimeLeft;
    //float lastGhostXPosition;
    float lastDash = -100; //so player can dash immidiately from start

    public bool Enraged = false;
    public bool isVulnerable = true;
    bool isGrounded;
    bool canDoubleJump = true;
    bool isDashing;

    public Transform cam;
    public Transform groundCheck;
    public Transform attackPoint;

    public Vector3 attackRange;
    Vector3 direction;

    public LayerMask groundMask;
    public LayerMask enemyMask;

    [Header("Interactables")]
    [HideInInspector] public IInteractable interactable;

    [Header ("Input")]
    Vector2 movementInput;
    float itemsScroll;
    PlayerInputActions inputActions;

    [Header("Components")]
    public InventoryObject inventory;

    Rigidbody rb;
    Animator animator;

    [Header("Animator")]
    int Horizontal;
    int Vertical;
    int VerticalSpeed;
    int JumpTrigger;
    int PrimaryAttackTrigger;
    int SecondaryAttackTrigger;
    int SpecialAttackTrigger;
    int InteractTrigger;
    int DashTrigger;
    int isDeadBool;
    int isGroundedBool;

    [SerializeField] ElementalForms myElement;
    //damage resistance will be separate from the form itself, will have to swap resistance files when swapping forms
    #endregion

    private void Awake()
    {
        inputActions = new PlayerInputActions();

        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();

        inputActions.PlayerControls.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputActions.PlayerControls.Move.canceled += ctx => movementInput = Vector2.zero;

        inputActions.PlayerControls.MouseCycle.performed += ctx => itemsScroll = ctx.ReadValue<float>();
        inputActions.PlayerControls.MouseCycle.canceled += ctx => itemsScroll = 0;
    }
    void Start()
    {
        myElement = ElementalForms.normal;
        Enraged = false;
        isVulnerable = true;
        currentHealth = maxHealth;
        isDashing = false;
        interactable = null;

        Horizontal = Animator.StringToHash("Horizontal");
        Vertical = Animator.StringToHash("Vertical");
        VerticalSpeed = Animator.StringToHash("VerticalSpeed");
        JumpTrigger = Animator.StringToHash("Jump");
        PrimaryAttackTrigger = Animator.StringToHash("Attack1");
        SecondaryAttackTrigger = Animator.StringToHash("Attack2");
        SpecialAttackTrigger = Animator.StringToHash("Attack3");
        InteractTrigger = Animator.StringToHash("Interact");
        DashTrigger = Animator.StringToHash("Dodge");
        isDeadBool = Animator.StringToHash("isDead");
        isGroundedBool = Animator.StringToHash("isGrounded");
    }

    void Update()
    {
        //makes sure health doesn't go above 100%
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        //checks if dash has refreshed
        CheckDash(direction);

        //checks for input to change element
        if (inputActions.PlayerControls.NormalForm.triggered)
        {
            myElement = ElementalForms.normal;
            //change hair shader
            //change weapon VFX
        }
        else if (inputActions.PlayerControls.FireForm.triggered)
        {
            myElement = ElementalForms.fire;
            //change hair shader
            //play VFX
            //change weapon VFX
        }
        else if (inputActions.PlayerControls.WaterForm.triggered)
        {
            myElement = ElementalForms.water;
            //change hair shader
            //play VFX
            //change weapon VFX
        }
        else if (inputActions.PlayerControls.AirForm.triggered)
        {
            myElement = ElementalForms.air;
            //change hair shader
            //play VFX
            //change weapon VFX
        }

        //checks to interact with objects
        if (inputActions.PlayerControls.Interact.triggered)
        {
            Interact();
        }

        //cycles the items in inventory
        if (itemsScroll < -0.1f || inputActions.PlayerControls.GamepadCycleRight.triggered)
        {
            inventory.MoveItem(0, inventory.Container.Items.Count - 1);
        }
        else if (itemsScroll > 0.1f || inputActions.PlayerControls.GamepadCycleLeft.triggered)
        {
            inventory.MoveItem(inventory.Container.Items.Count - 1, 0);
        }

        //checks to use item
        if (inputActions.PlayerControls.UseItem.triggered)
        {
            inventory.UseItem();
        }

        //takes input from keyboard or gamepad and makes into a direction for movement
        direction = transform.right * movementInput.x + transform.forward * movementInput.y;

        //checks if player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //checks if player can jump or double jump
        if (inputActions.PlayerControls.Jump.triggered && isGrounded)
        {
            //add if SHIFT is being clicked, return.
            //animator.SetTrigger(JumpTrigger); //this works
            Jump();
        }
        else if(inputActions.PlayerControls.Jump.triggered && canDoubleJump)
        {
            //add if SHIFT is being clicked, return.
            //animator.SetTrigger(JumpTrigger); //this works
            Jump();
            canDoubleJump = false;
        }

        //checks if player wants to dash
        if (inputActions.PlayerControls.Dodge.triggered)
        {
            if(Time.time >= (lastDash + dashCooldown))
            {
                AttemptDash();
            }
        }

        //checks if player can attack
        if(Time.time >= attackRate)
        {
            if (inputActions.PlayerControls.PrimaryAttack.triggered)
            {
                //add if SHIFT is being clicked, return.
                //animator.SetTrigger(PrimaryAttack); //this works
                Attack(attackPoint, attackRange, attackPoint.rotation, enemyMask);
                nextAttackTime = Time.time + 1 / attackRate; //won't need this if i use exit time in the animation, hopefully
            }
        }
        
        //if grounded, double jump is refreshed
        if (isGrounded)
        {
            canDoubleJump = true;
        }

        animator.SetBool(isGroundedBool, isGrounded);
        animator.SetFloat(Horizontal, movementInput.x);
        animator.SetFloat(Vertical, movementInput.y);
        animator.SetFloat(VerticalSpeed, rb.velocity.y);
    }

    private void FixedUpdate()
    {
        //checks if player can move
        if (!isDashing)
        {
            Move(direction); //only for testing purposes
        }

        //checks if player is falling to increase gravity for a less floaty jump
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    #region Player Actions
    public void Move(Vector3 direction)
    {
        if(direction.magnitude > 0.1f)
        {
            direction = Vector3.ClampMagnitude(direction, 1f);
            float targetAngle = cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            //movedir from brackeys here

            rb.MoveRotation(rotation);
            rb.velocity = new Vector3(direction.x * speed, rb.velocity.y, direction.z * speed); //jump no longer works here
        }
    }
    public void Jump()
    {
        animator.SetTrigger(JumpTrigger);
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.velocity = Vector3.up * jumpForce;
    }
    public void AttemptDash()
    {
        isDashing = true;
        animator.SetTrigger(DashTrigger);
        dashTimeLeft = dashTime;
        lastDash = Time.time;
        //drop first ghost
        //make ghostXPos equal to this transform.x
    }
    public void CheckDash(Vector3 direction)
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                rb.velocity = new Vector3(dashSpeed * direction.x, 0, dashSpeed * direction.z);
                dashTimeLeft -= Time.deltaTime;

                //if(Mathf.Abs(transform.position.x - lastGhostXPosition) > distanceBetweenGhosts){
                //enable another ghost from pool
                //lastGhostXPos = transform.position.x;
                //}
            }

            if (dashTimeLeft <= 0)
            {
                rb.velocity = Vector3.zero;
                isDashing = false;
            }
        }
    }
    public void Attack(Transform attackOrigin, Vector3 attackRange, Quaternion rotation, LayerMask whatIsEnemy)
    {
        Collider[] enemiesHit = Physics.OverlapBox(attackOrigin.position, attackRange, rotation, whatIsEnemy);

        foreach(Collider enemy in enemiesHit)
        {
            enemy.GetComponent<IDamageable<int/*, DamageTypes*/>>().TakeDamage(primaryAttackDamage); //must include damage type
            enemy.GetComponent<Animator>().SetTrigger("Hurt");
        }
    }
    public void Interact()
    {
        if(interactable != null)
        {
            interactable.Interact();
        }
    }
    public void TakeDamage(int damage/*, DamageTypes damageType*/)
    {
        if (isVulnerable)
        {
            currentHealth -= damage; //this will change to implement resistances
            if (currentHealth <= 0)
            {
                animator.SetBool("isDead", true);
            }
        }
    }
    public void RestoreValues(int health, int rage, int special)
    {
        currentHealth += health;
        //same for other two
    }
    public void Respawn()
    {
        //reset all variables to original values
    }
    #endregion

    #region Coroutines
    IEnumerator Rage()
    {
        speed *= 2;
        isVulnerable = false;

        yield return new WaitForSeconds(rageDuration);
        Enraged = false;
        speed /= 2;
        isVulnerable = true;
    }

    public IEnumerator Die()
    {
        Debug.Log("You died!");
        //play dissolve shader effect
        yield return new WaitForSeconds(2);
        //You died screen probably
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireCube(attackPoint.position, attackRange);
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Items.Clear(); //clears inventory when leaving play mode
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
