using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;

public class Teleport : MonoBehaviour
{
    [SerializeField] GameObject destination;

    private void OnTriggerEnter(Collider other)
    {
        GameFuncs.TeleportPlayer(destination);
    }
}
