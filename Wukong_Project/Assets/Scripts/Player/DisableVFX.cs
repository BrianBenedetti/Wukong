using System.Collections;
using UnityEngine;

public class DisableVFX : MonoBehaviour
{
    public float timeToDisable;

    public bool removeParent;

    void OnEnable()
    {
        if (removeParent)
            transform.SetParent(null);

        StartCoroutine(DisableAfterTime(timeToDisable));
    }

    IEnumerator DisableAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
    }
}
