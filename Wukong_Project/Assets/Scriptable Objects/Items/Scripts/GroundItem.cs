using UnityEngine;
using UnityEngine.UI;
public class GroundItem : MonoBehaviour, IInteractable
{
    public ItemObject item;
    public GameObject InteractPanel;
    GameObject player;
    public GameObject collectEffect;
    public GameObject effectpos;
    private void Start()
    {
        player = PlayerManager.instance.player;
        InteractPanel.SetActive(false);
        
    }

    public void Interact()
    {
        FindObjectOfType<AudioManager>().Play("pick");
        GameObject clone = Instantiate(collectEffect, effectpos.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
        Destroy(clone, 4f);
        var playerItemsScript = player.GetComponent<PlayerItems>();
        playerItemsScript.inventory.AddItem(new Item(item), 1);
        var playerInteractionsScript = player.GetComponent<PlayerInteractions>();
        playerInteractionsScript.interactable = null;
      
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("close");
            InteractPanel.SetActive(true);
        }
   
    }
    private void OnTriggerExit(Collider other)
    {
        InteractPanel.SetActive(false);
    }
}
