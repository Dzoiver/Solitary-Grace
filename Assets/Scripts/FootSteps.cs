using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    AudioSource audio;
    float currentTime = 0f;
    float stepTime = 0.42f;
    float stepSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void TryStep(Material material, float speed)
    {
        if (currentTime < stepTime || speed < stepSpeed)
            return;
        switch (material)
        {
            default:
                audio.PlayOneShot(Resources.Load<AudioClip>("Sounds/Footsteps1"));
                break;
        }

        currentTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
    }
}
