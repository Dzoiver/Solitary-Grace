using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] Light livingRoomLight;

    private void OnTriggerEnter(Collider other)
    {
        if (livingRoomLight.enabled)
        {
            livingRoomLight.enabled = false;
        }
        else
        {
            livingRoomLight.enabled = true;
        }
    }
}
