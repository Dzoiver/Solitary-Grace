using GM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public bool Opened;
    public List<InventoryItem> ChestItems = new List<InventoryItem>();
    [SerializeField] ChestItemSlot[] chestItemsUI;
    int maxItems = 30;
    Menu menu;
    Inventory inventory;
    // Start is called before the first frame update
    private void Awake()
    {
        menu = FindObjectOfType<Menu>();
        inventory = FindObjectOfType<Inventory>();
        gameObject.SetActive(false);
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddItem(InventoryItem item)
    {
        if (ChestItems.Count >= maxItems)
            return;
        item.inventorySlotID = ChestItems.Count;
        ChestItems.Add(item);
        chestItemsUI[item.inventorySlotID].ImageSprite = item.ItemSprite;
        chestItemsUI[item.inventorySlotID].ItemName.text = item.Name;
        if (item.Quantity == 1)
            chestItemsUI[item.inventorySlotID].ItemQuantity.text = "";
        else
            chestItemsUI[item.inventorySlotID].ItemQuantity.text = item.Quantity.ToString();
        chestItemsUI[item.inventorySlotID].item = item;
    }

    public void TakeItem(int itemSlot)
    {
        inventory.TryPickup(ChestItems[itemSlot]);
        ChestItems.RemoveAt(itemSlot);

        for (int i = 0; i < chestItemsUI.Length; i++)
        {
            if (chestItemsUI[i].item != null &&
                chestItemsUI[i].item.inventorySlotID == itemSlot)
            {
                chestItemsUI[i].Clear();
                break;
            }
        }

        UpdateChestIndices();
    }

    private void UpdateChestIndices()
    {
        // Обновляем inventorySlotID у оставшихся предметов
        for (int i = 0; i < ChestItems.Count; i++)
        {
            ChestItems[i].inventorySlotID = i;
        }

        // Обновляем UI
        BuildChestList();
    }

    public void BuildChestList()
    {
        foreach (var slot in chestItemsUI)
        {
            slot.Clear();
        }

        foreach (InventoryItem it in ChestItems)
        {
            if (it.inventorySlotID < chestItemsUI.Length)
            {
                chestItemsUI[it.inventorySlotID].ImageSprite = it.ItemSprite;
                chestItemsUI[it.inventorySlotID].ItemName.text = it.Name;
                if (it.Quantity == 1)
                    chestItemsUI[it.inventorySlotID].ItemQuantity.text = "";
                else
                    chestItemsUI[it.inventorySlotID].ItemQuantity.text = it.Quantity.ToString();
                chestItemsUI[it.inventorySlotID].item = it;
            }
        }
    }

    public void OpenChest()
    {
        gameObject.SetActive(true);
        menu.OpenMenu();
        //BuildChestList();
    }

    
}
