using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BullDemonKing : MonoBehaviour, IDamageable<int/*, DamageTypes*/>, IKillable
{
    [Header("Variables")]
    public float maxHealth;
    public float turnSmoothTime;
    float currentHealth;

    public int lightAttackDamage;
    public int heavyAttackDamage;
    public int leapAttackDamage;

    [HideInInspector] public Transform target;

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

    }

    public void SummonMinions()
    {

    }

    public void Attack(Transform attackPosition, float radius, LayerMask whatIsEnemy)
    {
        Collider[] enemiesHit = Physics.OverlapSphere(attackPosition.position, radius, whatIsEnemy);

        foreach (Collider enemy in enemiesHit)
        {
            enemy.GetComponent<IDamageable<int/*, DamageTypes*/>>().TakeDamage(projectileDamage); //must include damage type
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
        yield return new WaitForSeconds(1); //remove this
    }

    public IEnumerator Die()
    {
        GetComponent<Collider>().enabled = false;
        agent.enabled = false;
        yield return new WaitForSeconds(2);
        //initiate a cutscene or something
    }
}
