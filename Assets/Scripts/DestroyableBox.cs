using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableBox : MonoBehaviour
{
    enum items
    {
        HealthDrink,
        PistolAmmo,
        ShotgunAmmo
    }

    [SerializeField] items item;
    // Start is called before the first frame update
    void Start()
    {
        switch (item)
        {
            case items.HealthDrink:
                Instantiate(Resources.Load("Pickup/HealthDrink"), gameObject.transform.position, Quaternion.identity);
                break;

            case items.PistolAmmo:
                Instantiate(Resources.Load("Pickup/PistolAmmo"), gameObject.transform.position, Quaternion.identity);
                break;
            case items.ShotgunAmmo:
                Instantiate(Resources.Load("Pickup/ShotgunAmmo"), gameObject.transform.position, Quaternion.identity);
                break;
        }
        
    }

    public void DestroyBox()
    {
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
