using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCombat : MonoBehaviour, IDamageable<int, DamageTypes>, IKillable
{
    public int maxHealth = 100;
    public int rageDuration = 15;
    public int primaryAttackDamage = 25;
    public int secondaryAttackDamage = 50;
    public int specialAttackDamage = 100; //this has to change cause of different types of special attacks
    int actualDamage;
    [SerializeField] int currentHealth = 0;

    PlayerElementalForms elementalFormsScript;
    PlayerAnimations playerAnimationsScript;

    public float knockbackStrength;

    public bool Enraged = false;
    public bool isVulnerable = true;
    bool canDetectInput;

    readonly string damageText = "Damage Text";

    public Transform attackPoint;
    public Transform damageTextPos;

    public Vector3 attackRange;

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

        if (canDetectInput)
        {
            if (inputActions.PlayerControls.PrimaryAttack.triggered)
            {
                playerAnimationsScript.PlayLightAttack();
            }
            else if (inputActions.PlayerControls.SecondaryAttack.triggered)
            {
                playerAnimationsScript.PlayHeavyAttack();
            }
            else
            {
                playerAnimationsScript.PlayNoCombo();
            }
        }
    }

    public void ResetCombo()
    {
        playerAnimationsScript.PlayNoCombo();
    }

    public void StartDetectingInput()
    {
        canDetectInput = true;
    }

    public void StopDetectingInput()
    {
        canDetectInput = false;
    }

    public void CheckForEnemiesHit(Transform attackOrigin, Vector3 attackRange, LayerMask whatIsEnemy, int damage)
    {
        Collider[] enemiesHit = Physics.OverlapBox(attackOrigin.position, attackRange, attackOrigin.rotation, whatIsEnemy);

        foreach (Collider enemy in enemiesHit)
        {
            enemy.GetComponent<IDamageable<int, DamageTypes>>().TakeDamage(damage, elementalFormsScript.myDamageType);
        }
    }

    public void TakeDamage(int damage, DamageTypes damageType)
    {
        if (isVulnerable)
        {
            PlayerManager.instance.mainCamShake.Shake(1, 0.1f);
            PlayerManager.instance.lockOnShake.Shake(1, 0.1f);
            PlayerManager.instance.hitStop.Stop(0.1f);

            actualDamage = elementalFormsScript.myResistances.CalculateDamageWithResistance(damage, damageType);
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

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireCube(attackPoint.position, attackRange);
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
