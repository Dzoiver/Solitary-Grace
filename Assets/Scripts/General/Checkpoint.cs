using DG.Tweening;
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
    [SerializeField] GameObject eyeModel;
    [SerializeField] AudioSource music;
    bool playMusic = false;

    public bool PlayMusic { get => playMusic; set 
            {
            if (!value)
            {
                playMusic = false;
                music.Pause();
            }
            else if (!playMusic)
            {
                playMusic = true;
                music.Play();
                music.DOFade(0.1f, 5f);
            }
             }
    }

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

    private void FixedUpdate()
    {
        Vector3 direction = GameFuncs.PlayerScript.transform.position - eyeModel.transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);

            eyeModel.transform.rotation = Quaternion.Slerp(eyeModel.transform.rotation, rotation, 5f * Time.deltaTime);
        }

        if (Mathf.Abs(GameFuncs.PlayerScript.transform.position.y - transform.position.y) < 1f
            && particle.isPlaying)
        {
            PlayMusic = true;
        }
        else
            PlayMusic = false;
    }

    public void UpdateSpawn()
    {
        if (particle.isPlaying)
            return;

        if (sleep.checkpoint != null)
        {
            sleep.checkpoint.particle.Stop();
            //sleep.checkpoint.gameObject.transform.parent.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
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

    public void SmoothTeleport(GameObject objectToTeleport)
    {
        UpdateSpawn();
        GameFuncs.DisableWeapons(true);
        StartCoroutine(CoroutineSmooth(objectToTeleport));
    }

    IEnumerator CoroutineSmooth(GameObject objectToTeleport)
    {
        float timetoFade = 0.3f;
        GameFuncs.FadeIn(timetoFade);
        yield return new WaitForSeconds(timetoFade);
        GameFuncs.TeleportPlayer(objectToTeleport);
        GameFuncs.FadeOut(timetoFade);
    }
}
