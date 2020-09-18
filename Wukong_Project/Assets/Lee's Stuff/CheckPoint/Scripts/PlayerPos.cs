using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    private GameMaster Gm;

    private void Start()
    {
        Gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = Gm.lastCheckPointPos;
    }
}
