using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;

    public bool isLeftIcon;
    public bool isRightIcon;

    public int indexToDisplay;

    Sprite emptySprite;

    private void Start()
    {
        emptySprite = transform.GetChild(0).GetComponentInChildren<Image>().sprite;
    }

    void Update()
    {
        if (isLeftIcon)
        {
            if(inventory.Container.Items.Count >= 3)
            {
                transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[inventory.Container.Items[inventory.Container.Items.Count - 1].item.Id].uiDisplay;
            }
            else
            {
                transform.GetChild(0).GetComponentInChildren<Image>().sprite = emptySprite;
            }
        }
        else if (isRightIcon)
        {
            if (inventory.Container.Items.Count > indexToDisplay)
            {
                transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[inventory.Container.Items[indexToDisplay].item.Id].uiDisplay;
            }
            else
            {
                transform.GetChild(0).GetComponentInChildren<Image>().sprite = emptySprite;
            }
        }
        else
        {
            if (inventory.Container.Items.Count > indexToDisplay)
            {
                transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[inventory.Container.Items[indexToDisplay].item.Id].uiDisplay;
                GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container.Items[indexToDisplay].amount.ToString("n0");
            }
            else
            {
                transform.GetChild(0).GetComponentInChildren<Image>().sprite = emptySprite;
                GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }
}
