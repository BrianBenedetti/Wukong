﻿using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour, IDamageable<int, DamageTypes>, IKillable
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ShrineTag))
        {
            interactable = other.GetComponent<IInteractable>();
        }
        else if (other.CompareTag(ObjectTag))
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
    int actualDamage;

    public float speed = 6;
    public float jumpForce = 5;
    public float fallMultiplier = 2.5f;
    //public float attackRate = 2;
    public float groundDistance = 0.1f;
    public float turnSmoothTime = 0.1f;
    public float dashTime;
    public float dashSpeed;
    //public float distanceBetweenGhosts; //if we do ghost images, would be cool
    public float dashCooldown;
    public float knockbackStrength;
    float turnSmoothVelocity; //don't touch.
    //float nextAttackTime = 0; //don't touch
    float dashTimeLeft;
    //float lastGhostXPosition;
    float lastDash = -100; //so player can dash immidiately from start

    public bool Enraged = false;
    public bool isVulnerable = true;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool canDoubleJump = true;
    bool isDashing;

    readonly string ShrineTag = "Shrine";
    readonly string ObjectTag = "Object";
    readonly string damageText = "Damage Text";

    public Transform cam;
    public Transform groundCheck;
    public Transform attackPoint;
    public Transform damageTextPos;

    public Vector3 attackRange;
    [HideInInspector] public Vector3 direction;

    public LayerMask groundMask;
    public LayerMask enemyMask;

    ObjectPooler objectPooler;

    [Header("Interactables")]
    [HideInInspector] public IInteractable interactable;

    [Header ("Input")]
    [HideInInspector] public PlayerInputActions inputActions;
    Vector2 movementInput;
    float itemsScroll;

    [Header("Components")]
    public InventoryObject inventory;

    [HideInInspector] public Rigidbody rb;
    Animator animator;

    [Header("Animator")]
    int Horizontal;
    int Vertical;
    int VerticalSpeed;
    //int JumpTrigger;
    //int PrimaryAttackTrigger;
    //int SecondaryAttackTrigger;
    //int SpecialAttackTrigger;
    int DashTrigger;
    int HurtTrigger;
    int isDeadBool;
    int isGroundedBool;

    [SerializeField] ElementalForms myElement;
    public DamageTypes myDamageType;
    public DamageResistances myResistances;
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
        myDamageType = DamageTypes.normal;
        myElement = ElementalForms.normal;
        Enraged = false;
        isVulnerable = true;
        currentHealth = maxHealth;
        isDashing = false;
        interactable = null;
        objectPooler = ObjectPooler.Instance;

        Horizontal = Animator.StringToHash("Horizontal");
        Vertical = Animator.StringToHash("Vertical");
        VerticalSpeed = Animator.StringToHash("VerticalSpeed");
        //JumpTrigger = Animator.StringToHash("Jump");
        //PrimaryAttackTrigger = Animator.StringToHash("PrimaryAttack");
        //SecondaryAttackTrigger = Animator.StringToHash("SecondaryAttack");
        //SpecialAttackTrigger = Animator.StringToHash("SpecialAttack");
        DashTrigger = Animator.StringToHash("Dodge");
        HurtTrigger = Animator.StringToHash("Hurt");
        isDeadBool = Animator.StringToHash("isDead");
        isGroundedBool = Animator.StringToHash("isGrounded");
    }

    void Update()
    {
        //makes sure health doesn't go above 100%
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

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

        //checks if player wants to dash
        if (inputActions.PlayerControls.Dodge.triggered)
        {
            if(Time.time >= (lastDash + dashCooldown))
            {
                AttemptDash();
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
        //checks if dash has refreshed
        CheckDash(direction);

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
            rb.velocity = new Vector3(direction.x * speed, rb.velocity.y, direction.z * speed);
        }
    }

    public void Jump()
    {
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
                direction.Normalize();
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

    public void Attack(Transform attackOrigin, Vector3 attackRange, Quaternion rotation, LayerMask whatIsEnemy, int damage)
    {
        Collider[] enemiesHit = Physics.OverlapBox(attackOrigin.position, attackRange, rotation, whatIsEnemy);

        foreach(Collider enemy in enemiesHit)
        {
            enemy.GetComponent<IDamageable<int, DamageTypes>>().TakeDamage(damage, myDamageType);
        }
    }

    public void Interact()
    {
        if(interactable != null)
        {
            interactable.Interact();
        }
    }

    public void TakeDamage(int damage, DamageTypes damageType)
    {
        if (isVulnerable)
        {
            PlayerManager.instance.mainCamShake.Shake(1, 0.1f);
            PlayerManager.instance.lockOnShake.Shake(1, 0.1f);
            PlayerManager.instance.hitStop.Stop(0.1f);

            animator.SetTrigger(HurtTrigger);

            actualDamage = myResistances.CalculateDamageWithResistance(damage, damageType);
            currentHealth -= actualDamage;

            ShowDamageText();

            if (currentHealth <= 0)
            {
                animator.SetBool(isDeadBool, true);
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
        //reset all values
        //puts player at last checkpoint position
        transform.position = PlayerManager.instance.lastCheckpointPlayerPosition;
    }

    void ShowDamageText()
    {
        var obj = objectPooler.SpawnFromPool(damageText, damageTextPos.position, Quaternion.identity);
        obj.GetComponent<TextMeshPro>().text = actualDamage.ToString();
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
        //play dissolve shader effect
        Debug.Log(currentHealth);
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

        Gizmos.DrawSphere(groundCheck.position, groundDistance);
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
