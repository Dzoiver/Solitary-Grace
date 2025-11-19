using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatHistory : MonoBehaviour
{
    int freeMessageIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMessage(string newtext)
    {
        TextMeshProUGUI textobj = transform.GetChild(freeMessageIndex).GetComponent<TextMeshProUGUI>();
        textobj.gameObject.SetActive(true);
        textobj.text = newtext;
        freeMessageIndex = (freeMessageIndex + 1) % transform.childCount;
    }

    public void ClearChat()
    {
        foreach(Transform childTransorm in transform)
        {
            childTransorm.gameObject.SetActive(false);
        }
        freeMessageIndex = 0;
    }
}
