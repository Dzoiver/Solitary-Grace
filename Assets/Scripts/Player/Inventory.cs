using GM;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

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
    bool keyItem = false;
    string description = "No description for this item.";
    bool usable = true;

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

    public bool KeyItem { get => keyItem; set => keyItem = value; }
    public string Description { get => description; set => description = value; }
    public bool Usable { get => usable; set => usable = value; }

    public InventoryItem(int _id, int _maxQuantity = 1, string _name = "", int _quantity = 1, Sprite _itemSprite = null, bool _keyItem = false, string _description = "No description for this item", bool _usable = true)
    {
        id = _id;
        maxQuantity = _maxQuantity;
        name = _name;
        quantity = _quantity;
        itemSprite = _itemSprite;
        keyItem = _keyItem;
        description = _description;
        usable = _usable;
    }

    public InventoryItem(InventoryItem other)
    {
        this.id = other.id;
        this.maxQuantity = other.maxQuantity;
        this.name = other.name;
        this.quantity = other.quantity;
        this.itemSprite = other.itemSprite;
        this.keyItem = other.keyItem;
        this.inventorySlotID = other.inventorySlotID;
        description = other.description;
        usable = other.usable;
    }
}

public class Inventory : MonoBehaviour
{
    private int capacity = 12;
    public List<InventoryItem> ItemsList = new List<InventoryItem>();
    [SerializeField] InventorySlotUI[] uiSlots;
    MessagesUI mesUI;
    WeaponManager weaponmanager;

    private void Start()
    {
        mesUI = FindObjectOfType<MessagesUI>();
    }

    private void Awake()
    {
        weaponmanager = FindObjectOfType<WeaponManager>();
        GameFuncs.inventory = this;
    }

    private void AddItem(ScriptableItem scriptableItem)
    {
        InventoryItem item;
        if (scriptableItem == null)
        {
            item = new InventoryItem(999, 1, "unknown", 1, null, false, "No description for this item", true);
        }
        else
            item = new InventoryItem(scriptableItem.id, scriptableItem.maxQuantity, scriptableItem.name, scriptableItem.quantity, scriptableItem.sprite, scriptableItem.keyitem, scriptableItem.description, scriptableItem.usable);

        ItemsList.Add(item);
        DisplayItems();
        weaponmanager.pistolScript.UpdateAmmoFromInventory();
        weaponmanager.shotgunScript.UpdateAmmoFromInventory();
    }

    private void AddItem(InventoryItem item)
    {
        if (true)
        {
            ItemsList.Add(item);
        }
        DisplayItems();
        weaponmanager.pistolScript.UpdateAmmoFromInventory();
        weaponmanager.shotgunScript.UpdateAmmoFromInventory();
    }

    public bool HasReadyToBeFilledItem(ScriptableItem item)
    {
        if (Has(item.id, out var slot, out var i))
        {
            if (i.Quantity + item.quantity <= item.maxQuantity)
            {
                return true;
            }
        }
        return false;
    }

    public bool HasReadyToBeFilledItem(InventoryItem item)
    {
        if (Has(item.Id, out var slot, out var i))
        {
            if (i.Quantity + item.Quantity <= item.MaxQuantity)
            {
                return true;
            }
        }
        return false;
    }

    public bool TryPickup(ScriptableItem itemInfo)
    {
        //InventoryItem inInventoryUnfilled = HasReadyToBeFilledItem(itemInfo);
        if (ItemsList.Count < capacity)
        {
            AddItem(itemInfo);
            if (itemInfo == null)
            {
                mesUI.ShowPickup("Unknown");
            }
            else
                mesUI.ShowPickup(itemInfo.name);
            return true;
        }
        else
        {
            mesUI.FullInventory();
            return false;
        }
    }

    public bool TryPickup(InventoryItem item)
    {
        if (ItemsList.Count < capacity)
        {
            AddItem(item);
            return true;
        }
        else
        {
            mesUI.FullInventory();
            return false;
        }
    }

