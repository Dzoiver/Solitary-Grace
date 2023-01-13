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
        static public AudioClip switchSound;

        static private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            doorClose = Resources.Load<AudioClip>("Sounds/door-5-close");
            doorOpen = Resources.Load<AudioClip>("Sounds/door-14-open");
            switchSound = Resources.Load<AudioClip>("Sounds/switch-1");
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
                case "switch":
                    audioSource.clip = switchSound;
                    break;
            }
            audioSource.Play();
        }
    }
}
