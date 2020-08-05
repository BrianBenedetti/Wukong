using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootOrbFollow : MonoBehaviour
{
    float minSpeed = 20;
    float maxSpeed = 35;

    public int healthValue;
    public int rageValue;
    public int specialValue;

    Vector3 velocity = Vector3.zero;

    private void OnTriggerEnter(Collider other)
    {
        PlayerManager.instance.player.GetComponent<PlayerController>().RestoreValues(healthValue, rageValue, specialValue);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            PlayerManager.instance.lootReceiver.transform.position,
            Time.deltaTime * Random.Range(minSpeed, maxSpeed));
    }
}
