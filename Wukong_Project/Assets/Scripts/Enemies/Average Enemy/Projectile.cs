using UnityEngine;

public class Projectile : MonoBehaviour, IPooledObject
{
    public float speed;
    public float knockbackAmount;

    public AverageEnemy parent;

    public DamageTypes myDamageType;

    Transform player;

    readonly string playerTag = "Player";

    Vector3 target;

    public void OnObjectSpawn()
    {
        player = PlayerManager.instance.player.transform;
        target = new Vector3(player.position.x, transform.position.y, player.position.z);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(transform.position.x == target.x && transform.position.z == target.z)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var damageScript = other.gameObject.GetComponent<IDamageable<int, DamageTypes>>();
        if (damageScript != null)
        {
            damageScript.TakeDamage(parent.projectileDamage, myDamageType);

            Vector3 dir = transform.forward;
            if (other.CompareTag(playerTag))
            {
                StartCoroutine(other.GetComponent<PlayerMovement>().PlayerKnockback(dir, knockbackAmount));
            }
        }
        gameObject.SetActive(false);
    }
}
