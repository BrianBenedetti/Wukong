using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;

    public bool isLeftIcon;

    public int indexToDisplay;

    void Update()
    {
        if (inventory.Container.Items.Count > indexToDisplay)
        {
            if (!isLeftIcon)
            {
                transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[inventory.Container.Items[indexToDisplay].item.Id].uiDisplay;
                GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container.Items[indexToDisplay].amount.ToString("n0");
            }
            else if(isLeftIcon && inventory.Container.Items.Count >= 3)
            {
                transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[inventory.Container.Items[inventory.Container.Items.Count - 1].item.Id].uiDisplay;
                GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container.Items[inventory.Container.Items.Count - 1].amount.ToString("n0");
            }
            
        }
    }
}
