using UnityEngine;

public class Shrine : MonoBehaviour, IInteractable
{
    public GameObject SavePanel;
    GameObject player;

    AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        player = PlayerManager.instance.player;
        SavePanel.SetActive(false);

    }
    public void Interact()
    {
        PlayerManager.instance.lastCheckpointPlayerPosition = PlayerManager.instance.player.transform.position;
        SavePanel.SetActive(false);
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioManager.Play("close");
            SavePanel.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        SavePanel.SetActive(false);
    }
}


