using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowList : MonoBehaviour
{
    StreamChannel currentChannel;
    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    public void CloseAll()
    {
        if (currentChannel != null)
            currentChannel.Close();
    }
}
