using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ItemPickup : MonoBehaviour
{
    private Menu menu;
    [Inject] Inventory inventory;
    public ScriptableItem item;
    [SerializeField] private bool destroyOnPickUp = true;

    private void Start()
    {
        // inventory = FindObjectOfType<Inventory>();
        menu = FindObjectOfType<Menu>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "UseCube")
        {
            inventory.TryPickup(gameObject, item, destroyOnPickUp);
        }
    }
}
