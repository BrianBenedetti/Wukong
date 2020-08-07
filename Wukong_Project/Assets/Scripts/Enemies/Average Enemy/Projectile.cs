using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IPooledObject
{
    public float speed;

    public AverageEnemy parent;

    Transform player;

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
        other.GetComponent<IDamageable<int, DamageTypes>>().TakeDamage(parent.projectileDamage, parent.myDamageType);
        Vector3 dir = transform.position - player.transform.position;
        dir.y = 0;
        Rigidbody enemyRb = player.GetComponent<Rigidbody>();
        enemyRb.velocity = Vector3.zero;
        enemyRb.velocity = -dir.normalized * 3;
        gameObject.SetActive(false);
    }
}
