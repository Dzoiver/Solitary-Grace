using GM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ContextMenuItem : MonoBehaviour
{
    public InventoryItem currentItem;
    Inventory inventory;
    // Start is called before the first frame update

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
    }
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void CallContextMenu(InventoryItem item)
    {
        currentItem = item;
        gameObject.SetActive(true);
        gameObject.transform.position = Input.mousePosition;
    }

    public void UseItem()
    {
        switch (currentItem.Name)
        {
            case "Health Drink":
                GameFuncs.PlayerScript.GiveHP(30f);
                inventory.DeleteItem(currentItem.inventorySlotID, 1);
                inventory.DisplayItems();
                gameObject.SetActive(false);
                break;
            case "Tom":
                Console.WriteLine("Ваше имя - Tom");
                break;
            case "Sam":
                Console.WriteLine("Ваше имя - Sam");
                break;
            default:
                break;
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
