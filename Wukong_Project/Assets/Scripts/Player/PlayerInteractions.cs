using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ShrineTag))
        {
            interactable = other.GetComponent<IInteractable>();
        }
        else if (other.CompareTag(ObjectTag))
        {
            interactable = other.GetComponent<IInteractable>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Shrine"))
        {
            if (interactable != null)
            {
                interactable = null;
            }
        }
        else if (other.CompareTag("Object"))
        {
            if (interactable != null)
            {
                interactable = null;
            }
        }
    }

    readonly string ShrineTag = "Shrine";
    readonly string ObjectTag = "Object";

    [HideInInspector] public IInteractable interactable;

    [HideInInspector] public PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    // Update is called once per frame
    void Update()
    {
        //checks to interact with objects
        if (inputActions.PlayerControls.Interact.triggered)
        {
            Interact();
        }
    }

    public void Interact()
    {
        if (interactable != null)
        {
            interactable.Interact();
        }
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
