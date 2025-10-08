using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Elevator : MonoBehaviour
{
    [SerializeField] GameObject platformToLift;
    [SerializeField] GameObject refferenceObject;
    Vector3 initialPosition;
    Vector3 savedPosition;
    Vector3 floor1 = new Vector3(120.75f, 0.25f, 248.75f);
    Vector3 floor2 = new Vector3(120.75f, 6.25f, 248.75f);
    bool isMoving = false;
    bool isLifted = false;
    float delayTime = 2f;
    float currentTime = 2f;

    private void Start()
    {
        if (platformToLift != null)
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
        if (refferenceObject != null)
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

    public void SaveElevator()
    {
        savedPosition = gameObject.transform.position;
    }    

    public void GoFloor1()
    {
        gameObject.transform.DOMove(floor1, 3f);
    }

    public void GoFloor2()
    {
        gameObject.transform.DOMove(floor2, 3f);
    }

    public void RestorePosition()
    {
        gameObject.transform.position = savedPosition;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
    }
}
