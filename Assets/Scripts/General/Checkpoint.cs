using GM;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] GameObject oldSpawn;
    [SerializeField] GameObject newSpawn;
    GetSomeSleep sleep;
    GetSomeSleep[] sleeps;
    public UnityEvent onTeleport;
    public ParticleSystem particle;
    [SerializeField] Material notActiveMaterial;
    [SerializeField] Material activeMaterial;
    MeshRenderer meshrenderer;
    AudioSource audio;
    private void Awake()
    {
        sleeps = FindObjectsOfType<GetSomeSleep>();
        foreach (GetSomeSleep sl in sleeps)
        {
            if (sl.gameObject.name == "GetSleep2")
            {
                sleep = sl;
            }
        }

        meshrenderer = GetComponent<MeshRenderer>();
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSpawn()
    {
        if (particle.isPlaying)
            return;

        if (sleep.checkpoint != null)
        {
            sleep.checkpoint.particle.Stop();
            sleep.checkpoint.meshrenderer.material = notActiveMaterial;
        }
        if (!particle.isPlaying)
            audio.Play();
        sleep.checkpoint = this;
        meshrenderer.material = activeMaterial;
        particle.Play();
        oldSpawn.transform.position = newSpawn.transform.position;
        oldSpawn.transform.rotation = newSpawn.transform.rotation;
    }

    public void OnTeleportInvoke()
    {
        onTeleport.Invoke();
    }
}
