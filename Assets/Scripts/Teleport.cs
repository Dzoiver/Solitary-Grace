using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;

public class Teleport : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameFuncs.TeleportPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
