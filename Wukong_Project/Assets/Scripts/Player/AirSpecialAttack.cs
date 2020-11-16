using System.Collections;
using UnityEngine;

public class AirSpecialAttack : MonoBehaviour
{
    public int damage;

    public float attackLifespan;

    readonly string enemyTag = "Enemy";

    readonly DamageTypes myDamageType = DamageTypes.air;

    PlayerCombat playerCombatScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            //gets interface from collision
            IDamageable<int, DamageTypes> damageable = other.gameObject.GetComponent<IDamageable<int, DamageTypes>>();
            //checks if it exists
            if (damageable != null)
            {
                damageable.TakeDamage(damage, myDamageType);
                //adds to combo counter
                playerCombatScript.comboHits++;
                playerCombatScript.lastTimeHit = Time.time;
            }
        }
    }

    public void Start()
    {
        StartCoroutine(TimerToDestruction(attackLifespan + 0.2f));

        StartCoroutine(IncreaseSizeOverTime(attackLifespan));

        playerCombatScript = PlayerManager.instance.player.GetComponent<PlayerCombat>();
    }

    IEnumerator IncreaseSizeOverTime(float time)
    {
        float timePassed = 0;

        Vector3 currentSizeAtStart = transform.localScale;

        while (timePassed < 1)
        {
            timePassed += Time.deltaTime / time;

            transform.localScale = Vector3.Lerp(currentSizeAtStart, new Vector3(10, 10, 10), timePassed);

            yield return null;
        }

        transform.localScale = new Vector3(10,10,10);
    }

    IEnumerator TimerToDestruction(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }
}
