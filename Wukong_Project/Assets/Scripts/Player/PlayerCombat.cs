using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour, IDamageable<int, DamageTypes>, IKillable
{
    public int maxHealth = 100;
    public int maxMana = 100;
    public int maxRage = 100;
    public int rageDuration = 10;
    public int primaryAttackDamage = 25;
    public int secondaryAttackDamage = 50;
    public int currentHealth = 0;
    public int currentMana = 0;
    public int currentRage = 0;
    public int comboHits = 0;
    int lightAttackCounter = 0;
    int heavyAttackCounter = 0;
    int actualDamage;
    readonly int deathBool = Animator.StringToHash("isDead");

    PlayerElementalForms elementalFormsScript;
    PlayerAnimations playerAnimationsScript;
    PlayerMovement playerMovementScript;

    public UIBar healthBar;
    public UIBar manaBar;
    public UIBar rageBar;

    public GameObject staff;

    public float maxComboDelay = 1.5f;
    public float lastTimeHit = 0;
    float lastClickedTime = 0;
    public float attackRadius;

    public bool Enraged = false;
    public bool isVulnerable = true;

    readonly string damageText = "Damage Text";

    public Transform damageTextPos;

    public Vector3 attackSize;
    public Vector3 attackPositionOffsetFromPlayerCenter;
    Vector3 comboCounterScale;

    public LayerMask enemyMask;

    ObjectPooler objectPooler;

    [HideInInspector] public PlayerInputActions inputActions;

    Animator anim;

    CharacterController controller;

    public TextMeshProUGUI comboCounter;

    private void Awake()
    {
        inputActions = new PlayerInputActions();

        elementalFormsScript = GetComponent<PlayerElementalForms>();
        playerAnimationsScript = GetComponent<PlayerAnimations>();
        playerMovementScript = GetComponent<PlayerMovement>();

        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        comboCounterScale = comboCounter.GetComponent<RectTransform>().localScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxValue(maxHealth);
        healthBar.SetValue(maxHealth);
        currentMana = 100;
        manaBar.SetMaxValue(maxMana);
        manaBar.SetValue(currentMana);
        currentRage = 100;
        rageBar.SetMaxValue(maxRage);
        rageBar.SetValue(currentRage);

        Enraged = false;
        isVulnerable = true;

        objectPooler = ObjectPooler.Instance;

        comboCounter.enabled = false;
        staff.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);
        currentRage = Mathf.Clamp(currentRage, 0, maxRage);

        if (comboHits > 0)
        {
            comboCounter.enabled = true;
            comboCounter.text = "x" + comboHits.ToString();
        }
        else
        {
            comboCounter.enabled = false;
        }

        //resets combo timer
        if (Time.time - lastClickedTime >= maxComboDelay)
        {
            lightAttackCounter = 0;
            heavyAttackCounter = 0;
            playerMovementScript.canMove = true;
        }

        if(Time.time - lastTimeHit >= maxComboDelay)
        {
            comboHits = 0;
        }

        var gamepad = Gamepad.current;

        if (gamepad != null)
        {
            //light attack
            if (inputActions.PlayerControls.PrimaryAttack.triggered && !gamepad.leftShoulder.isPressed)
            {
                lastClickedTime = Time.time;
                lightAttackCounter++;
                playerMovementScript.canMove = false;
                staff.SetActive(true);

                if (lightAttackCounter == 1)
                {
                    playerAnimationsScript.SetAnimationBool(playerAnimationsScript.lightAttack1Bool, true);
                }
                lightAttackCounter = Mathf.Clamp(lightAttackCounter, 0, 3);
            }
            //heavy attack
            else if (inputActions.PlayerControls.SecondaryAttack.triggered && !gamepad.leftShoulder.isPressed)
            {
                lastClickedTime = Time.time;
                heavyAttackCounter++;
                playerMovementScript.canMove = false;
                staff.SetActive(true);

                if (heavyAttackCounter == 1)
                {
                    playerAnimationsScript.SetAnimationBool(playerAnimationsScript.heavyAttack1Bool, true);
                }
                heavyAttackCounter = Mathf.Clamp(heavyAttackCounter, 0, 3);
            }
        }
        else
        {
            //light attack
            if (inputActions.PlayerControls.PrimaryAttack.triggered)
            {
                lastClickedTime = Time.time;
                lightAttackCounter++;
                playerMovementScript.canMove = false;
                staff.SetActive(true);

                if (lightAttackCounter == 1)
                {
                    playerAnimationsScript.SetAnimationBool(playerAnimationsScript.lightAttack1Bool, true);
                }
                lightAttackCounter = Mathf.Clamp(lightAttackCounter, 0, 3);
            }
            //heavy attack
            else if (inputActions.PlayerControls.SecondaryAttack.triggered)
            {
                lastClickedTime = Time.time;
                heavyAttackCounter++;
                playerMovementScript.canMove = false;
                staff.SetActive(true);

                if (heavyAttackCounter == 1)
                {
                    playerAnimationsScript.SetAnimationBool(playerAnimationsScript.heavyAttack1Bool, true);
                }
                heavyAttackCounter = Mathf.Clamp(heavyAttackCounter, 0, 3);
            }
        }

        if (inputActions.PlayerControls.Rage.triggered && currentRage == maxRage && !Enraged)
        {
            StartCoroutine(Rage());
        }
    }

    public void LightAttack1End()
    {
        if(lightAttackCounter > 1)
        {
            playerAnimationsScript.SetAnimationBool(playerAnimationsScript.lightAttack2Bool, true);
        }
        else
        {
            playerAnimationsScript.SetAnimationBool(playerAnimationsScript.lightAttack1Bool, false);
            playerAnimationsScript.ResetTrigger(playerAnimationsScript.dodgeTrigger);
            playerAnimationsScript.ResetTrigger(playerAnimationsScript.jumpTrigger);
            lightAttackCounter = 0;
            staff.SetActive(false);
        }
    }

    public void HeavyAttack1End()
    {
        if (heavyAttackCounter > 1)
        {
            playerAnimationsScript.SetAnimationBool(playerAnimationsScript.heavyAttack2Bool, true);
        }
        else
        {
            playerAnimationsScript.SetAnimationBool(playerAnimationsScript.heavyAttack1Bool, false);
            playerAnimationsScript.SetAnimationBool(playerAnimationsScript.lightAttack1Bool, false);
            playerAnimationsScript.SetAnimationBool(playerAnimationsScript.lightAttack2Bool, false);
            playerAnimationsScript.SetAnimationBool(playerAnimationsScript.lightAttack3Bool, false);
            playerAnimationsScript.ResetTrigger(playerAnimationsScript.dodgeTrigger);
            playerAnimationsScript.ResetTrigger(playerAnimationsScript.jumpTrigger);
            heavyAttackCounter = 0;
            staff.SetActive(false);
        }
    }

    public void LightAttack2End()
    {
        if (lightAttackCounter > 2)
        {
            playerAnimationsScript.SetAnimationBool(playerAnimationsScript.lightAttack3Bool, true);
        }
        else
        {
            playerAnimationsScript.SetAnimationBool(playerAnimationsScript.lightAttack2Bool, false);
            playerAnimationsScript.SetAnimationBool(playerAnimationsScript.lightAttack1Bool, false);
            playerAnimationsScript.ResetTrigger(playerAnimationsScript.dodgeTrigger);
            playerAnimationsScript.ResetTrigger(playerAnimationsScript.jumpTrigger);
            lightAttackCounter = 0;
            staff.SetActive(false);
        }
    }

    public void HeavyAttack2End()
    {
        if (heavyAttackCounter > 2)
        {
            playerAnimationsScript.SetAnimationBool(playerAnimationsScript.heavyAttack3Bool, true);
        }
        else
        {
            playerAnimationsScript.SetAnimationBool(playerAnimationsScript.heavyAttack2Bool, false);
            playerAnimationsScript.SetAnimationBool(playerAnimationsScript.heavyAttack1Bool, false);
            playerAnimationsScript.SetAnimationBool(playerAnimationsScript.lightAttack1Bool, false);
            playerAnimationsScript.SetAnimationBool(playerAnimationsScript.lightAttack2Bool, false);
            playerAnimationsScript.SetAnimationBool(playerAnimationsScript.lightAttack3Bool, false);
            playerAnimationsScript.ResetTrigger(playerAnimationsScript.dodgeTrigger);
            playerAnimationsScript.ResetTrigger(playerAnimationsScript.jumpTrigger);
            heavyAttackCounter = 0;
            staff.SetActive(false);
        }
    }

    public void LightAttack3End()
    {
        playerAnimationsScript.SetAnimationBool(playerAnimationsScript.lightAttack1Bool, false);
        playerAnimationsScript.SetAnimationBool(playerAnimationsScript.lightAttack2Bool, false);
        playerAnimationsScript.SetAnimationBool(playerAnimationsScript.lightAttack3Bool, false);
        playerAnimationsScript.SetAnimationBool(playerAnimationsScript.heavyAttack1Bool, false);
        playerAnimationsScript.SetAnimationBool(playerAnimationsScript.heavyAttack2Bool, false);
        playerAnimationsScript.SetAnimationBool(playerAnimationsScript.heavyAttack3Bool, false);
        playerAnimationsScript.ResetTrigger(playerAnimationsScript.dodgeTrigger);
        playerAnimationsScript.ResetTrigger(playerAnimationsScript.jumpTrigger);
        lightAttackCounter = 0;
        staff.SetActive(false);
    }

    public void HeavyAttack3End()
    {
        playerAnimationsScript.SetAnimationBool(playerAnimationsScript.lightAttack1Bool, false);
        playerAnimationsScript.SetAnimationBool(playerAnimationsScript.lightAttack2Bool, false);
        playerAnimationsScript.SetAnimationBool(playerAnimationsScript.lightAttack3Bool, false);
        playerAnimationsScript.SetAnimationBool(playerAnimationsScript.heavyAttack1Bool, false);
        playerAnimationsScript.SetAnimationBool(playerAnimationsScript.heavyAttack2Bool, false);
        playerAnimationsScript.SetAnimationBool(playerAnimationsScript.heavyAttack3Bool, false);
        playerAnimationsScript.ResetTrigger(playerAnimationsScript.dodgeTrigger);
        playerAnimationsScript.ResetTrigger(playerAnimationsScript.jumpTrigger);
        heavyAttackCounter = 0;
        staff.SetActive(false);
    }

    public void CheckForEnemiesHit(int damage)
    {
        Collider[] enemiesHit = Physics.OverlapSphere(transform.TransformPoint(attackPositionOffsetFromPlayerCenter), attackRadius, enemyMask);

        foreach (Collider enemy in enemiesHit)
        {
            if (Enraged)
            {
                enemy.GetComponent<IDamageable<int, DamageTypes>>().TakeDamage(damage * 2, elementalFormsScript.currentDamageType);
            }
            else
            {
                enemy.GetComponent<IDamageable<int, DamageTypes>>().TakeDamage(damage, elementalFormsScript.currentDamageType);
            }

            comboHits++;
            lastTimeHit = Time.time;
        }
    }

    public void TakeDamage(int damage, DamageTypes damageType)
    {
        if (isVulnerable)
        {
            actualDamage = elementalFormsScript.currentResistances.CalculateDamageWithResistance(damage, damageType);
            currentHealth -= actualDamage;
            //dont play hurt animation if damage was zero AKA same element
            if(actualDamage != 0)
            {
                PlayerManager.instance.mainCamShake.Shake(1, 0.1f);
                PlayerManager.instance.lockOnShake.Shake(1, 0.1f);
                PlayerManager.instance.hitStop.Stop(0.1f);

                playerAnimationsScript.PlayHurtAnimation();
            }

            healthBar.SetValue(currentHealth);

            ShowDamageText();

            if (currentHealth <= 0)
            {
                anim.SetBool(deathBool, true);
                StartCoroutine(Die());
            }
        }
    }

    public void RestoreValues(int health, int rage, int mana)
    {
        currentHealth += health;
        healthBar.SetValue(currentHealth);
        currentMana += mana;
        manaBar.SetValue(currentMana);
        currentRage += rage;
        rageBar.SetValue(currentRage);
    }

    public void Respawn()
    {
        transform.position = PlayerManager.instance.lastCheckpointPlayerPosition;
        RestoreValues(60, 0, 0);
        controller.enabled = true;
        anim.SetBool(deathBool, false);
    }

    void ShowDamageText()
    {
        var obj = objectPooler.SpawnFromPool(damageText, damageTextPos.position, Quaternion.identity);
        obj.GetComponent<TextMeshPro>().text = actualDamage.ToString();
    }

    IEnumerator Rage()
    {
        Enraged = true;
        playerMovementScript.currentSpeed = playerMovementScript.rageSpeed;
        isVulnerable = false;
        playerAnimationsScript.PlayRageAnimation();
        //activate shader
        Debug.Log(Enraged);
        StartCoroutine(DecreaseRageBarOverTime(rageDuration));

        yield return new WaitForSeconds(rageDuration);

        Enraged = false;
        playerMovementScript.currentSpeed = playerMovementScript.normalSpeed;
        isVulnerable = true;
        Debug.Log(Enraged);
        //deactivate shader
    }

    IEnumerator DecreaseRageBarOverTime(float time)
    {
        float timePassed = 0;

        int currentRageAtStart = currentRage;

        while(timePassed < 1)
        {
            timePassed += Time.deltaTime / time;

            currentRage = (int)Mathf.Lerp(currentRageAtStart, 0, timePassed);
            rageBar.SetValue(currentRage);

            yield return null;
        }

        currentRage = 0;
        rageBar.SetValue(currentRage);
    }

    public IEnumerator Die()
    {
        //play dissolve shader effect
        controller.enabled = false;
        yield return new WaitForSeconds(2);
        Respawn();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.TransformPoint(attackPositionOffsetFromPlayerCenter), attackRadius);
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
