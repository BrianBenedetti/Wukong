using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System.Collections.Generic;

public class TankEnemy : MonoBehaviour, IDamageable<int, DamageTypes>, IKillable
{
    [Header("Variables")]
    public float maxHealth;
    public float turnSmoothTime;
    public float lookRadius;
    public float startWaitTime;
    public float knockbackAmount = 8;
    public float attackRadius;
    public float slamRadius;
    float currentHealth;
    float waitTime;

    public int lightAttackDamage;
    public int heavyAttackDamage;
    int randomWaypoint;
    int actualDamage;
    readonly int DieBool = Animator.StringToHash("isDead");
    readonly int IdleBool = Animator.StringToHash("isIdle");

    public int[] lootTable ={
        50, //health
        35, //mana
        15 //rage
    };

    int lootTotalTally = 0;

    public Transform[] waypoints;
    public Transform attackOrigin;
    [HideInInspector] public Transform target;
    public Transform damageTextPos;
    public Transform slashSpawn;
    public Transform attackSlamPos;
    

    public DamageTypes myDamageType;
    public DamageResistances myResistances;

    readonly string damageText = "Damage Text";
    public List<string> lootOrbs = new List<string>();

    ObjectPooler objectPooler;

    public LayerMask whatIsEnemy;

    [Header("Components")]
    [HideInInspector] public NavMeshAgent agent;

    Animator animator;
    AudioSource source;

    public AudioClip death;

    public GameObject fireSlash;

    public GameObject waterSlash;

    public GameObject airSlash;

    public GameObject normalSlash;

    public GameObject slamVFX;
    public GameObject ashes;

    public SkinnedMeshRenderer body;
    public MeshRenderer hair1;
    public MeshRenderer hair2;
    public MeshRenderer hair3;
    public MeshRenderer hair4;
    public MeshRenderer hair5;
    public MeshRenderer mask;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        objectPooler = ObjectPooler.Instance;
        source = GetComponent<AudioSource>();
        ashes.SetActive(false);

        foreach (int i in lootTable)
        {
            lootTotalTally += i;
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

    public void Attack()
    {
        Collider[] enemiesHit = Physics.OverlapSphere(attackOrigin.position, attackRadius, whatIsEnemy);

        Vector3 dir = transform.forward;

        foreach (Collider enemy in enemiesHit)
        {
            enemy.GetComponent<IDamageable<int, DamageTypes>>().TakeDamage(lightAttackDamage, myDamageType);
            StartCoroutine(enemy.GetComponent<PlayerMovement>().PlayerKnockback(dir, knockbackAmount));
        }
    }

    public void Slam()
    {
        Collider[] enemiesHit = Physics.OverlapSphere(transform.position, slamRadius, whatIsEnemy);

        Vector3 dir = transform.forward;

        foreach (Collider enemy in enemiesHit)
        {
            enemy.GetComponent<IDamageable<int, DamageTypes>>().TakeDamage(heavyAttackDamage, myDamageType);
            StartCoroutine(enemy.GetComponent<PlayerMovement>().PlayerKnockback(dir, knockbackAmount));
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

    public void PlaySlashTop()
    {
        switch (myDamageType)
        {
            case DamageTypes.fire:
                var obj = Instantiate(fireSlash, slashSpawn.position, Quaternion.identity, transform);
                obj.transform.localScale = new Vector3(4, 4, 4);
                break;
            case DamageTypes.water:
                var obj1 = Instantiate(waterSlash, slashSpawn.position, Quaternion.identity, transform);
                obj1.transform.localScale = new Vector3(4, 4, 4);
                break;
            case DamageTypes.air:
                var obj2 = Instantiate(airSlash, slashSpawn.position, Quaternion.identity, transform);
                obj2.transform.localScale = new Vector3(4, 4, 4);
                break;
            case DamageTypes.normal:
                var obj3 = Instantiate(normalSlash, slashSpawn.position, Quaternion.identity, transform);
                obj3.transform.localScale = new Vector3(4, 4, 4);
                break;
            default:
                break;
        }
    }

    public void PlaySlamVFX()
    {
        var obj = Instantiate(slamVFX, attackSlamPos.position, Quaternion.identity);
        obj.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    void AwardElement()
    {
        var playerElementscript = target.GetComponent<PlayerElementalForms>();

        switch (myDamageType)
        {
            case DamageTypes.fire:
                playerElementscript.hasFire = true;
                break;
            case DamageTypes.water:
                playerElementscript.hasWater = true;
                break;
            case DamageTypes.air:
                playerElementscript.hasAir = true;
                break;
            default:
                break;
        }
    }

    public IEnumerator Die()
    {
        //play dissolve shader effect
        StartCoroutine(LerpDeathShader(1.5f));
        //instantiate particle effect
        ashes.SetActive(true);
        source.PlayOneShot(death);
        GetComponent<Collider>().enabled = false;
        agent.enabled = false;
        PlayerManager.instance.lockOnSystem.KilledOpponent(gameObject);
        yield return new WaitForSeconds(2);
        DropLoot();
        AwardElement();
        Destroy(gameObject);
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
            mask.material.SetFloat("_DissolveCutoff", Mathf.Lerp(currentValueAtStart, -10, timePassed));



            yield return null;
        }

        body.material.SetFloat("_DissolveCutoff", -10);
        hair1.material.SetFloat("_DissolveCutoff", -10);
        hair2.material.SetFloat("_DissolveCutoff", -10);
        hair3.material.SetFloat("_DissolveCutoff", -10);
        hair4.material.SetFloat("_DissolveCutoff", -10);
        hair5.material.SetFloat("_DissolveCutoff", -10);
        mask.material.SetFloat("_DissolveCutoff", -10);

    }
}
