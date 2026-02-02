using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChestItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private UnityEngine.UI.Image image;
    private Sprite imageSprite;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemQuantity;
    public InventoryItem item;
    Chest chest;

    public Sprite ImageSprite { get => imageSprite; set
        {
            if (value == null)
            {
                image.enabled = false;
            }
            else
            {
                image.enabled = true;
                image.sprite = value;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && item != null)
        {
            int currentIndex = chest.ChestItems.IndexOf(item);
            Debug.Log("taking" + item.Name + " from slot: " + item.inventorySlotID); // неправильный слот
            chest.TakeItem(currentIndex);
        }
    }

    private void Awake()
    {
        chest = FindObjectOfType<Chest>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clear()
    {
        ImageSprite = null;
        ItemName.text = "Empty";
        ItemQuantity.text = "";
        item = null;
    }
}
