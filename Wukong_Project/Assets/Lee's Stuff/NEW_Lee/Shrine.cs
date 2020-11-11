using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : MonoBehaviour, IInteractable
{
    public GameObject SavePanel;
    GameObject player;
    bool isSaved;

    void Start()
    {
        player = PlayerManager.instance.player;
        SavePanel.SetActive(false);
        isSaved = false;

    }
    public void Interact()
    {
        PlayerManager.instance.lastCheckpointPlayerPosition = PlayerManager.instance.player.transform.position;
        isSaved = true;
        SavePanel.SetActive(false);
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && isSaved == false)
        {
            FindObjectOfType<AudioManager>().Play("close");
            SavePanel.SetActive(true);
        }
        else if (isSaved == true)
        {
            SavePanel.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        SavePanel.SetActive(false);
    }
}


