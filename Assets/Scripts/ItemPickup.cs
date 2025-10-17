using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class ItemPickup : MonoBehaviour
{
    // private Menu menu;
    Inventory inventory;
    public ScriptableItem item;
    public UnityEvent onPickup;
    [SerializeField] private bool destroyOnPickUp = true;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        // menu = FindObjectOfType<Menu>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "UseCube")
        {
            inventory.TryPickup(gameObject, item, destroyOnPickUp);
            onPickup.Invoke();
        }
    }
}
