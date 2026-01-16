using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Tape : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    [SerializeField] GameObject[] tapes;
    BoxCollider trigger;
    [SerializeField] GameObject collider;
    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<BoxCollider>();
    }

    public void RemoveTape()
    {
        particle.Play();
        trigger.enabled = false;
        collider.SetActive(false);
        foreach (GameObject tape in tapes)
        {
            tape.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
