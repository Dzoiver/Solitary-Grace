using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Steam : MonoBehaviour
{
    float currentTime = 0f;
    float rngTime = 0f;
    AudioSource audio;
    ParticleSystem particle;
    // Start is called before the first frame update
    void Awake()
    {
        particle = GetComponent<ParticleSystem>();
        audio = GetComponent<AudioSource>();
        rngTime = Random.Range(3f, 20f);
    }

    private void OnEnable()
    {
        audio.pitch = Random.Range(0.8f, 1.2f);
        audio.Play();
        particle.Play();
        rngTime = Random.Range(3f, 20f);
        currentTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > rngTime)
        {
            if (Vector3.Distance(GameFuncs.PlayerScript.transform.position, transform.position) < 4f)
            {
                audio.pitch = Random.Range(0.8f, 1.2f);
                audio.Play();
                particle.Play();
                rngTime = Random.Range(3f, 20f);
                currentTime = 0f;
            }
        }
    }
}
