using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FastEnemy : MonoBehaviour, IDamageable<int, DamageTypes>, IKillable
{
    [Header("Variables")]
    public float maxHealth;
    public float turnSmoothTime;
    public float lookRadius;
    public float startWaitTime;
    public float dashForce;
    public float dashDuration;
    float currentHealth;
    float waitTime;

    public int lightAttackDamage;
    public int heavyAttackDamage;
    int randomWaypoint;

    public Transform[] waypoints;
    public Transform attackOrigin;
    [HideInInspector] public Transform target;

    public DamageTypes myDamageType;
    public DamageResistances myResistances;


    [Header("Components")]
    [HideInInspector] public NavMeshAgent agent;

    Animator animator;

    Rigidbody rb;
    

    void Start()
    {
        currentHealth = maxHealth;
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        randomWaypoint = Random.Range(0, waypoints.Length);
        waitTime = startWaitTime;
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

    public void Attack(Transform attackOrigin, float attackRadius, LayerMask whatIsEnemy)
    {
        Collider[] enemiesHit = Physics.OverlapSphere(attackOrigin.position, attackRadius, whatIsEnemy);

        foreach (Collider enemy in enemiesHit)
        {
            enemy.GetComponent<IDamageable<int, DamageTypes>>().TakeDamage(lightAttackDamage, myDamageType);
            //enemy.GetComponent<Animator>().SetTrigger("Hurt");
        }
    }

    public void TakeDamage(int damageTaken, DamageTypes damageType)
    {
        CinemachineShake.Instance.Shake(1, 0.1f);

        animator.SetTrigger("Hurt");
        currentHealth -= myResistances.CalculateDamageWithResistance(damageTaken, damageType); //this will change to implement resitsances
        if (currentHealth <= 0)
        {
            animator.SetBool("isDead", true);
        }
    }

    public IEnumerator Dodge()
    {
        agent.isStopped = true;
        rb.isKinematic = false;
        rb.AddForce(-transform.forward * dashForce, ForceMode.VelocityChange);

        yield return new WaitForSeconds(dashDuration);
        rb.isKinematic = true;
        agent.isStopped = false;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
