using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemname;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Image itemIcon;
    ContextMenuItem context;
    
    public InventoryItem item = null;

    private void Awake()
    {
        context = FindObjectOfType<ContextMenuItem>();
    }

    public void UpdateSlot(InventoryItem it, int index)
    {
        if (it == null)
        {
            item = null;
            itemIcon.enabled = false;
            return;
        }
        itemIcon.enabled = true;
        item = it;
        item.inventorySlotID = index;
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
        context.CallContextMenu(item);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
