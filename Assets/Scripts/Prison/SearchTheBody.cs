using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;
using Zenject;

public class SearchTheBody : MonoBehaviour
{
    [Inject] Menu menu;
    Inventory inventory;
    [SerializeField] ScriptableItem key;
    private InventoryItem item;
    private bool keyTaken = false;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        item = new InventoryItem(key.id, key.maxQuantity, key.name);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (inventory.Has(key.id))
            return;

        if (other.gameObject.name != "UseCube")
            return;

        if (keyTaken)
            return;

        other.GetComponent<UseCube>().pScript.SetControl(false);

        // Do you want to take the key card?
        //menu.ConfirmBox(key);

        // Call dialogue window

        // Give it this object ref
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
