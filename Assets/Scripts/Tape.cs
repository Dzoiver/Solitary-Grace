using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Tape : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    [SerializeField] GameObject[] tapes;
    BoxCollider boxCollider;
    MeshCollider meshCollider;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        meshCollider = GetComponent<MeshCollider>();
    }

    public void RemoveTape()
    {
        particle.Play();
        boxCollider.enabled = false;
        meshCollider.enabled = false;
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
