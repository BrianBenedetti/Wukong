using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

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
    int actualDamage;

    bool knockback;

    public Transform[] waypoints;
    public Transform attackOrigin;
    [HideInInspector] public Transform target;
    public Transform damageTextPos;

    public DamageTypes myDamageType;
    public DamageResistances myResistances;

    public GameObject damageText;
    public GameObject[] loot;

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

            Vector3 dir = transform.position - enemy.transform.position;
            dir.y = 0;

            Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
            enemyRb.velocity = Vector3.zero;
            enemyRb.velocity = -dir.normalized * 5;
        }
    }

    public void TakeDamage(int damageTaken, DamageTypes damageType)
    {
        PlayerManager.instance.mainCamShake.Shake(1, 0.1f);
        PlayerManager.instance.lockOnShake.Shake(1, 0.1f);

        StartCoroutine(Knockback());

        animator.SetTrigger("Hurt");

        actualDamage = myResistances.CalculateDamageWithResistance(damageTaken, damageType);
        currentHealth -= actualDamage;

        if (damageText)
        {
            ShowDamageText();
        }

        if (currentHealth <= 0)
        {
            animator.SetBool("isDead", true);
        }
    }

    void ShowDamageText()
    {
        var obj = Instantiate(damageText, damageTextPos.position, Quaternion.identity, transform);
        obj.GetComponent<TextMeshPro>().text = actualDamage.ToString();
    }

    void DropLoot()
    {
        for (int i = 0; i < maxHealth / 50; i++)
        {
            int rand = Random.Range(0, 3);

            Instantiate(loot[rand], transform.position + new Vector3(Random.Range(-1, 1),
                2,
                Random.Range(-1, 1)),
                Quaternion.identity);
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
        DropLoot();
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
