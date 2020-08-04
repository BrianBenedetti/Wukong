using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LockOnSystem : MonoBehaviour
{
    PlayerInputActions inputActions;

    public Camera mainCamera;

    public CinemachineFreeLook mainVirtualCamera;
    public CinemachineVirtualCamera lockOnCamera;
    public CinemachineTargetGroup targetGroup;

    bool isLockedOn;

    int index;

    public float lockOnRange;

    string enemyTag = "Enemy";

    Transform target;

    [SerializeField] List<Transform> allEnemies = new List<Transform>();
    [SerializeField] List<Transform> enemiesInRange = new List<Transform>();

    GameObject[] enemiesFound;

    public GameObject lockOnCursor;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    // Start is called before the first frame update
    void Start()
    {
        target = null;
        lockOnCursor.SetActive(false);
        isLockedOn = false;
        index = 0;

        FindAllEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        if(inputActions.PlayerControls.LockOn.triggered && !isLockedOn)
        {
            FindAllEnemies();
            CheckForEnemiesInRange();
            
            if (enemiesInRange.Count > 0)
            {
                LockOn();
            }
        }
        else if(inputActions.PlayerControls.LockOn.triggered && isLockedOn || enemiesInRange.Count < 1 || Vector3.Distance(this.gameObject.transform.position, target.position) > lockOnRange)
        {
            enemiesInRange.Clear();
            mainVirtualCamera.enabled = true;
            lockOnCamera.enabled = false;
            lockOnCursor.SetActive(false);
            isLockedOn = false;
        }

        if(inputActions.PlayerControls.SwitchTarget.triggered && isLockedOn)
        {
            CycleThroughEnemies();
        }

        if (isLockedOn)
        {
            target = enemiesInRange[index];
            targetGroup.m_Targets[1].target = target;
            lockOnCursor.transform.position = mainCamera.WorldToScreenPoint(target.position);
        }
    }

    void LockOn()
    {
        isLockedOn = true;
        lockOnCursor.SetActive(true);
        lockOnCamera.enabled = true;
        mainVirtualCamera.enabled = false;
    }

    void FindAllEnemies()
    {
        enemiesFound = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject e in enemiesFound)
        {
            if (!allEnemies.Contains(e.transform))
            {
                allEnemies.Add(e.transform);
            }
        }
    }

    void CheckForEnemiesInRange()
    {
        if(allEnemies.Count > 0)
        {
            foreach (Transform e in allEnemies)
            {
                Vector3 ePos = mainCamera.WorldToViewportPoint(e.position);

                if ((Vector3.Distance(this.gameObject.transform.position, e.position) <= lockOnRange) && !enemiesInRange.Contains(e))
                {
                    enemiesInRange.Add(e);
                }
                else
                {
                    enemiesInRange.Remove(e);
                }
            }
        }        
    }

    void CycleThroughEnemies()
    {
        if (isLockedOn)
        {
            enemiesInRange.Clear();
            CheckForEnemiesInRange();

            if(enemiesInRange.Count > 0)
            {
                index++;
                if (index > enemiesInRange.Count - 1)
                {
                    index = 0;
                }
            }            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, lockOnRange);
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
