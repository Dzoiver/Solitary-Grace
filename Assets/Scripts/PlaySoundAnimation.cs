using SolitaryAudio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlaySound(AudioClip clip)
    {
        AudioController.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
