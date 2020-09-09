using UnityEngine;

public class Billboard : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.forward = PlayerManager.instance.cam.transform.forward;
    }
}
