using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    PlayerScript player;
    [SerializeField] GameObject destination;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerScript>();
    }

    public void TeleportPlayerTo()
    {
        player.controller.enabled = false;
        player.gameObject.transform.position = destination.transform.position;
        player.controller.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
