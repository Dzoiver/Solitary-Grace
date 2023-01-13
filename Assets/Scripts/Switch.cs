using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SolitaryAudio;

public class Switch : MonoBehaviour
{
    [SerializeField] Light livingRoomLight;

    private void OnTriggerEnter(Collider other)
    {
        if (livingRoomLight.enabled)
        {
            AudioController.Play("switch");
            livingRoomLight.enabled = false;
        }
        else
        {
            AudioController.Play("switch");
            livingRoomLight.enabled = true;
        }
    }
}
