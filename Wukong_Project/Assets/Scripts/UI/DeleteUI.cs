using System.Collections;
using UnityEngine;

public class DeleteUI : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeleteAfterTime(time));
    }

    IEnumerator DeleteAfterTime(float time)
    {
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(time);

        Time.timeScale = 1;
        Destroy(gameObject);
    }
}
