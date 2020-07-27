using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BullDemonKing : MonoBehaviour, IDamageable<int/*, DamageTypes*/>, IKillable
{
    [Header("Variables")]
    public float maxHealth;
    public float turnSmoothTime;
    public float leapHeight;
    public float leapDuration;
    float leapCurrentTime;
    float currentHealth;

    public bool isVulnerable;

    public int lightAttackDamage;
    public int heavyAttackDamage;
    public int leapAttackDamage;

    [HideInInspector] public Transform target;
    public Transform spawn1;
    public Transform spawn2;
    public Transform attackPosition;

    [HideInInspector] public Vector3 leapDestination;
    [HideInInspector] public Vector3 leapStart;

    public GameObject[] averageEnemies;
    public GameObject[] fastEnemies;

    public DamageTypes myDamageType;

    //public DamageResistance myResistances;

    [Header("Components")]
    [HideInInspector] public NavMeshAgent agent;

    Animator animator;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //checks if health is half to rage
        if(currentHealth <= (maxHealth / 2))
        {
            animator.SetBool("isEnraged", true);
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
        
        //Instantiate(averageEnemies[rand1], spawn1.position, Quaternion.identity);
        //Instantiate(fastEnemies[rand2], spawn2.position, Quaternion.identity);
    }

    public void Attack(Transform attackPosition, float radius, LayerMask whatIsEnemy)
    {
        Collider[] enemiesHit = Physics.OverlapSphere(attackPosition.position, radius, whatIsEnemy);

        foreach (Collider enemy in enemiesHit)
        {
            enemy.GetComponent<IDamageable<int/*, DamageTypes*/>>().TakeDamage(lightAttackDamage); //must include damage type
            //enemy.GetComponent<Animator>().SetTrigger("Hurt");
        }
    }

    public void TakeDamage(int damageTaken)
    {
        if(currentHealth > (maxHealth / 2))
        {
            animator.SetTrigger("Hurt");
        }

        currentHealth -= damageTaken; //this will change to implement resitsances
        if (currentHealth <= 0)
        {
            animator.SetBool("isDead", true);
        }
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
        animator.SetTrigger("Land");
    }

    public IEnumerator Die()
    {
        GetComponent<Collider>().enabled = false;
        agent.enabled = false;
        yield return new WaitForSeconds(2);
        //initiate a cutscene or something
    }
}
