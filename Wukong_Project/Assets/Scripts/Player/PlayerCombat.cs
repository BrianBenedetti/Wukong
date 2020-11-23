using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour, IDamageable<int, DamageTypes>, IKillable
{
    public ParticleSystem RageEffect;
    public Image RageFull;
    public GameObject normalFace;
    public GameObject rageFace;
    public ParticleSystem ManaEffect;

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

    #region Rage Mat and Model Components
    public Material clothesMaterial;
    public Material bodyMaterial;
    public Material hairMaterial;
    public Material rageMaterial;
    public Material staffMaterial;

    public SkinnedMeshRenderer hair;
    public SkinnedMeshRenderer body;
    public SkinnedMeshRenderer shirt;
    public SkinnedMeshRenderer rope;
    public MeshRenderer plateL;
    public MeshRenderer plateR;
    public MeshRenderer plateB;
    public MeshRenderer ring;
    public SkinnedMeshRenderer staff;

    public GameObject rageEyes;
    public GameObject rageSteam;
    #endregion

    #region Slashes
    public GameObject fireSlash;

    public GameObject waterSlash;

    public GameObject airSlash;

    public GameObject normalSlash;
    #endregion

    #region Death Effect
    public Material hairDeathMat;
    public Material bodyDeathMat;
    public Material clothesDeathMat;

    #endregion

    PlayerElementalForms elementalFormsScript;
    PlayerAnimations playerAnimationsScript;
    PlayerMovement playerMovementScript;
    PlayerAudio audioScript;

    public UIBar healthBar;
    public UIBar manaBar;
    public UIBar rageBar;

    public GameObject staffObject;
    public GameObject slamVFX;

    public float maxComboDelay = 1.5f;
    public float lastTimeHit = 0;
    float lastClickedTime = 0;
    public float attackRadius;

    public bool Enraged = false;
    public bool isVulnerable = true;

    readonly string damageText = "Damage Text";

    public Transform damageTextPos;
    public Transform attackSlamPos;
    public Transform rageSlamPos;
    public Transform slashSpawn;

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
        audioScript = GetComponent<PlayerAudio>();

        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        comboCounterScale = comboCounter.GetComponent<RectTransform>().localScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        normalFace.SetActive(true);
        rageFace.SetActive(false);

        currentHealth = maxHealth;
        healthBar.SetMaxValue(maxHealth);
        healthBar.SetValue(maxHealth);
        currentMana = 0;
        manaBar.SetMaxValue(maxMana);
        manaBar.SetValue(currentMana);
        currentRage = 0;
        rageBar.SetMaxValue(maxRage);
        rageBar.SetValue(currentRage);

        Enraged = false;
        isVulnerable = true;

        objectPooler = ObjectPooler.Instance;

        comboCounter.enabled = false;

        staffObject.SetActive(false);

        rageEyes.SetActive(false);
        rageSteam.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);
        currentRage = Mathf.Clamp(currentRage, 0, maxRage);

        if (playerAnimationsScript.anim.GetCurrentAnimatorStateInfo(0).IsName("Light 1") ||
            playerAnimationsScript.anim.GetCurrentAnimatorStateInfo(0).IsName("Light 2") ||
            playerAnimationsScript.anim.GetCurrentAnimatorStateInfo(0).IsName("Light 3") ||
            playerAnimationsScript.anim.GetCurrentAnimatorStateInfo(0).IsName("Heavy 1") ||
            playerAnimationsScript.anim.GetCurrentAnimatorStateInfo(0).IsName("Heavy 2") ||
            playerAnimationsScript.anim.GetCurrentAnimatorStateInfo(0).IsName("Heavy 3"))
        {
            staffObject.SetActive(true);
        }
        else
        {
            staffObject.SetActive(false);
        }

        if (currentRage == maxRage)
        {
            normalFace.SetActive(false);
            rageFace.SetActive(true);
            RageEffect.Play();
            RageFull.color = Color.black;
        }
        else
        {
            normalFace.SetActive(true);
            rageFace.SetActive(false);
            RageEffect.Stop();
            RageFull.color = Color.white;
        }

        if(currentMana == maxMana)
        {
            ManaEffect.Play();
        }
        else
        {
            ManaEffect.Stop();
        }

        if (playerAnimationsScript.anim.IsInTransition(0))
        {
            staffObject.SetActive(false);
        }

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
        }

        if(Time.time - lastTimeHit >= maxComboDelay * 2)
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
            StartCoroutine(playerMovementScript.AttackMovement());
        }
        else
        {
            playerAnimationsScript.SetAnimationBool(playerAnimationsScript.lightAttack1Bool, false);
            playerAnimationsScript.ResetTrigger(playerAnimationsScript.dodgeTrigger);
            playerAnimationsScript.ResetTrigger(playerAnimationsScript.jumpTrigger);
            lightAttackCounter = 0;
        }
    }

    public void HeavyAttack1End()
    {
        if (heavyAttackCounter > 1)
        {
            playerAnimationsScript.SetAnimationBool(playerAnimationsScript.heavyAttack2Bool, true);
            StartCoroutine(playerMovementScript.AttackMovement());
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
        }
    }

    public void LightAttack2End()
    {
        if (lightAttackCounter > 2)
        {
            playerAnimationsScript.SetAnimationBool(playerAnimationsScript.lightAttack3Bool, true);
            StartCoroutine(playerMovementScript.AttackMovement());
        }
        else
        {
            playerAnimationsScript.SetAnimationBool(playerAnimationsScript.lightAttack2Bool, false);
            playerAnimationsScript.SetAnimationBool(playerAnimationsScript.lightAttack1Bool, false);
            playerAnimationsScript.ResetTrigger(playerAnimationsScript.dodgeTrigger);
            playerAnimationsScript.ResetTrigger(playerAnimationsScript.jumpTrigger);
            lightAttackCounter = 0;
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
        lightAttackCounter = 0;
    }

    public void PlaySlashTop()
    {
        switch (elementalFormsScript.currentElement)
        {
            case PlayerElementalForms.ElementalForms.fire:
                Instantiate(fireSlash, slashSpawn.position, Quaternion.identity);
                break;
            case PlayerElementalForms.ElementalForms.water:
                Instantiate(waterSlash, slashSpawn.position, Quaternion.identity);
                break;
            case PlayerElementalForms.ElementalForms.air:
                Instantiate(airSlash, slashSpawn.position, Quaternion.identity);
                break;
            case PlayerElementalForms.ElementalForms.normal:
                Instantiate(normalSlash, slashSpawn.position, Quaternion.identity);
                break;
            default:
                break;
        }
    }

    public void PlaySlashBot()
    {
        switch (elementalFormsScript.currentElement)
        {
            case PlayerElementalForms.ElementalForms.fire:
                var obj = Instantiate(fireSlash, slashSpawn.position, Quaternion.identity);
                obj.transform.parent = transform;
                obj.transform.localRotation = Quaternion.Euler(180, 0, 0);
                break;
            case PlayerElementalForms.ElementalForms.water:
                var obj1 = Instantiate(waterSlash, slashSpawn.position, Quaternion.identity);
                obj1.transform.parent = transform;
                obj1.transform.localRotation = Quaternion.Euler(180, 0, 0);
                break;
            case PlayerElementalForms.ElementalForms.air:
                var obj2 = Instantiate(airSlash, slashSpawn.position, Quaternion.identity);
                obj2.transform.parent = transform;
                obj2.transform.localRotation = Quaternion.Euler(180, 0, 0);
                break;
            case PlayerElementalForms.ElementalForms.normal:
                var obj3 = Instantiate(normalSlash, slashSpawn.position, Quaternion.identity);
                obj3.transform.parent = transform;
                obj3.transform.localRotation = Quaternion.Euler(180, 0, 0);
                break;
            default:
                break;
        }
    }

    public void PlaySlash45()
    {
        switch (elementalFormsScript.currentElement)
        {
            case PlayerElementalForms.ElementalForms.fire:
                var obj = Instantiate(fireSlash, slashSpawn.position, Quaternion.identity);
                obj.transform.parent = transform;
                obj.transform.localRotation = Quaternion.Euler(180, 0, -45);
                break;
            case PlayerElementalForms.ElementalForms.water:
                var obj1 = Instantiate(waterSlash, slashSpawn.position, Quaternion.identity);
                obj1.transform.parent = transform;
                obj1.transform.localRotation = Quaternion.Euler(180, 0, -45);
                break;
            case PlayerElementalForms.ElementalForms.air:
                var obj2 = Instantiate(airSlash, slashSpawn.position, Quaternion.identity);
                obj2.transform.parent = transform;
                obj2.transform.localRotation = Quaternion.Euler(180, 0, -45);
                break;
            case PlayerElementalForms.ElementalForms.normal:
                var obj3 = Instantiate(normalSlash, slashSpawn.position, Quaternion.identity);
                obj3.transform.parent = transform;
                obj3.transform.localRotation = Quaternion.Euler(180, 0, -45);
                break;
            default:
                break;
        }
    }

    public void PlaySlashNegative45()
    {
        switch (elementalFormsScript.currentElement)
        {
            case PlayerElementalForms.ElementalForms.fire:
                var obj = Instantiate(fireSlash, slashSpawn.position, Quaternion.identity);
                obj.transform.parent = transform;
                obj.transform.localRotation = Quaternion.Euler(0, 0, -45);
                break;
            case PlayerElementalForms.ElementalForms.water:
                var obj1 = Instantiate(waterSlash, slashSpawn.position, Quaternion.identity);
                obj1.transform.parent = transform;
                obj1.transform.localRotation = Quaternion.Euler(0, 0, -45);
                break;
            case PlayerElementalForms.ElementalForms.air:
                var obj2 = Instantiate(airSlash, slashSpawn.position, Quaternion.identity);
                obj2.transform.parent = transform;
                obj2.transform.localRotation = Quaternion.Euler(0, 0, -45);
                break;
            case PlayerElementalForms.ElementalForms.normal:
                var obj3 = Instantiate(normalSlash, slashSpawn.position, Quaternion.identity);
                obj3.transform.parent = transform;
                obj3.transform.localRotation = Quaternion.Euler(0, 0, -45);
                break;
            default:
                break;
        }
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
                PlayerManager.instance.mainCamShake.Shake(2, 0.1f);
                //PlayerManager.instance.lockOnShake.Shake(1, 0.1f);
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
        elementalFormsScript.SetSkinnedMaterial(body, 0, bodyMaterial);
        elementalFormsScript.SetSkinnedMaterial(hair, 0, hairMaterial);
        elementalFormsScript.SetSkinnedMaterial(shirt, 0, clothesMaterial);
        elementalFormsScript.SetSkinnedMaterial(rope, 0, clothesMaterial);
        elementalFormsScript.SetMaterial(plateR, 0, clothesMaterial);
        elementalFormsScript.SetMaterial(plateL, 0, clothesMaterial);
        elementalFormsScript.SetMaterial(plateB, 0, clothesMaterial);
        elementalFormsScript.SetMaterial(ring, 0, clothesDeathMat);

        transform.position = PlayerManager.instance.lastCheckpointPlayerPosition;
        RestoreValues(100, 0, 0);
        controller.enabled = true;
        anim.SetBool(deathBool, false);
      
    }

    public void PlaySlamVFX(int pos)
    {
        if(pos == 1)
        {
            var obj = ObjectPooler.Instance.SpawnFromPool("Slam Effect", attackSlamPos.position, Quaternion.identity);
            obj.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);

        }else if(pos == 2)
        {
            var obj = ObjectPooler.Instance.SpawnFromPool("Slam Effect", rageSlamPos.position, Quaternion.identity);
            obj.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }
    }

    void ShowDamageText()
    {
        var obj = objectPooler.SpawnFromPool(damageText, damageTextPos.position, Quaternion.identity);
        obj.GetComponent<TextMeshPro>().text = actualDamage.ToString();
    }

    IEnumerator Rage()
    {
        Enraged = true;
        yield return new WaitForSeconds(0.01f);
        playerMovementScript.currentSpeed = playerMovementScript.rageSpeed;
        isVulnerable = false;
        playerAnimationsScript.PlayRageAnimation();
        audioScript.PlaySFX(1);
        //activate shader
        elementalFormsScript.SetSkinnedMaterial(hair, 0, rageMaterial);
        elementalFormsScript.SetSkinnedMaterial(body, 0, rageMaterial);
        elementalFormsScript.SetSkinnedMaterial(rope, 0, rageMaterial);
        elementalFormsScript.SetSkinnedMaterial(shirt, 0, rageMaterial);
        elementalFormsScript.SetSkinnedMaterial(staff, 0, rageMaterial);
        elementalFormsScript.SetMaterial(plateB, 0, rageMaterial);
        elementalFormsScript.SetMaterial(plateL, 0, rageMaterial);
        elementalFormsScript.SetMaterial(plateR, 0, rageMaterial);
        elementalFormsScript.SetMaterial(ring, 0, rageMaterial);
        rageEyes.SetActive(true);
        rageSteam.SetActive(true);
        StartCoroutine(DecreaseRageBarOverTime(rageDuration));

        yield return new WaitForSeconds(rageDuration);

        Enraged = false;
        playerMovementScript.currentSpeed = playerMovementScript.normalSpeed;
        isVulnerable = true;
        //deactivate shader
        elementalFormsScript.SetSkinnedMaterial(hair, 0, hairMaterial);
        elementalFormsScript.SetSkinnedMaterial(body, 0, bodyMaterial);
        elementalFormsScript.SetSkinnedMaterial(rope, 0, clothesMaterial);
        elementalFormsScript.SetSkinnedMaterial(shirt, 0, clothesMaterial);
        elementalFormsScript.SetSkinnedMaterial(staff, 0, staffMaterial);
        elementalFormsScript.SetMaterial(plateB, 0, clothesMaterial);
        elementalFormsScript.SetMaterial(plateL, 0, clothesMaterial);
        elementalFormsScript.SetMaterial(plateR, 0, clothesMaterial);
        elementalFormsScript.SetMaterial(ring, 0, clothesMaterial);
        rageEyes.SetActive(false);
        rageSteam.SetActive(false);
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

    IEnumerator LerpDeathShader(float time)
    {
        float timePassed = 0;

        int currentValueAtStart = 0;

        while (timePassed < 1)
        {
            timePassed += Time.deltaTime / time;
            bodyDeathMat.SetFloat("_AlbedoTransition", Mathf.Lerp(currentValueAtStart, 3, timePassed));
            hairDeathMat.SetFloat("_AlbedoTransition", Mathf.Lerp(currentValueAtStart, 3, timePassed));
            clothesDeathMat.SetFloat("_AlbedoTransition", Mathf.Lerp(currentValueAtStart, 3, timePassed));



            yield return null;
        }

        bodyDeathMat.SetFloat("_AlbedoTransition", 3);
        hairDeathMat.SetFloat("_AlbedoTransition", 3);
        clothesDeathMat.SetFloat("_AlbedoTransition", 3);

    }

    public IEnumerator Die()
    {
        //play dissolve shader effect
        elementalFormsScript.SetSkinnedMaterial(body, 0, bodyDeathMat);
        elementalFormsScript.SetSkinnedMaterial(hair, 0, hairDeathMat);
        elementalFormsScript.SetSkinnedMaterial(shirt, 0, clothesDeathMat);
        elementalFormsScript.SetSkinnedMaterial(rope, 0, clothesDeathMat);
        elementalFormsScript.SetMaterial(plateR, 0, clothesDeathMat);
        elementalFormsScript.SetMaterial(plateL, 0, clothesDeathMat);
        elementalFormsScript.SetMaterial(plateB, 0, clothesDeathMat);
        elementalFormsScript.SetMaterial(ring, 0, clothesDeathMat);

        StartCoroutine(LerpDeathShader(2f));

        controller.enabled = false;
        yield return new WaitForSeconds(3);
        Respawn();
        elementalFormsScript.ToNormal();
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
