using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSpecialAttack : MonoBehaviour
{
    public int damage;

    public float attackLifespan;
    public float speed;

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
        StartCoroutine(TimerToDestruction(attackLifespan));

        playerCombatScript = PlayerManager.instance.player.GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        Expand();
    }

    public void Expand()
    {
        transform.localScale += new Vector3(1, 1, 1) * speed * Time.deltaTime;
    }

    IEnumerator TimerToDestruction(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }
}
