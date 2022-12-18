using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrashPickup : MonoBehaviour
{
    [SerializeField] GameObject outTrigger;
    [SerializeField] GameObject doorMessage;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        outTrigger.SetActive(true);
        doorMessage.SetActive(false);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
