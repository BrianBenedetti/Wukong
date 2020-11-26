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

    public GameObject ashes;

    public Transform[] waypoints;
    public Transform projectileOrigin;
    [HideInInspector] public Transform target;
    public Transform damageTextPos;
    public Transform hitSpawn;


    public string myProjectile;

    readonly string damageText = "Damage Text";
    public List<string> lootOrbs = new List<string>();

    public LayerMask whatIsEnemy;

    public DamageTypes myDamageType;

    public DamageResistances myResistances;

    [Header("Components")]
    [HideInInspector] public NavMeshAgent agent;

    Animator animator;
    AudioSource source;

    public AudioClip death;

    public SkinnedMeshRenderer body;
    public SkinnedMeshRenderer hair1;
    public SkinnedMeshRenderer hair2;
    public SkinnedMeshRenderer hair3;
    public SkinnedMeshRenderer hair4;
    public SkinnedMeshRenderer hair5;
    public SkinnedMeshRenderer hair6;
    public SkinnedMeshRenderer hair7;
    public SkinnedMeshRenderer hair8;
    public SkinnedMeshRenderer hair9;
    public MeshRenderer mask;

    void Start()
    {
        currentHealth = maxHealth;
        knockback = false;
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        ashes.SetActive(false);

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

        PlaySlamVFX();

        foreach (Collider enemy in enemiesHit)
        {
            enemy.GetComponent<IDamageable<int, DamageTypes>>().TakeDamage(slamDamage, myDamageType);
            StartCoroutine(enemy.GetComponent<PlayerMovement>().PlayerKnockback(dir, knockbackAmount));
        }
    }

    public void Shoot()
    {
        ObjectPooler.Instance.SpawnFromPool(myProjectile, projectileOrigin.position, Quaternion.identity);
    }

    public void TakeDamage(int damageTaken, DamageTypes damageType)
    {
        actualDamage = myResistances.CalculateDamageWithResistance(damageTaken, damageType);
        currentHealth -= actualDamage;

        if (actualDamage > 10)
        {
            animator.SetTrigger(HurtTrigger);
            StartCoroutine(Knockback());
            PlayerManager.instance.mainCamShake.Shake(2, 0.1f);
            //PlayerManager.instance.lockOnShake.Shake(2, 0.1f);
            PlayerManager.instance.hitStop.Stop(0.2f);
        }

        ShowDamageText();
        ObjectPooler.Instance.SpawnFromPool("Hit Effect", hitSpawn.position, Quaternion.identity);

        if (currentHealth <= 0)
        {
            animator.SetBool(DieBool, true);
        }
    }

    void ShowDamageText()
    {
        var obj = ObjectPooler.Instance.SpawnFromPool(damageText, damageTextPos.position, Quaternion.identity);
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
                    ObjectPooler.Instance.SpawnFromPool(lootOrbs[j].ToString(),
                        transform.position + new Vector3(Random.Range(-1, 1), 2,
                        Random.Range(-1, 1)), Quaternion.identity);
                    break;
                }
                else
                {
                    randomNumber -= lootTable[j];
                }
            }
        }
    }

    public void PlaySlamVFX()
    {
        var obj = ObjectPooler.Instance.SpawnFromPool("Slam Effect", transform.position, Quaternion.identity);
        obj.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }

    public IEnumerator Die()
    {
        //play dissolve shader effect
        StartCoroutine(LerpDeathShader(1.9f));
        //instantiate particle effect
        ashes.SetActive(true);
        source.PlayOneShot(death);
        GetComponent<Collider>().enabled = false;
        agent.enabled = false;
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

    IEnumerator LerpDeathShader(float time)
    {
        float timePassed = 0;

        float currentValueAtStart = body.material.GetFloat("_DissolveCutoff");

        while (timePassed < 1)
        {
            timePassed += Time.deltaTime / time;
            body.material.SetFloat("_DissolveCutoff", Mathf.Lerp(currentValueAtStart, -10, timePassed));
            hair1.material.SetFloat("_DissolveCutoff", Mathf.Lerp(currentValueAtStart, -10, timePassed));
            hair2.material.SetFloat("_DissolveCutoff", Mathf.Lerp(currentValueAtStart, -10, timePassed));
            hair3.material.SetFloat("_DissolveCutoff", Mathf.Lerp(currentValueAtStart, -10, timePassed));
            hair4.material.SetFloat("_DissolveCutoff", Mathf.Lerp(currentValueAtStart, -10, timePassed));
            hair5.material.SetFloat("_DissolveCutoff", Mathf.Lerp(currentValueAtStart, -10, timePassed));
            hair6.material.SetFloat("_DissolveCutoff", Mathf.Lerp(currentValueAtStart, -10, timePassed));
            hair7.material.SetFloat("_DissolveCutoff", Mathf.Lerp(currentValueAtStart, -10, timePassed));
            hair8.material.SetFloat("_DissolveCutoff", Mathf.Lerp(currentValueAtStart, -10, timePassed));
            hair9.material.SetFloat("_DissolveCutoff", Mathf.Lerp(currentValueAtStart, -10, timePassed));
            mask.material.SetFloat("_DissolveCutoff", Mathf.Lerp(currentValueAtStart, -10, timePassed));



            yield return null;
        }

        body.material.SetFloat("_DissolveCutoff", -10);
        hair1.material.SetFloat("_DissolveCutoff", -10);
        hair2.material.SetFloat("_DissolveCutoff", -10);
        hair3.material.SetFloat("_DissolveCutoff", -10);
        hair4.material.SetFloat("_DissolveCutoff", -10);
        hair5.material.SetFloat("_DissolveCutoff", -10);
        hair6.material.SetFloat("_DissolveCutoff", -10);
        hair7.material.SetFloat("_DissolveCutoff", -10);
        hair8.material.SetFloat("_DissolveCutoff", -10);
        hair9.material.SetFloat("_DissolveCutoff", -10);
        mask.material.SetFloat("_DissolveCutoff", -10);

    }
}
