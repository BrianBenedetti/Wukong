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

    [SerializeField] bool isLockedOn;

    int index;

    public float lockOnRange;

<<<<<<< HEAD
    readonly string enemyTag = "Enemy";
=======
    const string enemyTag = "Enemy";
>>>>>>> 64429ae0c6f70c4ba99a9f0d1069938577b46d9a

    public Transform emptyTarget;
    Transform target;

    [SerializeField] protected List<Transform> allEnemies = new List<Transform>();
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
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        //constantly removes null transforms after they die
        allEnemies.RemoveAll(Transform => Transform == null);
        enemiesInRange.RemoveAll(Transform => Transform == null);
=======
        ////constantly removes null transforms after they die
        //allEnemies.RemoveAll(Transform => Transform == null);
        //enemiesInRange.RemoveAll(Transform => Transform == null);
>>>>>>> 64429ae0c6f70c4ba99a9f0d1069938577b46d9a

        if (index > enemiesInRange.Count - 1)
        {
            index = 0;
        }

        if(enemiesInRange.Count > 0)
        {
            target = enemiesInRange[index];
        }

        //locks on
        if (inputActions.PlayerControls.LockOn.triggered && !isLockedOn)
        {
            FindAllEnemies();
            CheckForEnemiesInRange();
            
            if (enemiesInRange.Count > 0)
            {
                LockOn();
            }
        }
        //locks off
        else if(inputActions.PlayerControls.LockOn.triggered && isLockedOn || enemiesInRange.Count <= 0 || Vector3.Distance(this.gameObject.transform.position, target.position) > lockOnRange)
        {
            targetGroup.m_Targets[1].target = emptyTarget;
            enemiesInRange.Clear();
            mainVirtualCamera.enabled = true;
            lockOnCamera.enabled = false;
            lockOnCursor.SetActive(false);
            isLockedOn = false;
        }

        //checks to cycle
        if (inputActions.PlayerControls.SwitchTarget.triggered && isLockedOn)
        {
            CycleThroughEnemies();
        }

        //checks if is locked on
        if (isLockedOn)
        {
            targetGroup.m_Targets[1].target = target;
            lockOnCursor.transform.position = mainCamera.WorldToScreenPoint(target.position);
        }
    }

<<<<<<< HEAD
=======
    public void KilledOpponent(GameObject opponent)
    {
        if (allEnemies.Contains(opponent.transform))
        {
            allEnemies.Remove(opponent.transform);
        }
        if (enemiesInRange.Contains(opponent.transform))
        {
            enemiesInRange.Remove(opponent.transform);
        }
    }

>>>>>>> 64429ae0c6f70c4ba99a9f0d1069938577b46d9a
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
        for (int i = enemiesFound.Length - 1; i > -1; i--)
        {
            if (!allEnemies.Contains(enemiesFound[i].transform))
            {
                allEnemies.Add(enemiesFound[i].transform);
            }
        }
    }

    void CheckForEnemiesInRange()
    {
        if(allEnemies.Count > 0)
        {
            for (int i = allEnemies.Count - 1; i > -1; i--)
            {
                Vector3 ePos = mainCamera.WorldToViewportPoint(allEnemies[i].position);

                if ((Vector3.Distance(this.gameObject.transform.position, allEnemies[i].position) <= lockOnRange) && !enemiesInRange.Contains(allEnemies[i]))
                {
                    enemiesInRange.Add(allEnemies[i]);
                }
                else
                {
                    enemiesInRange.Remove(allEnemies[i]);
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
