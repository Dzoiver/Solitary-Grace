using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SolitaryAudio
{
    public class MusicAmbientController : MonoBehaviour
    {
        [SerializeField] private AudioSource music;
        [SerializeField] private AudioSource ambient;
        [SerializeField] private AudioSource sounds;
        private AudioClip piano;
        private float ambientVolume = 1f;
        private float musicVolume = 1f;
        private float soundVolume = 1f;

        public float AmbientVolume { get => ambientVolume; set => ambientVolume = value; }
        public float MusicVolume { get => musicVolume; set => musicVolume = value; }
        public float SoundVolume { get => soundVolume; set => soundVolume = value; }

        // Start is called before the first frame update
        void Start()
        {

        }
        public void PlayMusic(string audioName, float volume = 1f)
        {
            music.volume = volume;
            switch (audioName)
            {
                case "Piano":
                    music.clip = piano;
                    break;
            }
            music.Play();
        }

        public void PlayMusic(AudioClip clip)
        {
            /*
            music.volume = volume;
            switch (audioName)
            {
                case "Piano":
                    music.clip = piano;
                    break;
            }
            */
            music.volume = musicVolume;
            music.clip = clip;
            music.Play();
        }

        public void PlayAmbient(AudioClip clip)
        {
            ambient.volume = ambientVolume;
            ambient.clip = clip;
            ambient.Play();
        }

        public void StopAmbient()
        {
            ambient.DOFade(0f, 1f);
            //ambient.Stop();
        }

        public void StopMusic()
        {
            music.DOFade(0f, 1f);
            // music.clip = null;
        }

        public void PlaySound(AudioClip clip)
        {
            sounds.volume = soundVolume;
            sounds.clip = clip;
            sounds.Play();
        }

        public void PlayAmbientFade(AudioClip clip)
        {
            if (ambient.isPlaying)
                return;
            ambient.volume = ambientVolume;
            ambient.clip = clip;
            ambient.DOFade(1f, 1f);
        }
    }
}
