using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GM;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;

public class Elevator : MonoBehaviour
{
    [SerializeField] GameObject platformToLift;
    [SerializeField] DOTweenAnimation animationdotween;
    Vector3 initialPosition;
    Vector3 savedPosition;
    [SerializeField] Vector3 destinationFloor = new Vector3(120.75f, 0.25f, 248.75f);
    [SerializeField] int currentFloor = 1;

    bool isMoving = false;
    bool isLifted = false;
    float delayTime = 2f;
    float currentTime = 2f;
    bool moving = false;
    bool playerInElevator = false;
    [SerializeField] float floorDistance = 6f;

    private Rigidbody rb;
    private Vector3 startPosition;

    private Vector3 lastPosition;
    public Vector3 Velocity { get; private set; }

    private void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        destinationFloor = startPosition;

        if (platformToLift != null)
            initialPosition = platformToLift.transform.position;

        lastPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // GameFuncs.PlayerScript.gameObject.transform.SetParent(gameObject.transform);
            playerInElevator = true;
            GameFuncs.PlayerScript.inElevator = true;
            GameFuncs.PlayerScript.currentElevator = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameFuncs.PlayerScript.gameObject.transform.SetParent(null);
            playerInElevator = false;
            GameFuncs.PlayerScript.inElevator = false;
            GameFuncs.PlayerScript.currentElevator = null;
        }
    }

    private void Update()
    {
    }

    private void LateUpdate()
    {
    }

    private void FixedUpdate()
    {

        if (moving)
        {
            //transform.position = Vector3.MoveTowards(transform.position, destinationFloor, 1f * Time.fixedDeltaTime);
            Vector3 newPosition = Vector3.MoveTowards(
            rb.position,
            destinationFloor,
            1f * Time.fixedDeltaTime
        );
            Velocity = (newPosition - rb.position) / Time.fixedDeltaTime;
            rb.MovePosition(newPosition);

            if (transform.position == destinationFloor)
            {
                moving = false;
            }
        }
        else
        {
            Velocity = Vector3.zero;
        }
    }

    public void SaveElevator()
    {
        savedPosition = gameObject.transform.position;
    }

    public void MoveToFloor(int floor)
    {
        if (floor == currentFloor || moving)
            return;
        if (floor > currentFloor)
            destinationFloor.y += floorDistance * (floor - 1);
        else
            destinationFloor.y -= floorDistance * (floor);
        currentFloor = floor;
        moving = true;
    }

    public void RestorePosition()
    {
        gameObject.transform.position = savedPosition;
    }
}
