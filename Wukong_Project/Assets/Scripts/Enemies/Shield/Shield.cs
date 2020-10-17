using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, IDamageable<int, DamageTypes>
{
    Animator animator;

    DamageResistances myResistances = null;

    public DamageResistances[] allResistances;

    public int maxHealth;
    [SerializeField] int currentHealth;
    int actualDamage;
    readonly int DestroyBool = Animator.StringToHash("Destroy");

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;

        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 2:
                myResistances = allResistances[0];
                break;
            case 1:
                myResistances = allResistances[1];
                break;
            case 0:
                myResistances = allResistances[2];
                break;
            default:
                myResistances = null;
                break;
        }
    }

    public void TakeDamage(int damageTaken, DamageTypes damageType)
    {
        PlayerManager.instance.mainCamShake.Shake(1, 0.1f);
        PlayerManager.instance.lockOnShake.Shake(1, 0.1f);
        PlayerManager.instance.hitStop.Stop(0.1f);

        actualDamage = myResistances.CalculateDamageWithResistance(damageTaken, damageType);
        currentHealth -= actualDamage;

        if (currentHealth <= 0)
        {
            animator.SetBool(DestroyBool, true);
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}