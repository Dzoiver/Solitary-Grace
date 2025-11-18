using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatHistory : MonoBehaviour
{
    Transform[] messages;
    // Start is called before the first frame update
    void Start()
    {
        messages = GetComponents<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMessage(string text)
    {

    }    

    public void ClearChat()
    {
        foreach(Transform mes in messages)
        {
            mes.gameObject.SetActive(false);
        }
    }
}
