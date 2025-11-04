using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeHitbox : MonoBehaviour
{
    [SerializeField] Knife knife;
    AudioSource audio;
    private float hitboxLingerTime = 0.1f;
    private float currentHitboxLingerTime = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            knife.KnifeSound(Resources.Load<AudioClip>("Sounds/monsterHit"));
            other.gameObject.GetComponent<Monster>().GetDamage(knife.GetDamageValue());
        }
        
        if (other.gameObject.CompareTag("Tape"))
        {
            other.gameObject.GetComponent<Tape>().RemoveTape();
        }

        if (other.gameObject.CompareTag("Box"))
        {
            other.gameObject.GetComponent<DestroyableBox>().DestroyBox();
        }
    }

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        gameObject.SetActive(false);
        GetComponent<MeshRenderer>().enabled = false;
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
}
