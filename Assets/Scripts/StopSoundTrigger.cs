using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StopSoundTrigger : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioSource source2;
    private void SlowlyStop()
    {
        source.DOFade(0f, 6f);
        source2.DOFade(0.08f, 6f);
    }

    private void OnTriggerEnter(Collider other)
    {
        SlowlyStop();
    }
}
