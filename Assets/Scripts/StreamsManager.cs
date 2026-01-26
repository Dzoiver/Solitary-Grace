using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class StreamsManager : MonoBehaviour
{
    private string title;
    public StreamChannel currentChannel;
    [SerializeField] TextMeshProUGUI streamTitle;
    [SerializeField] TextMeshProUGUI viewerCount;
    [SerializeField] GameObject chat;
    public Acquire acquire;
    GameObject viewerCountParent;
    Chat chatScript;

    public string Title { get => title; set => title = currentChannel.streamTitle; }

    // Start is called before the first frame update
    void Start()
    {
        viewerCountParent = viewerCount.transform.parent.gameObject;
        chatScript = chat.GetComponent<Chat>();
        title = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenStream(StreamChannel channel)
    {
        if (currentChannel != null)
            currentChannel.Close();
        channel.Open();
        currentChannel = channel;
        streamTitle.text = channel.streamTitle;
        viewerCountParent.SetActive(true);
        viewerCount.text = channel.viewers.ToString();
        chat.SetActive(true);
        chatScript.ClearChat();
    }

    public void CloseAll()
    {
        if (currentChannel != null)
            currentChannel.Close();
        viewerCountParent.SetActive(false);
        chat.SetActive(false);
    }
}
