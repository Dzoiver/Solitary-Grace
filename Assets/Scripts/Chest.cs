using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] GameObject chestPanel;
    public List<InventoryItem> ChestItems = new List<InventoryItem>();
    Menu menu;
    // Start is called before the first frame update
    private void Awake()
    {
        menu = FindObjectOfType<Menu>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(ScriptableItem scriptableItem)
    {
        InventoryItem item;
        if (scriptableItem == null)
        {
            item = new InventoryItem(999, 1, "unknown", 1, null, false);
        }
        else
            item = new InventoryItem(scriptableItem.id, scriptableItem.maxQuantity, scriptableItem.name, scriptableItem.quantity, scriptableItem.sprite, scriptableItem.keyitem);
        ChestItems.Add(item);
    }

    public void TakeItem(int itemSlot, int deleteQuantity)
    {
        if (ChestItems[itemSlot].Quantity - deleteQuantity <= 0)
        {
            ChestItems[itemSlot].Quantity = 0;
            ChestItems.RemoveAt(itemSlot);
            return;
        }

        ChestItems[itemSlot].Quantity -= deleteQuantity;
    }

    public void OpenChest()
    {
        menu.OpenChest();
    }

    public void CloseChest()
    {
        menu.OpenMenu();
    }
}
