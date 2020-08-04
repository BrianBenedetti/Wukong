using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AverageEnemy : MonoBehaviour, IDamageable<int, DamageTypes>, IKillable
{
    [Header("Variables")]
    public float maxHealth;
    public float turnSmoothTime;
    public float lookRadius;
    public float startWaitTime;
    public float retreatDistance;
    public float startTimeBetweenShots;
    float currentHealth;
    float waitTime;
    float timeBetweenShots;

    public int projectileDamage;
    public int slamDamage;
    int randomWaypoint;

    bool knockback;

    public Transform[] waypoints;
    public Transform projectileOrigin;
    [HideInInspector] public Transform target;

    public GameObject myProjectile;

    public DamageTypes myDamageType;

    public DamageResistances myResistances;

    [Header("Components")]
    [HideInInspector] public NavMeshAgent agent;

    Animator animator;

    Rigidbody rb;


    void Start()
    {
        currentHealth = maxHealth;
        knockback = false;
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        randomWaypoint = Random.Range(0, waypoints.Length);
        waitTime = startWaitTime;
        timeBetweenShots = startTimeBetweenShots;
    }

    private void FixedUpdate()
    {
        if (knockback)
        {
            Vector3 direction = transform.position - target.position;
            direction.y = 0;

            agent.velocity = direction.normalized * 5;
        }
    }

    public void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSmoothTime);
    }

    public void ChaseTarget()
    {
        agent.SetDestination(target.position);
    }

    public void Retreat()
    {
        agent.Move((transform.position - target.position).normalized * 0.05f);
    }

    public void Patrol()
    {
        agent.SetDestination(waypoints[randomWaypoint].position);
        if (Vector3.Distance(transform.position, waypoints[randomWaypoint].position) < agent.stoppingDistance)
        {
            if (waitTime <= 0)
            {
                randomWaypoint = Random.Range(0, waypoints.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    public void Attack(Transform attackPosition, float radius, LayerMask whatIsEnemy)
    {
        Collider[] enemiesHit = Physics.OverlapSphere(attackPosition.position, radius, whatIsEnemy);

        foreach (Collider enemy in enemiesHit)
        {
            enemy.GetComponent<IDamageable<int, DamageTypes>>().TakeDamage(projectileDamage, myDamageType);

            Vector3 dir = transform.position - enemy.transform.position;
            dir.y = 0;

            Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
            enemyRb.velocity = Vector3.zero;
            enemyRb.velocity = -dir.normalized * 8;
        }
    }

    public void Shoot()
    {
        if(timeBetweenShots <= 0)
        {
            Instantiate(myProjectile, projectileOrigin.position, Quaternion.identity);
            timeBetweenShots = startTimeBetweenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damageTaken, DamageTypes damageType)
    {
        PlayerManager.instance.mainCamShake.Shake(1, 0.1f);
        PlayerManager.instance.lockOnShake.Shake(1, 0.1f);

        StartCoroutine(Knockback());

        animator.SetTrigger("Hurt");
        currentHealth -= myResistances.CalculateDamageWithResistance(damageTaken, damageType);
        if (currentHealth <= 0)
        {
            animator.SetBool("isDead", true);
        }
    }

    public IEnumerator Die()
    {
        //play dissolve shader effect
        //instantiate particle effect
        GetComponent<Collider>().enabled = false;
        agent.enabled = false;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    IEnumerator Knockback()
    {
        knockback = true;
        agent.angularSpeed = 0;

        yield return new WaitForSeconds(0.1f);

        knockback = false;
        agent.angularSpeed = 120;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
