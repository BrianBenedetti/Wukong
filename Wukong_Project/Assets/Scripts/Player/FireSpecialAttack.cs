using System.Collections;
using UnityEngine;

public class FireSpecialAttack : MonoBehaviour
{
    public int damage;
    public int damageRateInSeconds;
    public int attackLifespan;

    bool continueCoroutine;

    readonly DamageTypes myDamageType = DamageTypes.fire;

    readonly string enemyTag = "Enemy";

    PlayerCombat playerCombatScript;

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

    public void Start()
    {
        StartCoroutine(TimerToDestruction(attackLifespan));

        playerCombatScript = PlayerManager.instance.player.GetComponent<PlayerCombat>();
    }

    IEnumerator DamageOverTime(IDamageable<int, DamageTypes> damageable)
    {
        while (continueCoroutine)
        {
            //calls the damage function on the interface
            damageable.TakeDamage(damage, myDamageType);
            //adds to combo counter
            playerCombatScript.comboHits++;
            playerCombatScript.lastTimeHit = Time.time;
            //waits for a certain amount of time to apply damage again
            yield return new WaitForSeconds(damageRateInSeconds);
        }
    }

    IEnumerator TimerToDestruction(int time)
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }
}
