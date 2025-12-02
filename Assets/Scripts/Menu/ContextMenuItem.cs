using GM;
using UnityEngine;

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
            default:
                break;
        }
    }

    public void DropItem()
    {
        inventory.DeleteItem(currentItem.inventorySlotID, 99);
        inventory.DisplayItems();
        gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
