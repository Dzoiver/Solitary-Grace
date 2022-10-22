using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] Light livingRoomLight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
    // Update is called once per frame
    void Update()
    {
        
    }
}
