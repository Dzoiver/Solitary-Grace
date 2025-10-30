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
    GameObject itemObject;
    // Start is called before the first frame update
    void Start()
    {
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

    public void DestroyBox()
    {
        onBreak.Invoke();
        if (itemObject != null)
            itemObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void DropItem()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
