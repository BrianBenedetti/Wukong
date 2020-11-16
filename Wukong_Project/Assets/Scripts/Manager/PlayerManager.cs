using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject player;

    public Transform lootReceiver;

    public CinemachineShake mainCamShake;
    public CinemachineShake lockOnShake;

    public Camera cam;

    public Vector3 lastCheckpointPlayerPosition;

    public AudioManager audioManager;

    public HitStop hitStop;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

#if UNITY_EDITOR
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
#endif
    }
}
