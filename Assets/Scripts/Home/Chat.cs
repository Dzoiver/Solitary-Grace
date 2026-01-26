using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    [SerializeField] StreamsManager streamManager;
    [SerializeField] ChatHistory history;
    [SerializeField] TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendMessageChat();
        }
    }

    public void SendMessageChat()
    {
        if (streamManager.currentChannel.name == "BathTub")
            streamManager.acquire.Wave();
        history.AddMessage("You: " + inputField.text);
        inputField.text = "";
    }

    public void ClearChat()
    {
        streamManager.acquire.Wave(false);
        history.ClearChat();
    }
}
