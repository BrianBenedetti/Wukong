using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    [HideInInspector] public PlayerInputActions inputActions;

    float itemsScroll;

    public InventoryObject inventory;

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
            inventory.MoveItem(0, inventory.Container.Items.Count - 1);
        }
        else if (itemsScroll > 0.1f || inputActions.PlayerControls.GamepadCycleLeft.triggered)
        {
            inventory.MoveItem(inventory.Container.Items.Count - 1, 0);
        }

        //checks to use item
        if (inputActions.PlayerControls.UseItem.triggered)
        {
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
