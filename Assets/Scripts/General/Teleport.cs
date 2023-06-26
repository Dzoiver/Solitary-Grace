using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;

public class Teleport : MonoBehaviour
{
    [SerializeField] GameObject destination;
    [SerializeField] GameObject warpWorld;

    private void OnTriggerEnter(Collider other)
    {
        warpWorld.SetActive(true);
        GameFuncs.PlayerScript.Warping(true);
        GameFuncs.TeleportPlayer(destination);
    }
}
