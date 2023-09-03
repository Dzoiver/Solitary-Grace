using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SolitaryAudio;

public class DiningRoom : MonoBehaviour
{
    private bool firstTime = true;
    private void OnTriggerEnter(Collider other)
    {
        if (firstTime)
        {
            MusicController.PlayMusic("Piano");
            firstTime = false;
        }
    }
}
