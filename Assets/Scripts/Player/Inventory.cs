using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public int inventorySlotID;

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
    private int capacity = 12;
    public List<InventoryItem> ItemsList = new List<InventoryItem>();
    [SerializeField] InventorySlotUI[] uiSlots;
    MessagesUI mesUI;

    private void Start()
    {
        mesUI = FindObjectOfType<MessagesUI>();
    }

    private void AddItem(ScriptableItem scriptableItem)
    {
        if (scriptableItem == null)
        {
            scriptableItem.id = 999;
            scriptableItem.name = "unknown";
            scriptableItem.quantity = 1;
            scriptableItem.maxQuantity = 1;
        }
        InventoryItem item = new InventoryItem(scriptableItem.id, scriptableItem.maxQuantity, scriptableItem.name, scriptableItem.quantity, scriptableItem.sprite);

        ItemsList.Add(item);
        // Debug.Log("item added: " + item.Name);
    }

    public void TryPickup(GameObject objectItem, ScriptableItem itemInfo, bool destroyOnPickup)
    {
        if (ItemsList.Count < capacity)
        {
            objectItem.SetActive(!destroyOnPickup);
            AddItem(itemInfo);
            mesUI.ShowPickup(itemInfo.name);
        }
        else
        {
            mesUI.FullInventory();
        }
    }

    public void DeleteItem(int itemSlot, int deleteQuantity)
    {
        if (ItemsList[itemSlot].Quantity - deleteQuantity <= 0)
        {
            ItemsList.RemoveAt(itemSlot);
            return;
        }

        ItemsList[itemSlot].Quantity -= deleteQuantity;
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
        foreach (InventorySlotUI slot in uiSlots)
        {
            if (index < ItemsList.Count)
            {
                uiSlots[index].UpdateSlot(ItemsList[index], index);
            }
            else
            {
                uiSlots[index].UpdateSlot(null, index);
            }
            index++;
        }
        return;
        foreach (InventoryItem it in ItemsList)
        {
            uiSlots[index].UpdateSlot(it, index);
            index++;
        }
    }
}
