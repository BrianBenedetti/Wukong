using UnityEngine;
//using UnityEngine.UI;

public class GroundItem : MonoBehaviour, IInteractable
{
    public ItemObject item;
    public GameObject InteractPanel;
    GameObject player;
    public GameObject collectEffect;
    public GameObject effectpos;

    AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        player = PlayerManager.instance.player;
        InteractPanel.SetActive(false);
    }

    public void Interact()
    {
        audioManager.Play("pick");
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
            audioManager.Play("close");
            InteractPanel.SetActive(true);
        }
   
    }
    private void OnTriggerExit(Collider other)
    {
        InteractPanel.SetActive(false);
    }
}
