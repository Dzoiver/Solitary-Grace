using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip detarame;
    AudioClip defaultClip;
    bool heardDetarame = false;
    // Start is called before the first frame update
    void Start()
    {
        defaultClip = audio.clip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toggle()
    {
        if (audio.isPlaying)
            audio.Pause();
        else
        {
            if (audio.clip == detarame)
            {
                heardDetarame = true;
            }
            audio.Play();
        }
            
    }

    public void BreakMusic()
    {
        audio.clip = detarame;
    }

    public void RepairMusic()
    {
        if (heardDetarame)
        {
            audio.clip = defaultClip;
            audio.Play();
        }
    }
}
