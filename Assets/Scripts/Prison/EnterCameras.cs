using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SolitaryAudio;

public class EnterCameras : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        MusicController.StopMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
