using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;
using SolitaryAudio;

public class DownStairs : MonoBehaviour
{
    private bool triggeredOnce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggeredOnce)
        {
            AudioController.Play("horrific", 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
