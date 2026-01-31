using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingSound : MonoBehaviour
{
    AudioSource audio;
    public float fadeinVolume = 1f;
    public float timeToFade = 3f;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeOutAudio()
    {
        
        audio.DOFade(0f, timeToFade);
    }

    public void FadeInAudio()
    {
        audio.DOFade(fadeinVolume, timeToFade);
        audio.Play();
    }
}
