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
<<<<<<< HEAD
        var playerController = player.GetComponent<PlayerController>();
        playerController.inventory.AddItem(new Item(item), 1);
        playerController.interactable = null;
=======
        var playerItemsScript = player.GetComponent<PlayerItems>();
        playerItemsScript.inventory.AddItem(new Item(item), 1);
        var playerInteractionsScript = player.GetComponent<PlayerInteractions>();
        playerInteractionsScript.interactable = null;
>>>>>>> 64429ae0c6f70c4ba99a9f0d1069938577b46d9a
        Destroy(gameObject);
    }
}
