using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class TankEnemy : MonoBehaviour, IDamageable<int, DamageTypes>, IKillable
{
    [Header("Variables")]
    public float maxHealth;
    public float turnSmoothTime;
    public float lookRadius;
    public float startWaitTime;
    public float knockbackAmount = 8;
    [SerializeField]float currentHealth;
    float waitTime;

    public int lightAttackDamage;
    public int heavyAttackDamage;
    int randomWaypoint;
    int actualDamage;
    readonly int DieBool = Animator.StringToHash("isDead");

    public Transform[] waypoints;
    public Transform attackOrigin;
    [HideInInspector] public Transform target;
    public Transform damageTextPos;

    public DamageTypes myDamageType;
    public DamageResistances myResistances;

    readonly string damageText = "Damage Text";

    ObjectPooler objectPooler;

    public LayerMask whatIsEnemy;

    [Header("Components")]
    [HideInInspector] public NavMeshAgent agent;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        objectPooler = ObjectPooler.Instance;

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

    public void Attack(Transform attackOrigin, float attackRadius, LayerMask whatIsEnemy, int damage)
    {
        Collider[] enemiesHit = Physics.OverlapSphere(attackOrigin.position, attackRadius, whatIsEnemy);

        foreach (Collider enemy in enemiesHit)
        {
            enemy.GetComponent<IDamageable<int, DamageTypes>>().TakeDamage(damage, myDamageType);

            //Vector3 dir = transform.position - enemy.transform.position;
            //dir.y = 0;

            //Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
            //enemyRb.velocity = Vector3.zero;
            //enemyRb.velocity = -dir.normalized * knockbackAmount;
        }
    }

    public void TakeDamage(int damageTaken, DamageTypes damageType)
    {
        PlayerManager.instance.mainCamShake.Shake(1, 0.1f);
        PlayerManager.instance.lockOnShake.Shake(1, 0.1f);
        PlayerManager.instance.hitStop.Stop(0.1f);

        actualDamage = myResistances.CalculateDamageWithResistance(damageTaken, damageType);

        ShowDamageText();

        currentHealth -= actualDamage;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
