using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;
using Zenject;

public class SearchTheBody : MonoBehaviour
{
    [Inject] Menu menu;
    [Inject] Inventory inventory;
    [SerializeField] ScriptableItem key;
    private bool keyTaken = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "UseCube")
            return;

        if (keyTaken)
            return;

        GameFuncs.PlayerScript.SetControl(false);

        // Do you want to take the key card?
        menu.OpenMenu();

        // Call dialogue window

        // Give it this object ref

        ResultAction(true);
    }

    public void ResultAction(bool yes)
    {
        if (yes)
        {
            InventoryItem item = new InventoryItem(key.id, key.maxQuantity, key.name);
            inventory.AddItem(item);
            keyTaken = true;
            // give key
        }
        else
        {
        }

        GameFuncs.PlayerScript.SetControl(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
