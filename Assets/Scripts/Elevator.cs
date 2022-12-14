using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Elevator : MonoBehaviour
{
    [SerializeField] GameObject platformToLift;
    [SerializeField] GameObject refferenceObject;
    Vector3 initialPosition;
    bool isMoving = false;
    bool isLifted = false;
    float delayTime = 2f;
    float currentTime = 2f;

    private void Start()
    {
        initialPosition = platformToLift.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isMoving && currentTime > delayTime && !isLifted)
            GoUp();
        else
        if (!isMoving && currentTime > delayTime)
            GoDown();
    }

    private void GoUp()
    {
        isMoving = true;
        platformToLift.transform.DOMove(refferenceObject.transform.position, 5f).SetUpdate(UpdateType.Normal, false).onComplete = () =>
        {
            isLifted = true;
            isMoving = false;
            currentTime = 0f;
        };
    }

    private void GoDown()
    {
        isMoving = true;
        platformToLift.transform.DOMove(initialPosition, 5f).onComplete = () =>
        {
            isLifted = false;
            isMoving = false;
            currentTime = 0f;
        };
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
    }
}