    public void DeleteItem(int itemSlot, int deleteQuantity)
    {
        if (ItemsList[itemSlot].Quantity - deleteQuantity <= 0)
        {
            ItemsList[itemSlot].Quantity = 0;
            if (ItemsList[itemSlot].Id == 6 || ItemsList[itemSlot].Id == 7)
                weaponmanager.pistolScript.UpdateAmmoFromInventory();
            else
                mesUI.ShowUsage(ItemsList[itemSlot].Name);


            ItemsList.RemoveAt(itemSlot);
            DisplayItems();
            return;
        }

        ItemsList[itemSlot].Quantity -= deleteQuantity;

        if (ItemsList[itemSlot].Id == 6)
            weaponmanager.pistolScript.UpdateAmmoFromInventory();
        if (ItemsList[itemSlot].Id == 7)
            weaponmanager.shotgunScript.UpdateAmmoFromInventory();
    }

    public void DecreaseCount(int demandCount, int itemID)
    {
        InventoryItem leastQuantity;
        switch (itemID)
        {
            case 6: // If Pistol Ammo
                
                leastQuantity = LeastQuantityItem(6);
                if (demandCount >= leastQuantity.Quantity) // If ammo needed is greater than 1 slot
                {
                    int reminder = demandCount - leastQuantity.Quantity;
                    DeleteItem(leastQuantity.inventorySlotID, 999);
                    InventoryItem leastQuantity2 = LeastQuantityItem(6);
                    if (leastQuantity2 != null)
                        DeleteItem(LeastQuantityItem(6).inventorySlotID, reminder);
                }
                else
                {
                    DeleteItem(leastQuantity.inventorySlotID, demandCount);
                }
                break;

            case 7: // If Shotgun Ammo
                leastQuantity = LeastQuantityItem(7);
                if (demandCount >= leastQuantity.Quantity) // If ammo needed is greater than 1 slot
                {
                    int reminder = demandCount - leastQuantity.Quantity;
                    DeleteItem(leastQuantity.inventorySlotID, 999);
                    InventoryItem leastQuantity2 = LeastQuantityItem(7);
                    if (leastQuantity2 != null)
                    {
                        DeleteItem(LeastQuantityItem(7).inventorySlotID, reminder);
                    }

                }
                else
                {
                    DeleteItem(leastQuantity.inventorySlotID, demandCount);
                }
                break;
        }
    }

    public InventoryItem LeastQuantityItem(int givenID)
    {
        InventoryItem itemToReturn = null;
        int leastQuantity = 999;
        foreach (InventoryItem it in ItemsList)
        {
            if (it.Id == givenID)
            {
                if (it.Quantity < leastQuantity)
                {
                    leastQuantity = it.Quantity;
                    itemToReturn = it;
                }
            }
        }
        return itemToReturn;
    }

    public void CombineItems(InventoryItem newItem)
    {
        foreach (InventoryItem it in ItemsList)
        {
            if (it.Id == newItem.Id)
            {
                if (it.Quantity + newItem.Quantity <= newItem.MaxQuantity)
                {
                    it.Quantity += newItem.Quantity;
                }
            }
        }
    }

    public bool Has(int givenID, out int slot, out InventoryItem item)
    {
        foreach (InventoryItem it in ItemsList)
        {
            if (it.Id == givenID)
            {
                slot = it.inventorySlotID;
                item = it;
                return true;
            }
        }

        slot = 0;
        item = null;
        return false;
    }

    public bool Has(int givenID, out int slot)
    {
        foreach (InventoryItem it in ItemsList)
        {
            if (it.Id == givenID)
            {
                slot = it.inventorySlotID;
                return true;
            }
        }

        slot = 0;
        return false;
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

    public int ItemAmount(int givenID)
    {
        int sum = 0;
        foreach (InventoryItem it in ItemsList)
        {
            if (it.Id == givenID)
            {
                sum += it.Quantity;
            }
        }

        return sum;
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
