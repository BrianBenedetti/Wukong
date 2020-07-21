using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable<int, DamageTypes>, IKillable
{
    public int maxHealth = 100;
    int currentHealth;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage, DamageTypes damageType)
    {
        currentHealth -= damage;
        //play hurt animation

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log(this.gameObject.name + " died");

        //die animation bool to true

        //disable enemy
        GetComponent<Collider>().enabled = false;
        rb.isKinematic = true;
        this.enabled = false;


    }
}
