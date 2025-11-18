using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    [SerializeField] ChatHistory history;
    [SerializeField] TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SendMessageChat()
    {
        if (inputField.text.Contains("") || inputField.text.Contains(" "))
        {
            return;
        }

        inputField.text = "";
        history.AddMessage(inputField.text);
    }

    public void ClearChat()
    {
        history.ClearChat();
    }
}
