using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;
using SolitaryAudio;

public class DownStairs : MonoBehaviour
{
    private bool triggeredOnce;
    [SerializeField] TeleportPlayer deathtriggerScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggeredOnce)
        {
            AudioController.Play("horrific", 1);
            deathtriggerScript.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
