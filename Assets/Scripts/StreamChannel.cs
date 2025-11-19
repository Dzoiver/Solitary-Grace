using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamChannel : MonoBehaviour
{
    public string name = "";
    public int viewers;
    public string streamTitle;
    bool opened = false;

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

    public void Close()
    {
        gameObject.SetActive(false);

    }

    public void Open()
    {
        gameObject.SetActive(true);
        
    }
}
