using UnityEngine;

public class CheckPoint : MonoBehaviour, IInteractable
{
    //make audio file here and play thhat instead of looking it up

    public void Interact()
    {
        PlayerManager.instance.lastCheckpointPlayerPosition = transform.position;
        //PlayerManager.instance.audioManager.Play("check");
    }
}