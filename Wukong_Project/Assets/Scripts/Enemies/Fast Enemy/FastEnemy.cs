using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System.Collections.Generic;

public class FastEnemy : MonoBehaviour, IDamageable<int, DamageTypes>, IKillable
{
    [Header("Variables")]
    public float maxHealth;
    public float turnSmoothTime;
    public float lookRadius;
    public float knockbackAmount = 5;
    public float attackRadius;
    public float dashForce = 50;
    public float dashDuration = 0.1f;
    float currentHealth;

    public int lightAttackDamage;
    public int heavyAttackDamage;
    int randomWaypoint = -1;
    int actualDamage;
    readonly int HurtTrigger = Animator.StringToHash("Hurt");
    readonly int DieBool = Animator.StringToHash("isDead");
    readonly int IdleBool = Animator.StringToHash("isIdle");

    public int[] lootTable ={
        50, //health
        35, //mana
        15 //rage
    };

    int lootTotalTally = 0;

    bool knockback;

    public Transform[] waypoints;
    public Transform attackOrigin;
    [HideInInspector] public Transform target;
    public Transform damageTextPos;

    public DamageTypes myDamageType;
    public DamageResistances myResistances;

    readonly string damageText = "Damage Text";
    public List<string> lootOrbs = new List<string>();

    ObjectPooler objectPooler;

    public LayerMask whatIsEnemy;

    [Header("Components")]
    [HideInInspector] public NavMeshAgent agent;

    Animator animator;    

    void Start()
    {
        currentHealth = maxHealth;
        knockback = false;
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        objectPooler = ObjectPooler.Instance;

        foreach(int i in lootTable)
        {
            lootTotalTally += i;
        }
    }

    private void FixedUpdate()
    {
        if (knockback)
        {
            Vector3 direction = transform.position - target.position;
            direction.y = 0;

            agent.velocity = direction.normalized * knockbackAmount;
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

    public void StartPatrol()
    {
        agent.ResetPath();

        if (randomWaypoint < 0)
        {
            randomWaypoint = Random.Range(0, waypoints.Length);
        }
        else
        {
            randomWaypoint = (randomWaypoint + 1) % waypoints.Length;
        }
        agent.SetDestination(waypoints[randomWaypoint].position);
    }

    public void CheckPatrol()
    {
        if (Vector3.Distance(transform.position, waypoints[randomWaypoint].position) <= 1)
        {
            animator.SetBool(IdleBool, true);
        }
    }

    public void Attack(int damage)
    {
        Collider[] enemiesHit = Physics.OverlapSphere(attackOrigin.position, attackRadius, whatIsEnemy);

        Vector3 dir = transform.forward;

        foreach (Collider enemy in enemiesHit)
        {
            enemy.GetComponent<IDamageable<int, DamageTypes>>().TakeDamage(damage, myDamageType);
            StartCoroutine(enemy.GetComponent<PlayerMovement>().PlayerKnockback(dir, knockbackAmount));
        }
    }

    public void TakeDamage(int damageTaken, DamageTypes damageType)
    {
        actualDamage = myResistances.CalculateDamageWithResistance(damageTaken, damageType);
        currentHealth -= actualDamage;

        if (actualDamage > 10)
        {
            animator.SetTrigger(HurtTrigger);
            StartCoroutine(Knockback());
            PlayerManager.instance.mainCamShake.Shake(1, 0.1f);
            PlayerManager.instance.lockOnShake.Shake(1, 0.1f);
            PlayerManager.instance.hitStop.Stop(0.1f);
        }

        ShowDamageText();

        if (currentHealth <= 0)
        {
            animator.SetBool(DieBool, true);
        }
    }

    void ShowDamageText()
    {
        var obj = objectPooler.SpawnFromPool(damageText, damageTextPos.position, Quaternion.identity);
        obj.GetComponent<TextMeshPro>().text = actualDamage.ToString();
    }

    void DropLoot()
    {
        for (int i = 0; i < maxHealth / 50; i++)
        {
            int randomNumber = Random.Range(0, lootTotalTally + 1);

            for (int j = 0; j < lootTable.Length; j++)
            {
                if(randomNumber <= lootTable[j])
                {
                    objectPooler.SpawnFromPool(lootOrbs[j].ToString(), transform.position + new Vector3(Random.Range(-1, 1), 2, Random.Range(-1, 1)), Quaternion.identity);
                    break;
                }
                else
                {
                    randomNumber -= lootTable[j];
                }
            }
        }
    }

    public IEnumerator Dodge()
    {
        agent.isStopped = true;

        yield return new WaitForSeconds(dashDuration);

        agent.isStopped = false;
    }

    public IEnumerator Die()
    {
        //play dissolve shader effect
        //instantiate particle effect
        GetComponent<Collider>().enabled = false;
        agent.enabled = false;
        PlayerManager.instance.lockOnSystem.KilledOpponent(gameObject);
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
