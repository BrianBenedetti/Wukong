using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    [HideInInspector] public PlayerInputActions inputActions;

    float itemsScroll;

    public InventoryObject inventory;

    //collection effect (Lee)
    public GameObject scrollEffect;
    public GameObject useEffect;
    //position
    public GameObject effectpos;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.PlayerControls.MouseCycle.performed += ctx => itemsScroll = ctx.ReadValue<float>();
        inputActions.PlayerControls.MouseCycle.canceled += ctx => itemsScroll = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //cycles the items in inventory
        if (itemsScroll < -0.1f || inputActions.PlayerControls.GamepadCycleRight.triggered)
        {
            GameObject clone = Instantiate(scrollEffect, effectpos.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            Destroy(clone, 0.5f);
            FindObjectOfType<AudioManager>().Play("swap");
            inventory.MoveItem(0, inventory.Container.Items.Count - 1);
        }
        else if (itemsScroll > 0.1f || inputActions.PlayerControls.GamepadCycleLeft.triggered)
        {
            GameObject clone = Instantiate(scrollEffect, effectpos.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            Destroy(clone, 0.5f);
            FindObjectOfType<AudioManager>().Play("swap");
            inventory.MoveItem(inventory.Container.Items.Count - 1, 0);
        }

        //checks to use item
        if (inputActions.PlayerControls.UseItem.triggered)
        {
            GameObject clone = Instantiate(useEffect, effectpos.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            Destroy(clone, 3f);
            FindObjectOfType<AudioManager>().Play("use");
            inventory.UseItem();
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

    private void OnApplicationQuit()
    {
        inventory.Container.Items.Clear(); //clears inventory when leaving play mode
    }
}