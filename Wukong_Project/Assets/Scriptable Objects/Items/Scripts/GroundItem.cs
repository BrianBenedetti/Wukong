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
        var playerItemsScript = player.GetComponent<PlayerItems>();
        playerItemsScript.inventory.AddItem(new Item(item), 1);
        var playerInteractionsScript = player.GetComponent<PlayerInteractions>();
        playerInteractionsScript.interactable = null;
        Destroy(gameObject);
    }
}
