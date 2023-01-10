using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Lever : MonoBehaviour
{
    [SerializeField] GameObject wall;
    [SerializeField] GameObject endValueObject;
    [SerializeField] GameObject handle;
    [SerializeField] GameObject handleRef;

    private void OnTriggerEnter(Collider other)
    {
        wall.transform.DOMove(endValueObject.transform.position, 5f);
        handle.transform.DORotate(handleRef.transform.rotation.eulerAngles, 2f);
    }
}
