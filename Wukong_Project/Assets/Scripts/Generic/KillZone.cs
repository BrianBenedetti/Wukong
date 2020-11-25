using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var script = other.gameObject.GetComponent<PlayerCombat>();

        if (script != null)
        {
            //script.Respawn();
            script.TakeDamage(100, DamageTypes.normal);
        }
    }
}
