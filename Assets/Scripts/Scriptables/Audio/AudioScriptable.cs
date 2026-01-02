using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSettings", menuName = "ScriptableObjects/AudioSettings", order = 3)]
public class AudioScriptable : ScriptableObject
{
    public AudioClip pickupSound;
    public float pickupSoundSoundVolume;
    public AudioClip pickupErrorSound;
    public float pickupErrorSoundVolume;
}
