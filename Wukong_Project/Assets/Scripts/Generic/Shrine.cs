using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        PlayerManager.instance.lastCheckpointPlayerPosition = PlayerManager.instance.player.transform.position;
    }
}
