﻿using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;

    public ItemDatabaseObject database;

    public Inventory Container;

    public void AddItem(Item _item, int _amount)
    {
        for (int i = 0; i < Container.Items.Count; i++)
        {
            if(Container.Items[i].item.Id == _item.Id)
            {
                Container.Items[i].AddAmount(_amount);
                return;
            }
        }
        Container.Items.Add(new InventorySlot(_item.Id, _item, _amount));
    }

    public void MoveItem(int oldIndex, int newIndex)
    {
        if(Container.Items.Count >= 2)
        {
            var item = Container.Items[oldIndex];
            Container.Items.RemoveAt(oldIndex);
            Container.Items.Insert(newIndex, item);
        }
    }

    public void UseItem()
    {
        if(Container.Items.Count > 0)
        {
            var itemToUse = Container.Items[0];

            //add values to player
            PlayerManager.instance.player.GetComponent<PlayerCombat>().RestoreValues(itemToUse.item.healthValue, itemToUse.item.rageValue, itemToUse.item.specialValue);

            itemToUse.ReduceAmount(1);

            if (itemToUse.amount <= 0)
            {
                Container.Items.Remove(itemToUse);
            }
        } 
    }

    [ContextMenu("Save")]
    public void Save()
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }

    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Container = (Inventory)formatter.Deserialize(stream);
            stream.Close();
        }
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        Container = new Inventory();
    }
}

[System.Serializable]
public class Inventory
{
    public List<InventorySlot> Items = new List<InventorySlot>();
}

[System.Serializable]
public class InventorySlot
{
    public int ID = -1;

    public Item item;

    public int amount;

    public InventorySlot(int _id, Item _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value){

        amount += value;
    }

    public void ReduceAmount(int value)
    {
        amount -= value;
    }
}
