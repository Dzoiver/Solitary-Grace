using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyChanger : MonoBehaviour
{
    [SerializeField] Material skycow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        RenderSettings.skybox = skycow;
    }

    private void OnDisable()
    {
        RenderSettings.skybox = null;
    }
}
