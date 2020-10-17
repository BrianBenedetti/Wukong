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
<<<<<<< HEAD
        PlayerManager.instance.player.GetComponent<PlayerController>().RestoreValues(healthValue, rageValue, specialValue);
=======
        PlayerManager.instance.player.GetComponent<PlayerCombat>().RestoreValues(healthValue, rageValue, specialValue);
>>>>>>> 64429ae0c6f70c4ba99a9f0d1069938577b46d9a
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            PlayerManager.instance.lootReceiver.transform.position,
            Time.deltaTime * Random.Range(minSpeed, maxSpeed));
    }
}
