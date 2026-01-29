using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    [SerializeField] AudioClip[] grassClips;
    [SerializeField] AudioClip waterClip;
    [SerializeField] AudioClip[] defaultClips;
    [SerializeField] AudioClip[] woodClips;
    [SerializeField] AudioClip[] metalClips;
    AudioSource audio;
    float currentTime = 0f;
    float stepTime = 0.42f;
    public float stepSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void TryStep(GameObject ground, float speed)
    {
        if (currentTime < stepTime || speed < stepSpeed)
            return;

        float rng = Random.Range(0.75f, 1.25f);
        audio.pitch = rng;

        if (ground.CompareTag("Water"))
        {
            audio.PlayOneShot(waterClip);
        }
        else if (ground.CompareTag("Wood"))
        {
            audio.volume = 0.08f;
            audio.PlayOneShot(woodClips[Random.Range(0, woodClips.Length)]);
        }
        else if (ground.CompareTag("Metal"))
        {
            audio.volume = 0.17f;
            audio.PlayOneShot(metalClips[Random.Range(0, metalClips.Length)]);
        }
        else
        {
            audio.volume = 0.1f;
            audio.PlayOneShot(defaultClips[Random.Range(0, defaultClips.Length)]);
        }

        currentTime = 0f;
    }

    public void TryStepTerrain(Terrain terrain, float speed)
    {
        if (currentTime < stepTime || speed < stepSpeed)
            return;

        audio.volume = 0.16f;
        //TerrainData data = terrain.terrainData;

        int rng = Random.Range(0, grassClips.Length);
        audio.PlayOneShot(grassClips[rng]);

        currentTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
    }
}
