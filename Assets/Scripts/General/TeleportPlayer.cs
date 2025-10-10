using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    MouseLook mouseLook;
    PlayerScript player;
    [SerializeField] GameObject destination;
    // Start is called before the first frame update
    void Start()
    {
        mouseLook = FindObjectOfType<MouseLook>();
        player = FindObjectOfType<PlayerScript>();
    }

    public void TeleportPlayerTo()
    {
        mouseLook.CenterView();
        player.controller.enabled = false;
        player.gameObject.transform.position = destination.transform.position;
        player.gameObject.transform.rotation = destination.transform.rotation;
        player.controller.enabled = true;
    }
}
