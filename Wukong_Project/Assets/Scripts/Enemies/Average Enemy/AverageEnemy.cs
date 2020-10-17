using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class AverageEnemy : MonoBehaviour, IDamageable<int, DamageTypes>, IKillable
{
    [Header("Variables")]
    public float maxHealth;
    public float turnSmoothTime;
    public float lookRadius;
    public float startWaitTime;
    public float retreatDistance;
    public float startTimeBetweenShots;
    public float knockbackAmount = 8;
    float retreatSpeed = 0.05f;
    float currentHealth;
    float waitTime;
    float timeBetweenShots;

    public int projectileDamage;
    public int slamDamage;
    int randomWaypoint;
    int actualDamage;
    readonly int HurtTrigger = Animator.StringToHash("Hurt");
    readonly int DieBool = Animator.StringToHash("isDead");

    bool knockback;

    public Transform[] waypoints;
    public Transform projectileOrigin;
    [HideInInspector] public Transform target;
    public Transform damageTextPos;

    public string myProjectile;

    readonly string damageText = "Damage Text";

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

        randomWaypoint = Random.Range(0, waypoints.Length);
        waitTime = startWaitTime;
        timeBetweenShots = startTimeBetweenShots;
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

    public void Attack(Transform attackPosition, float radius, LayerMask whatIsEnemy, int damage)
    {
        Collider[] enemiesHit = Physics.OverlapSphere(attackPosition.position, radius, whatIsEnemy);

        foreach (Collider enemy in enemiesHit)
        {
            enemy.GetComponent<IDamageable<int, DamageTypes>>().TakeDamage(damage, myDamageType);

            Vector3 dir = transform.forward;
            StartCoroutine(enemy.GetComponent<PlayerMovement>().PlayerKnockback(dir, knockbackAmount));
        }
    }

    //public void CheckForEnemiesHit(int damage)
    //{
    //    Collider[] enemiesHit = Physics.OverlapSphere(transform.TransformPoint(attackPositionOffsetFromPlayerCenter), attackRadius, enemyMask);

    //    foreach (Collider enemy in enemiesHit)
    //    {
    //        enemy.GetComponent<IDamageable<int, DamageTypes>>().TakeDamage(damage, elementalFormsScript.currentDamageType);
    //    }
    //}

    public void Shoot()
    {
        if(timeBetweenShots <= 0)
        {
            objectPooler.SpawnFromPool(myProjectile, projectileOrigin.position, Quaternion.identity);
            timeBetweenShots = startTimeBetweenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
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
            int rand = Random.Range(0, 3);
            string randomOrb = null;

            switch (rand)
            {
                case 2:
                    randomOrb = "Special Orb";
                    break;
                case 1:
                    randomOrb = "Rage Orb";
                    break;
                case 0:
                    randomOrb = "Health Orb";
                    break;
                default:
                    randomOrb = null;
                    break;
            }
            objectPooler.SpawnFromPool(randomOrb, transform.position + new Vector3(Random.Range(-1, 1), 2, Random.Range(-1, 1)), Quaternion.identity);
        }
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
