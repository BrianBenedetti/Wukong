using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    public AverageEnemy parent;

    Transform player;

    Vector3 target;

    private void Start()
    {
        player = PlayerManager.instance.player.transform;
        target = new Vector3(player.position.x, transform.position.y, player.position.z);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(transform.position.x == target.x && transform.position.z == target.z)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamageable<int, DamageTypes>>().TakeDamage(parent.projectileDamage, parent.myDamageType);

            Vector3 dir = transform.position - player.transform.position;
            dir.y = 0;

            Rigidbody enemyRb = player.GetComponent<Rigidbody>();
            enemyRb.velocity = Vector3.zero;
            enemyRb.velocity = -dir.normalized * 3;

            Destroy(gameObject);
        }
    }
}
