using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour, IDamageable<int, DamageTypes>, IKillable
{
    public int maxHealth = 100;
    public int rageDuration = 15;
    public int primaryAttackDamage = 25;
    public int secondaryAttackDamage = 50;
    public int specialAttackDamage = 100; //this has to change cause of different types of special attacks
    public int currentHealth = 0;
    int lightAttackCounter = 0;
    int heavyAttackCounter = 0;
    int actualDamage;

    PlayerElementalForms elementalFormsScript;
    PlayerAnimations playerAnimationsScript;

    //public float knockbackStrength;
    public float maxComboDelay = 1.5f;
    float lastClickedTime = 0;
    public float attackRadius;

    public bool Enraged = false;
    public bool isVulnerable = true;

    readonly string damageText = "Damage Text";

    public Transform damageTextPos;

    public Vector3 attackSize;
    public Vector3 attackPositionOffsetFromPlayerCenter;

    public LayerMask enemyMask;

    ObjectPooler objectPooler;

    [HideInInspector] public PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();

        elementalFormsScript = GetComponent<PlayerElementalForms>();
        playerAnimationsScript = GetComponent<PlayerAnimations>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        Enraged = false;
        isVulnerable = true;

        objectPooler = ObjectPooler.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        //resets combo timer
        if (Time.time - lastClickedTime >= maxComboDelay)
        {
            lightAttackCounter = 0;
            heavyAttackCounter = 0;
        }

        var gamepad = Gamepad.current;

        if (gamepad != null)
        {
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
            lightAttackCounter = 0;
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
            heavyAttackCounter = 0;
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
        heavyAttackCounter = 0;
    }

    public void CheckForEnemiesHit(int damage)
    {
        Collider[] enemiesHit = Physics.OverlapSphere(transform.TransformPoint(attackPositionOffsetFromPlayerCenter), attackRadius, enemyMask);

        foreach (Collider enemy in enemiesHit)
        {
            enemy.GetComponent<IDamageable<int, DamageTypes>>().TakeDamage(damage, elementalFormsScript.currentDamageType);
        }
    }

    public void TakeDamage(int damage, DamageTypes damageType)
    {
        if (isVulnerable)
        {
            PlayerManager.instance.mainCamShake.Shake(1, 0.1f);
            PlayerManager.instance.lockOnShake.Shake(1, 0.1f);
            PlayerManager.instance.hitStop.Stop(0.1f);

            actualDamage = elementalFormsScript.currentResistances.CalculateDamageWithResistance(damage, damageType);
            currentHealth -= actualDamage;

            ShowDamageText();

            if (currentHealth <= 0)
            {
                //play dead anim
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

    IEnumerator Rage()
    {
        //speed *= 2;
        isVulnerable = false;

        yield return new WaitForSeconds(rageDuration);
        Enraged = false;
        //speed /= 2;
        isVulnerable = true;
    }

    public IEnumerator Die()
    {
        //play dissolve shader effect
        yield return new WaitForSeconds(2);
        //You died screen probably
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
