using GM;
using SolitaryAudio;
using UnityEngine;

public class ContextMenuItem : MonoBehaviour
{
    public InventoryItem currentItem;
    [SerializeField] GameObject dropOption;
    [SerializeField] GameObject useOption;
    public Inventory inventory;
    [SerializeField] MusicAmbientController audioController;
    [SerializeField] AudioClip healthDrinkSound;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void CallContextMenu(InventoryItem item)
    {
        if (item.KeyItem)
            dropOption.SetActive(false);
        else
            dropOption.SetActive(true);

        if (item.Usable)
        {
            useOption.SetActive(true);
        }
        else
        {
            useOption.SetActive(false);
        }

            currentItem = item;
        gameObject.SetActive(true);
        gameObject.transform.position = Input.mousePosition;
    }

    public void UseItem()
    {
        switch (currentItem.Id)
        {
            case 2:
                GameFuncs.PlayerScript.GiveHP(35f);
                inventory.DeleteItem(currentItem.inventorySlotID, 1);
                inventory.DisplayItems();
                audioController.SoundVolume = 0.2f;
                audioController.PlaySound(healthDrinkSound);
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
