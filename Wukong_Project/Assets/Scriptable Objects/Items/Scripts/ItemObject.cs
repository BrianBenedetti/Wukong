using UnityEngine;


public enum ItemType
{
    Health,
    Rage,
    Special
}

[CreateAssetMenu(fileName = "New Item Object", menuName = "Inventory System/Items/Item")]
public class ItemObject : ScriptableObject
{
    public int Id;

    public Sprite uiDisplay;

    public ItemType type;

    [TextArea(15, 20)]
    public string description;

    public int healthRestoreValue;
    public int rageRestoreValue;
    public int specialRestoreValue;
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public int healthValue;
    public int rageValue;
    public int specialValue;

    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.Id;
        healthValue = item.healthRestoreValue;
        rageValue = item.rageRestoreValue;
        specialValue = item.specialRestoreValue;
    }
}
