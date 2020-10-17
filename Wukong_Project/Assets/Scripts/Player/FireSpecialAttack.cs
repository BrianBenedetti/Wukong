using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpecialAttack : MonoBehaviour
{
    public int damage;
    public int damageRateInSeconds;
    public int attackLifespan;

    bool continueCoroutine;

    readonly DamageTypes myDamageType = DamageTypes.fire;

    readonly string enemyTag = "Enemy";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            //gets interface from collision
            IDamageable<int, DamageTypes> takeDamage = other.gameObject.GetComponent<IDamageable<int, DamageTypes>>();
            //checks if it exists
            if (takeDamage != null)
            {
                //starts coroutine
                continueCoroutine = true;
                StartCoroutine(DamageOverTime(takeDamage));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            //gets interface from collision
            IDamageable<int, DamageTypes> takeDamage = other.gameObject.GetComponent<IDamageable<int, DamageTypes>>();
            //checks if it exists
            if (takeDamage != null)
            {
                //stops coroutine
                continueCoroutine = false;
                StopCoroutine(DamageOverTime(takeDamage));
            }
        }
    }

    private void Start()
    {
        StartCoroutine(TimerToDestruction(attackLifespan));
    }

    IEnumerator DamageOverTime(IDamageable<int, DamageTypes> damageable)
    {
        while (continueCoroutine)
        {
            //calls the damage function on the interface
            damageable.TakeDamage(damage, myDamageType);
            //waits for a certain amount of time to apply damage again
            yield return new WaitForSeconds(damageRateInSeconds);
        }
    }

    IEnumerator TimerToDestruction(float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
    }
}
