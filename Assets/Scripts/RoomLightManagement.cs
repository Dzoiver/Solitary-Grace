using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLightManagement : MonoBehaviour
{
    [SerializeField] Switch[] switches;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TurnOffLights()
    {
        foreach (Switch s in switches)
        {
            s.LightOff();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
