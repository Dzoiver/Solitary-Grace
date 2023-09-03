using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchTheBody : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "UseCube")
            return;

        // Do you want to take the key card?

        // Call dialogue window

        // Give it this object ref
    }

    public void ResultAction(bool yes)
    {
        if (yes)
        {
            // give key
        }
        else
        {
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
