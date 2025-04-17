using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;

public class ScriptedDeath : MonoBehaviour
{
    [SerializeField] GameObject teleportPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Random time wait
        // Monster on top of player camera
        // Time wait
        // Kill player
        // 
        GameFuncs.TeleportPlayer(teleportPoint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
