using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] GameObject destinationPoint;
    [SerializeField] GameObject player;
    [SerializeField] CharacterController playercont;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("yes");
        playercont.enabled = false;
        player.transform.position = destinationPoint.transform.position;
        playercont.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
