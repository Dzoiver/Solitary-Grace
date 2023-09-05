using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SolitaryAudio
{
    public class MusicController : MonoBehaviour
    {
        static private AudioSource music;
        static private AudioClip piano;
        // Start is called before the first frame update
        void Start()
        {
            music = GetComponent<AudioSource>();
            piano = Resources.Load<AudioClip>("Sounds/Horror Elements/Save Room/SR_Piano_room");
        }
        static public void PlayMusic(string audioName)
        {

            switch (audioName)
            {
                case "Piano":
                    music.clip = piano;
                    break;
            }
            music.Play();
        }

        static public void StopMusic()
        {
            music.DOFade(0f, 1f);
            // music.clip = null;
        }
    }
}
