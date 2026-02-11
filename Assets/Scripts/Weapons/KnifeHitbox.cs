using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class KnifeHitbox : MonoBehaviour
{
    [SerializeField] Knife knife;
    AudioSource audio;
    private float hitboxLingerTime = 0.1f;
    private float currentHitboxLingerTime = 0f;
    [SerializeField] ParticleSystem blood;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            knife.KnifeSound(Resources.Load<AudioClip>("Sounds/monsterHit"));
            blood.Stop();
            blood.Play();
            other.gameObject.GetComponent<Monster>().GetDamage(Random.Range(knife.minDamage, knife.maxDamage));
        }
        else if (other.gameObject.CompareTag("Tape"))
        {
            knife.KnifeSound(Resources.Load<AudioClip>("Sounds/paper-rip-fast"));
            other.gameObject.GetComponent<Tape>().RemoveTape();
        }
        else if (other.gameObject.CompareTag("Box"))
        {
            other.gameObject.GetComponent<DestroyableBox>().DestroyBox();
        }
        else if (other.gameObject.CompareTag("Boss"))
        {
            
            blood.Stop();
            blood.Play();
            knife.KnifeSound(Resources.Load<AudioClip>("Sounds/monsterHit"));
            other.gameObject.GetComponent<Boss>().GetDamage(knife.maxDamage);
        }
    }

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        gameObject.SetActive(false);
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnEnable()
    {
        //knife.KnifeSound(Resources.Load<AudioClip>("Sounds/air-whoosh"));
    }

    void Update()
    {
        currentHitboxLingerTime += Time.deltaTime;
        if (currentHitboxLingerTime > hitboxLingerTime)
        {
            gameObject.SetActive(false);
            currentHitboxLingerTime = 0f;
        }
    }

    public void GetHitInformation()
    {

    }
}
