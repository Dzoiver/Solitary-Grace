using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    private int quantity = 0;
    private int maxQuantity = 0;
    private int id = 0;
    private string name = "";

    public int Quantity
    {
        get { return quantity; }
        set { quantity = value; }
    }

    public int MaxQuantity
    {
        get { return maxQuantity; }
    }

    public int Id
    {
        get { return id; }
    }

    public string Name
    {
        get { return name; }
    }

    public InventoryItem(int _id, int _maxQuantity = 1, string _name = "", int _quantity = 1)
    {
        id = _id;
        maxQuantity = _maxQuantity;
        name = _name;
        quantity = _quantity;
    }
}

public class Inventory : MonoBehaviour
{
    private int capacity = 6;
    public List<InventoryItem> ItemsList = new List<InventoryItem>();

    public void AddItem(InventoryItem item)
    {
        if (ItemsList.Count < capacity)
        {
            ItemsList.Add(item);
            Debug.Log("item added: " + item.Name);
        }
    }

    public void DeleteItem(int itemSlot, int quantity)
    {

        if (ItemsList[itemSlot].Quantity < quantity)
        {
            ItemsList.RemoveAt(itemSlot);
            return;
        }

        ItemsList[itemSlot].Quantity -= quantity;
    }
}
