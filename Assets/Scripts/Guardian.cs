using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardian : MonoBehaviour
{
    [SerializeField] AudioClip growl1;
    [SerializeField] AudioClip growl2;
    [SerializeField] AudioClip killPlayerGrowl;
    [SerializeField] AudioClip distractedSound;
    AudioSource audio;
    float currentGrowlTime = 0f;
    float growlTime = 3f;
    bool growl = false;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        // GROWLING
        currentGrowlTime += Time.deltaTime;
        if (currentGrowlTime > growlTime && growl)
        {
            int rng = Random.Range(0, 2);
            switch (rng)
            {
                case 0:
                    audio.PlayOneShot(growl1);
                    break;
                case 1:
                    audio.PlayOneShot(growl2);
                    break;
            }
            currentGrowlTime = 0f;
        }
        // GROWLING


    }

    public void Growl()
    {
        currentGrowlTime = 0f;
        growl = true;
    }

    public void StopGrowl()
    {
        growl = false;
    }

    public void EatPlayer()
    {
        audio.Stop();
        growl = false;
        audio.PlayOneShot(killPlayerGrowl);
    }

    public void Distract()
    {
        growl = false;
        audio.PlayOneShot(distractedSound);
    }
}
