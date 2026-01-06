using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFader : MonoBehaviour
{
    [SerializeField] float volume = 1f;
    AudioSource audio;
    // Start is called before the first frame update
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    void Start()
    {
        audio.volume = volume;
        audio.DOFade(volume, 2f);
    }

    public void Fadeout()
    {
        audio.DOFade(0f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
