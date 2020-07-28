using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItem : MonoBehaviour, IInteractable
{
    public ItemObject item;

    GameObject player;

    private void Start()
    {
        player = PlayerManager.instance.player;
    }

    public void Interact()
    {
        var playerController = player.GetComponent<PlayerController>();
        playerController.inventory.AddItem(new Item(item), 1);
        playerController.interactable = null;
        Destroy(gameObject);
    }
}
