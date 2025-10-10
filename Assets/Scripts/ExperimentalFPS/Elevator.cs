using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GM;
using Unity.VisualScripting;

public class Elevator : MonoBehaviour
{
    [SerializeField] GameObject platformToLift;
    [SerializeField] GameObject refferenceObject;
    [SerializeField] DOTweenAnimation animationdotween;
    Vector3 initialPosition;
    Vector3 savedPosition;
    Vector3 destinationFloor = new Vector3(120.75f, 0.25f, 248.75f);
    bool isMoving = false;
    bool isLifted = false;
    float delayTime = 2f;
    float currentTime = 2f;
    bool moving = false;
    int currentFloor = 0;
    bool playerInElevator = false;

    private void Start()
    {
        if (gameObject.transform.position.y == 0.25f)
            currentFloor = 1;
        else
            currentFloor = 2;
        if (platformToLift != null)
            initialPosition = platformToLift.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameFuncs.PlayerScript.gameObject.transform.SetParent(gameObject.transform);
            playerInElevator = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameFuncs.PlayerScript.gameObject.transform.SetParent(null);
            playerInElevator = false;
        }
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
        /*
        if (currentFloor == 1)
            return;
        if (moving)
            return;
        moving = true;
        //if (playerInElevator)
            //GameFuncs.PlayerScript.SetControl(false);
        gameObject.transform.DOMove(floor1, 3f).onComplete = () =>
        {
            
            currentFloor = 1;
            moving = false;
            //GameFuncs.PlayerScript.SetControl(true);
        };
        */
    }

    public void GoFloor2()
    {
        /*
        if (currentFloor == 2)
            return;
        if (moving)
            return;
        //GameFuncs.PlayerScript.gameObject.transform.SetParent(gameObject.transform);
        moving = true;
        //if (playerInElevator)
        //GameFuncs.PlayerScript.SetControl(false);
        gameObject.transform.DOMove(floor2, 3f).onComplete = () =>
        {
            currentFloor = 2;
            moving = false;
            //GameFuncs.PlayerScript.SetControl(true);
        };
        */
    }

    public void MoveToFloor(int floor)
    {
        currentFloor = floor;
        moving = true;
    }

    public void RestorePosition()
    {
        gameObject.transform.position = savedPosition;
    }

    private void Update()
    {
        if (moving)
        {
            destinationFloor.y = 0.25f + 6f * (currentFloor - 1);
            transform.position = Vector3.MoveTowards(transform.position, destinationFloor, 1f * Time.deltaTime);

            if (transform.position == destinationFloor)
            {
                moving = false;
            }
        }
    }
}
