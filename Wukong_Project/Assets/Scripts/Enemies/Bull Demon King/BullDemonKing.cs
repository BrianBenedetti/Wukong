using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class BullDemonKing : MonoBehaviour, IDamageable<int, DamageTypes>, IKillable
{
    [Header("Variables")]
    public float maxHealth;
    public float turnSmoothTime;
    public float leapHeight;
    public float leapDuration;
    public float knockbackAmount = 10;
    float leapCurrentTime;
    float currentHealth;

    public bool isVulnerable;

    public int lightAttackDamage;
    public int heavyAttackDamage;
    public int leapAttackDamage;
    int actualDamage;
    readonly int LandTrigger = Animator.StringToHash("Land");
    readonly int HurtTrigger = Animator.StringToHash("Hurt");
    readonly int DieBool = Animator.StringToHash("isDead");
    readonly int RageBool = Animator.StringToHash("isEnraged");

    [HideInInspector] public Transform target;
    public Transform spawn1;
    public Transform spawn2;
    public Transform attackPosition;
    public Transform damageTextPos;

    [HideInInspector] public Vector3 leapDestination;
    [HideInInspector] public Vector3 leapStart;

    public GameObject[] averageEnemies;
    public GameObject[] fastEnemies;
    public GameObject shieldPrefab;

    ObjectPooler objectPooler;

    public LayerMask whatIsEnemy;

    readonly string damageText = "Damage Text";

    public DamageTypes myDamageType;

    public DamageResistances myResistances;

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
    }

    // Update is called once per frame
    void Update()
    {
        //checks if health is half to rage
        if(currentHealth <= (maxHealth / 2))
        {
            animator.SetBool(RageBool, true);
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

    public void Rage()
    {
        agent.speed = 5;

        lightAttackDamage = 60;
        heavyAttackDamage = 75;
    }

    public void SummonMinions()
    {
        int rand1 = Random.Range(0, averageEnemies.Length);
        int rand2 = Random.Range(0, fastEnemies.Length);

        Instantiate(averageEnemies[rand1], spawn1.position, Quaternion.identity);
        Instantiate(fastEnemies[rand2], spawn2.position, Quaternion.identity);
    }

    public void Attack(Transform attackPosition, float radius, LayerMask whatIsEnemy, int damage)
    {
        Collider[] enemiesHit = Physics.OverlapSphere(attackPosition.position, radius, whatIsEnemy);

        foreach (Collider enemy in enemiesHit)
        {
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

>>>>>>> 64429ae0c6f70c4ba99a9f0d1069938577b46d9a
    public void TakeDamage(int damageTaken, DamageTypes damageType)
    {
        if (isVulnerable)
        {
            PlayerManager.instance.mainCamShake.Shake(1, 0.1f);
            PlayerManager.instance.lockOnShake.Shake(1, 0.1f);
            PlayerManager.instance.hitStop.Stop(0.1f);

            actualDamage = myResistances.CalculateDamageWithResistance(damageTaken, damageType);
            currentHealth -= actualDamage;

            ShowDamageText();

            if (currentHealth > (maxHealth / 2))
            {
                animator.SetTrigger(HurtTrigger);
            }

            if (currentHealth <= 0)
            {
                animator.SetBool(DieBool, true);
            }
        }
    }

    public void CreateShield()
    {
        Vector3 shieldSpawnPos = new Vector3(transform.position.x, 1.5f, transform.position.z);
        Instantiate(shieldPrefab, shieldSpawnPos, Quaternion.identity, transform);
    }

    void ShowDamageText()
    {
        var obj = objectPooler.SpawnFromPool(damageText, damageTextPos.position, Quaternion.identity);
        obj.GetComponent<TextMeshPro>().text = actualDamage.ToString();
    }

    public IEnumerator LeapAttack()
    {
        while (leapCurrentTime < leapDuration)
        {
            leapCurrentTime += Time.deltaTime;

            transform.position = MathParabola.Parabola(leapStart, leapDestination, leapHeight, leapCurrentTime / leapDuration);

            yield return null;
        }

        leapCurrentTime = 0;
        transform.position = leapDestination;
        animator.SetTrigger(LandTrigger);
    }

    public IEnumerator Die()
    {
        GetComponent<Collider>().enabled = false;
        agent.enabled = false;
<<<<<<< HEAD
=======
        PlayerManager.instance.lockOnSystem.KilledOpponent(gameObject);
>>>>>>> 64429ae0c6f70c4ba99a9f0d1069938577b46d9a
        yield return new WaitForSeconds(2);
        //initiate a cutscene or something
    }
}
