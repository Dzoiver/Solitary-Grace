using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StopSoundTrigger : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioSource source2;
    [SerializeField] GameObject doorTrigger;
    private void OnTriggerEnter(Collider other)
    {
        doorTrigger.SetActive(false);
        source.DOFade(0f, 6f); // Stop room music
        source2.DOFade(0.08f, 6f); // Play creepy music
    }
}
