using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour, IPooledObject
{
    readonly float destroyTime = 0.5f;

    Vector3 RandomisePosition = new Vector3(0.5f, 0.5f, 0.5f);

    // Start is called before the first frame update
    public void OnObjectSpawn()
    {
        StartCoroutine(Disable());

        transform.localPosition += new Vector3(Random.Range(-RandomisePosition.x, RandomisePosition.x),
            Random.Range(-RandomisePosition.y, RandomisePosition.y),
            Random.Range(-RandomisePosition.z, RandomisePosition.z));
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(destroyTime);

        gameObject.SetActive(false);
    }
}
