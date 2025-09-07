using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ItemPickup : MonoBehaviour
{
    private Menu menu;
    [SerializeField] ScriptableItem item;
    [SerializeField] private bool destroyOnPickUp = true;

    private void Start()
    {
        menu = FindObjectOfType<Menu>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "UseCube")
        {
            if (destroyOnPickUp)
            {
                menu.ConfirmBox(item, gameObject);
            }
            else
            {
                menu.ConfirmBox(item);
            }
        }
    }
}
