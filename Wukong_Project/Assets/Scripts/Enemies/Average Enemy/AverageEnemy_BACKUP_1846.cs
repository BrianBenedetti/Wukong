using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System.Collections.Generic;

public class AverageEnemy : MonoBehaviour, IDamageable<int, DamageTypes>, IKillable
{
    [Header("Variables")]
    public float maxHealth;
    public float turnSmoothTime;
    public float lookRadius;
    public float retreatDistance;
    public float knockbackAmount = 8;
    public float slamRadius;
    float retreatSpeed = 0.05f;
    float currentHealth;

    public int projectileDamage;
    public int slamDamage;
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
    public Transform projectileOrigin;
    [HideInInspector] public Transform target;
    public Transform damageTextPos;

    public string myProjectile;

    readonly string damageText = "Damage Text";
    public List<string> lootOrbs = new List<string>();

    ObjectPooler objectPooler;

    public LayerMask whatIsEnemy;

    public DamageTypes myDamageType;

    public DamageResistances myResistances;

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

        foreach (int i in lootTable)
        {
            lootTotalTally += i;
        }
    }

    private void FixedUpdate()
    {
        if (agent.isActiveAndEnabled && knockback)
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

    public void Retreat()
    {
        agent.Move((transform.position - target.position).normalized * retreatSpeed);
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

    public void Slam()
    {
        Collider[] enemiesHit = Physics.OverlapSphere(transform.position, slamRadius, whatIsEnemy);

        Vector3 dir = transform.forward;

        foreach (Collider enemy in enemiesHit)
        {
<<<<<<< HEAD
            enemy.GetComponent<IDamageable<int, DamageTypes>>().TakeDamage(damage, myDamageType);

<<<<<<< HEAD
            Vector3 dir = transform.position - enemy.transform.position;
            dir.y = 0;

            Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
            enemyRb.velocity = Vector3.zero;
            enemyRb.velocity = -dir.normalized * knockbackAmount;
        }
    }

=======
            Vector3 dir = transform.forward;
=======
            enemy.GetComponent<IDamageable<int, DamageTypes>>().TakeDamage(slamDamage, myDamageType);
>>>>>>> 4a65a347e482b55c82be7e734f0434bb6150b2ed
            StartCoroutine(enemy.GetComponent<PlayerMovement>().PlayerKnockback(dir, knockbackAmount));
        }
    }

<<<<<<< HEAD
    //public void CheckForEnemiesHit(int damage)
    //{
    //    Collider[] enemiesHit = Physics.OverlapSphere(transform.TransformPoint(attackPositionOffsetFromPlayerCenter), attackRadius, enemyMask);

    //    foreach (Collider enemy in enemiesHit)
    //    {
    //        enemy.GetComponent<IDamageable<int, DamageTypes>>().TakeDamage(damage, elementalFormsScript.currentDamageType);
    //    }
    //}

>>>>>>> 64429ae0c6f70c4ba99a9f0d1069938577b46d9a
=======
>>>>>>> 4a65a347e482b55c82be7e734f0434bb6150b2ed
    public void Shoot()
    {
        objectPooler.SpawnFromPool(myProjectile, projectileOrigin.position, Quaternion.identity);
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
                if (randomNumber <= lootTable[j])
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

    public IEnumerator Die()
    {
        //play dissolve shader effect
        //instantiate particle effect
        GetComponent<Collider>().enabled = false;
        agent.enabled = false;
<<<<<<< HEAD
=======
        PlayerManager.instance.lockOnSystem.KilledOpponent(gameObject);
>>>>>>> 64429ae0c6f70c4ba99a9f0d1069938577b46d9a
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
