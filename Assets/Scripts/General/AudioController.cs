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
        static public AudioClip horrific;
        static AudioSource[] sources;

        // Music
        static public AudioClip piano;

        static private AudioSource source1;
        static private AudioSource source2;


        private void Start()
        {
            sources = GetComponents<AudioSource>();
            source1 = sources[0];
            source2 = sources[1];
            doorClose = Resources.Load<AudioClip>("Sounds/door-5-close");
            doorOpen = Resources.Load<AudioClip>("Sounds/door-14-open");
            switchSound = Resources.Load<AudioClip>("Sounds/switch-1");
            horrific = Resources.Load<AudioClip>("Sounds/Horror Elements/Misc/Misc_horrific");
        }

        static public void Play(string audioName, int channel = 0)
        {
            AudioSource source = sources[channel];
            switch (audioName)
            {
                case "doorOpen":
                    source.clip = doorOpen;
                    break;
                case "doorClose":
                    source.clip = doorClose;
                    break;
                case "switch":
                    source.clip = switchSound;
                    break;
                case "horrific":
                    source.clip = horrific;
                    break;
                default:
                    source.clip = null;
                    break;
            }
            source.Play();
        }
    }
}
