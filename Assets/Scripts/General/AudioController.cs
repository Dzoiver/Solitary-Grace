using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SolitaryAudio
{
    public class AudioController : MonoBehaviour
    {
        // Sound Effects
        static public AudioClip doorClose;
        static public AudioClip doorOpen;
        static public AudioClip switchSound;

        // Music
        static public AudioClip piano;

        static private AudioSource soundEffect;
        

        private void Start()
        {
            soundEffect = GetComponent<AudioSource>();
            doorClose = Resources.Load<AudioClip>("Sounds/door-5-close");
            doorOpen = Resources.Load<AudioClip>("Sounds/door-14-open");
            switchSound = Resources.Load<AudioClip>("Sounds/switch-1");
        }

        static public void Play(string audioName)
        {
            switch (audioName)
            {
                case "doorOpen":
                    soundEffect.clip = doorOpen;
                    break;
                case "doorClose":
                    soundEffect.clip = doorClose;
                    break;
                case "switch":
                    soundEffect.clip = switchSound;
                    break;
                default:
                    soundEffect.clip = null;
                    break;
            }
            soundEffect.Play();
        }
    }
}
