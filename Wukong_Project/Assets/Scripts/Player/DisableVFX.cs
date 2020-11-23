using System.Collections;
using UnityEngine;

public class DisableVFX : MonoBehaviour, IPooledObject
{
    public float timeToDisable;

    public bool removeParent;

    public void OnObjectSpawn()
    {
        StartCoroutine(DisableAfterTime(timeToDisable));
    }

    IEnumerator DisableAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (removeParent)
            transform.SetParent(null);

        gameObject.SetActive(false);
    }
}
