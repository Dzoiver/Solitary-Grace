using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrowserManager : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseButton()
    {
        gameObject.SetActive(false);
    }
}
