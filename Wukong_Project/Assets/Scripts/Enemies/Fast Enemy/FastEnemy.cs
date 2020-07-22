﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FastEnemy : MonoBehaviour, IDamageable<int/*, DamageTypes*/>, IKillable
{
    [Header("Variables")]
    public EnemyStats myStats;

    float distanceToTarget;
    float waitTime;
    public float startWaitTime;
    public float dashForce;
    public float dashDuration;

    int randomWaypoint;

    public Transform[] waypoints;


    [Header("Components")]
    [HideInInspector] public NavMeshAgent agent;

    Animator animator;

    Rigidbody rb;
    

    void Start()
    {
        myStats.currentHealth = myStats.maxHealth;
        myStats.target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        randomWaypoint = Random.Range(0, waypoints.Length);
        waitTime = startWaitTime;
    }

    public void faceTarget()
    {
        Vector3 direction = (myStats.target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * myStats.turnSmoothTime);
    }

    public void ChaseTarget()
    {
        agent.SetDestination(myStats.target.position);
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

    public void Attack(Vector3 attackPosition, Vector3 attackRange, Quaternion rotation, LayerMask whatIsEnemy)
    {
        Collider[] enemiesHit = Physics.OverlapBox(attackPosition, attackRange, rotation, whatIsEnemy);

        foreach (Collider enemy in enemiesHit)
        {
            enemy.GetComponent<IDamageable<int/*, DamageTypes*/>>().TakeDamage(myStats.lightAttackDamage); //must include damage type
            //enemy.GetComponent<Animator>().SetTrigger("Hurt");
        }
    }

    public void TakeDamage(int damageTaken/*, DamageTypes damageType*/)
    {
        animator.SetTrigger("Hurt");
        myStats.currentHealth -= damageTaken; //this will change to implement resitsances
        if (myStats.currentHealth <= 0)
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
        Gizmos.DrawWireSphere(transform.position, myStats.lookRadius);
    }
}
