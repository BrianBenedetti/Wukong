using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Special Object", menuName = "Inventory System/Items/Special")]
public class SpecialObject : ItemObject
{
    public int valueToRestore;

    public void Awake()
    {
        type = ItemType.Health;
    }
}
