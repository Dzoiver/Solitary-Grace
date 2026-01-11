using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOnFire : MonoBehaviour
{
    [SerializeField] Inventory inventory; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckBothItems()
    {
        if (inventory.Has(11) && inventory.Has(12))
        {

        }
    }
}
