using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    float destroyTime = 0.5f;

    Vector3 RandomisePosition = new Vector3(0.5f, 0.5f, 0.5f);

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);

        transform.localPosition += new Vector3(Random.Range(-RandomisePosition.x, RandomisePosition.x),
            Random.Range(-RandomisePosition.y, RandomisePosition.y),
            Random.Range(-RandomisePosition.z, RandomisePosition.z));
    }
}
