using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] AudioMixer masterMixer;
    [SerializeField] Slider volumeSlider;
    private const string MasterVolumeParam = "MasterVolumeParam";

    private void Start()
    {
        // Optional: Load saved volume from PlayerPrefs on start
        if (PlayerPrefs.HasKey(MasterVolumeParam))
        {
            float savedVolume = PlayerPrefs.GetFloat(MasterVolumeParam);
            volumeSlider.value = Mathf.Pow(10, savedVolume / 20); // Convert back from logarithmic
            SetVolume();
        }
        else
        {
            volumeSlider.value = 1f; // Default to full volume
            SetVolume();
        }
    }

    public void SetVolume()
    {
        // Convert linear slider value to logarithmic for the Audio Mixer
        float volume = Mathf.Log10(volumeSlider.value) * 20;
        Debug.Log(volume);
        masterMixer.SetFloat(MasterVolumeParam, volume);

        // Optional: Save volume to PlayerPrefs
        PlayerPrefs.SetFloat(MasterVolumeParam, volume);
        PlayerPrefs.Save();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
