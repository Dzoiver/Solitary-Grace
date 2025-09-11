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
    public InventoryItem item;

    public void UpdateSlot(InventoryItem it)
    {
        if (it == null)
        {
            item = null;
            itemIcon.enabled = false;
            return;
        }
        itemIcon.enabled = true;
        item = it;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
