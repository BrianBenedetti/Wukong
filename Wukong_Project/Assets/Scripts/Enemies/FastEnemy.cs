using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FastEnemy : MonoBehaviour, IDamageable<int, DamageTypes>, IKillable
{
    [Header("Variables")]
    public EnemyStats myStats;

    float distanceToTarget;

    [Header("Components")]
    NavMeshAgent agent;

    Rigidbody rb;
    

    void Start()
    {
        myStats.currentHealth = myStats.maxHealth;
        myStats.target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    public void faceTarget()
    {
        Vector3 direction = (myStats.target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * myStats.turnSmoothTime);
    }

    public void ChaseTarget()
    {
        agent.SetDestination(myStats.target.position);
    }

    public void Attack(Vector3 attackPosition, Vector3 attackRange, Quaternion rotation, LayerMask whatIsEnemy)
    {
        Collider[] enemiesHit = Physics.OverlapBox(attackPosition, attackRange, rotation, whatIsEnemy);

        foreach (Collider enemy in enemiesHit)
        {
            //enemy.GetComponent<IDamageable<int, DamageTypes>>().TakeDamage(primaryAttackDamage); //must include damage type
        }
    }

    public void Die()
    {
        myStats.isDead = true;
        //disable navmesh
        //play shader effect
        //destroy gameobject after a while
    }

    public void TakeDamage(int damageTaken, DamageTypes damageType)
    {
        //set trigger to hurt
        myStats.currentHealth -= damageTaken; //this will change to implement resitsances
        if (myStats.currentHealth <= 0)
        {
            Die();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, myStats.lookRadius);
    }
}
