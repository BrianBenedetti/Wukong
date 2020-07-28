using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Rage Object", menuName = "Inventory System/Items/Rage")]
public class RageObject : ItemObject
{
    public int valueToRestore;

    public void Awake()
    {
        type = ItemType.Rage;
    }
}
