using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FollowList : MonoBehaviour
{
    StreamChannel currentChannel;
    [SerializeField] TextMeshProUGUI streamTitle;
    [SerializeField] TextMeshProUGUI viewerCount;
    [SerializeField] GameObject chat;
    GameObject viewerCountParent;
    Chat chatScript;
    // Start is called before the first frame update
    void Start()
    {
        viewerCountParent = viewerCount.transform.parent.gameObject;
        chatScript = chat.GetComponent<Chat>();
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
