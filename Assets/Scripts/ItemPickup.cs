using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ItemPickup : MonoBehaviour
{
    [Inject] Menu menu;
    [SerializeField] ScriptableItem item;
    [SerializeField] private bool destroyOnPickUp = true;

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
