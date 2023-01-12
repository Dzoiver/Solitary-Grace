using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SolitaryAudio
{
    public class AudioController : MonoBehaviour
    {
        static public AudioClip doorClose;
        static public AudioClip doorOpen;
        static private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            doorClose = Resources.Load<AudioClip>("Sounds/door-5-close");
            doorOpen = Resources.Load<AudioClip>("Sounds/door-14-open");
        }

        static public void Play(string audioName)
        {
            switch (audioName)
            {
                case "doorOpen":
                    audioSource.clip = doorOpen;
                    break;
                case "doorClose":
                    audioSource.clip = doorClose;
                    break;
            }
            audioSource.Play();
        }
    }
}
