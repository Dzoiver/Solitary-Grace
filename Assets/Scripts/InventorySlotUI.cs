using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] TextMeshProUGUI itemname;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Image itemIcon;
    [SerializeField] TextMeshProUGUI itemQuantity;
    ContextMenuItem context;
    Chest chest;
    
    public InventoryItem item = null;

    private void Awake()
    {
        chest = FindAnyObjectByType<Chest>(FindObjectsInactive.Include);
        context = FindObjectOfType<ContextMenuItem>();
    }

    public void UpdateSlot(InventoryItem it, int index)
    {
        if (it == null)
        {
            item = null;
            itemIcon.enabled = false;
            itemQuantity.text = "";
            return;
        }
        itemIcon.enabled = true;
        item = it;
        item.inventorySlotID = index;
        if (it.MaxQuantity > 1)
            itemQuantity.text = it.Quantity.ToString();
        else
            itemQuantity.text = "";
        if (item.ItemSprite == null)
        {
            itemIcon.sprite = defaultSprite;
        }
        else
        {
            itemIcon.sprite = item.ItemSprite;
        }
    }

    public void UpdateItemName()
    {
        if (item != null)
            itemname.text = item.Name;
    }

    public void TryOpenContext()
    {
        if (item != null)
        {
            Debug.Log(item.inventorySlotID);
            context.CallContextMenu(item);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            TryOpenContext();
        }
        else if (eventData.button == PointerEventData.InputButton.Left && chest.gameObject.activeSelf)
        {
            InventoryItem itemCopy = new InventoryItem(item);
            context.inventory.DeleteItem(item.inventorySlotID, 99);
            chest.AddItem(itemCopy);
        }
    }
}
