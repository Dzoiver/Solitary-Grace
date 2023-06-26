using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] GameObject referGameObj;
    Vector3 initVector;
    void Start()
    {
        initVector = transform.position;
        MoveNext();
    }

    void MoveNext()
    {
        if (initVector == transform.position)
            transform.DOMove(referGameObj.transform.position, 5f).onComplete = () => MoveNext();
        else
            transform.DOMove(initVector, 5f).onComplete = () => MoveNext();
    }
}
