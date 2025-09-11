using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ItemNames
{
    Nothing,
    CameraKey,
    HealthDrink,
    Knife,
    Pistol,
    Shotgun
}

public class InventoryItem
{
    private int quantity = 0;
    private int maxQuantity = 0;
    private int id = 0;
    private string name = "";
    private Sprite itemSprite;

    public Sprite ItemSprite
    {
        get { return itemSprite; }
        set { itemSprite = value; }
    }
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

    public InventoryItem(int _id, int _maxQuantity = 1, string _name = "", int _quantity = 1, Sprite _itemSprite = null)
    {
        id = _id;
        maxQuantity = _maxQuantity;
        name = _name;
        quantity = _quantity;
        itemSprite = _itemSprite;
    }
}

public class Inventory : MonoBehaviour
{
    private int capacity = 6;
    public List<InventoryItem> ItemsList = new List<InventoryItem>();
    [SerializeField] InventorySlotUI[] uiSlots;

    public void AddItem(ScriptableItem scriptableItem)
    {
        InventoryItem item = new InventoryItem(scriptableItem.id, scriptableItem.maxQuantity, scriptableItem.name, scriptableItem.quantity, scriptableItem.sprite);

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

    public bool Has(int givenID)
    {
        foreach (InventoryItem it in ItemsList)
        {
            if (it.Id == givenID)
            {
                return true;
            }
        }
        return false;
    }

    public void DisplayItems()
    {
        int index = 0;
        foreach (InventoryItem it in ItemsList)
        {
            uiSlots[index].UpdateSlot(it);
            index++;
        }
    }
}
