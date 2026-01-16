using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DestroyableBox : MonoBehaviour
{
    enum items
    {
        Nothing,
        HealthDrink,
        PistolAmmo,
        ShotgunAmmo,
        Valve,
    }

    [SerializeField] UnityEvent onBreak;
    [SerializeField] items item;
    [SerializeField] GameObject boxVisual;
    [SerializeField] ParticleSystem debris;
    public AudioClip[] clips;
    AudioSource audio;
    GameObject itemObject;
    BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider>();
        switch (item)
        {
            case items.Nothing:
                break;

            case items.Valve:
                itemObject = (GameObject)Instantiate(Resources.Load("Pickup/ValvePickup"), gameObject.transform.position, Quaternion.identity);
                break;
            case items.HealthDrink:
                itemObject = (GameObject)Instantiate(Resources.Load("Pickup/HealthDrink"), gameObject.transform.position, Quaternion.identity);
                break;

            case items.PistolAmmo:
                itemObject = (GameObject)Instantiate(Resources.Load("Pickup/PistolAmmo"), gameObject.transform.position, Quaternion.identity);
                break;
            case items.ShotgunAmmo:
                itemObject = (GameObject)Instantiate(Resources.Load("Pickup/ShotgunAmmo"), gameObject.transform.position, Quaternion.identity);
                break;
        }
        if (itemObject != null)
            itemObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            DestroyBox();
        }
    }

    public void DestroyBox()
    {
        boxVisual.SetActive(false);
        onBreak.Invoke();
        debris.Play();
        PlayRandomSound();
        if (itemObject != null)
            itemObject.SetActive(true);
        enabled = false;
        boxCollider.enabled = false;
    }

    public void PlayRandomSound()
    {
        audio.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }

    public void Restore()
    {
        boxVisual.SetActive(true);
        enabled = true;
        boxCollider.enabled = true;
    }
}
