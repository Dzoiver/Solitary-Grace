using GM;
using SolitaryAudio;
using UnityEngine;

public class ContextMenuItem : MonoBehaviour
{
    public InventoryItem currentItem;
    [SerializeField] GameObject dropOption;
    Inventory inventory;
    [SerializeField] MusicAmbientController audioController;
    [SerializeField] AudioClip healthDrinkSound;
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
        if (item.KeyItem)
            dropOption.SetActive(false);
        else
            dropOption.SetActive(true);
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
