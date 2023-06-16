using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrashPickup : MonoBehaviour
{
    [SerializeField] GameObject outTrigger;
    [SerializeField] GameObject doorMessage;

    private void OnTriggerEnter(Collider other)
    {
        outTrigger.SetActive(true);
        doorMessage.SetActive(false);
        gameObject.SetActive(false);
    }
}
