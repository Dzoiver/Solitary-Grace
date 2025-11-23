using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapWater : MonoBehaviour
{
    AudioSource audio;
    // Start is called before the first frame update
    private void Awake()
    {
        gameObject.SetActive(false);
        audio = GetComponent<AudioSource>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleTap()
    {
        bool open = gameObject.activeSelf ? false : true;
        gameObject.SetActive(open);
        audio.enabled = open;
    }
}
