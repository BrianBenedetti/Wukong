using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Stats", menuName = "Scriptable Objects/Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    public int maxHealth;
    public int currentHealth;
    public int lightAttackDamage;
    public int heavyAttackDamage;

    public float speed;
    public float dashTime;
    public float dashSpeed;
    public float turnSmoothTime;
    public float lookRadius;

    public bool isAggro;
    public bool isDead;

    public Vector3 attackRange;

    public Transform attackOrigin;
    public Transform target;

    public DamageTypes myDamageType;

    public LayerMask whatToDamage;

    //public DamageResistance myResistances;
}
