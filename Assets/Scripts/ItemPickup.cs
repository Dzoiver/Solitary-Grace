using GM;
using SolitaryAudio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Zenject;

public class ItemPickup : MonoBehaviour
{
    // private Menu menu;
    Inventory inventory;
    public ScriptableItem item;
    public UnityEvent onPickup;
    MeshRenderer mesh;
    AudioSource audio;
    MeshCollider collider;
    BoxCollider box;
    [SerializeField] private bool destroyOnPickUp = true;
    [SerializeField] AudioClip healthDrinkPickupClip;
    [SerializeField] AudioClip defaultPickup;
    public bool disableObject1 = false;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        audio = GetComponent<AudioSource>();
        inventory = FindObjectOfType<Inventory>();
        collider = GetComponent<MeshCollider>();
        box = GetComponent<BoxCollider>();
        // menu = FindObjectOfType<Menu>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "UseCube")
        {
            other.gameObject.SetActive(false);
            //other.gameObject.SetActive(false);
            if (inventory.TryPickup(item))
            {
                if (destroyOnPickUp)
                {
                    if (box != null)
                        box.enabled = false;
                    if (mesh != null)
                        mesh.enabled = false;
                    else
                        gameObject.SetActive(false);

                    if (disableObject1)
                        gameObject.SetActive(false);
                }
                enabled = false;
                if (collider != null)
                    collider.enabled = false;
                
                AudioController.PlayOneShot(Resources.Load<AudioClip>("Sounds/pickup"));
                onPickup.Invoke();
            }
            else
            {
                AudioController.PlayOneShot(Resources.Load<AudioClip>("Sounds/pickupError"));
            }
        }
    }
}
