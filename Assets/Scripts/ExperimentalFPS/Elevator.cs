using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GM;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Events;

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
    [SerializeField] private Vector3 firstFloor = new Vector3(0,0,0);
    public Vector3 Velocity { get; private set; }

    AudioSource audio;
    [SerializeField] DOTweenAnimation door1;
    [SerializeField] DOTweenAnimation door2;
    private bool doorsBusy = false;
    private int nextFloor = 2;
    public Vector3 horizontalTeleportPos;
    private bool teleportedHorizontally;
    [SerializeField] GameObject horizontalWall;
    public bool removeOnStart = false;
    public float speed = 2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        startPosition = transform.position;
        destinationFloor = startPosition;

        if (platformToLift != null)
            initialPosition = platformToLift.transform.position;

        lastPosition = transform.position;
    }

    private void Awake()
    {
        if (removeOnStart)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //GameFuncs.PlayerScript.gameObject.transform.SetParent(gameObject.transform);
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
            speed * Time.fixedDeltaTime
        );
            Velocity = (newPosition - rb.position) / Time.fixedDeltaTime;
            rb.MovePosition(newPosition);

            if (transform.position == destinationFloor)
            {
                OnStop();
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
        /*
        if (floor == currentFloor || moving)
            return;
        if (floor > currentFloor)
            destinationFloor.y += floorDistance * (floor - 1);
        else
            destinationFloor.y -= floorDistance * (floor);
        */
        if (floor == currentFloor)
            OpenDoors();
        
        if (floor == currentFloor || moving)
            return;

        nextFloor = floor;
        StartCoroutine(OnDoorsClosed());
    }

    public void RestorePosition()
    {
        gameObject.transform.position = savedPosition;
    }

    public void OnStop()
    {
        moving = false;
        audio.enabled = false;
        audio.volume = 0f;
        OpenDoors();
    }

    public void OpenDoors()
    {
        if (door1 == null)
            return;
        door1.DOPlayForward();
        door2.DOPlayForward();
    }

    public void CloseDoors()
    {
        if (door1 == null)
            return;
        door1.DOPlayBackwards();
        door2.DOPlayBackwards();
    }

    public void FreeDoors()
    {

    }

    public IEnumerator OnDoorsClosed()
    {
        CloseDoors();
        yield return new WaitForSeconds(1f);
        destinationFloor.y = firstFloor.y + nextFloor * floorDistance;
        currentFloor = nextFloor;
        moving = true;
        audio.enabled = true;
        audio.Play();
        audio.DOFade(0.2f, 2f);
    }

    public void TeleportHorizontally()
    {
        if (teleportedHorizontally)
            return;
        teleportedHorizontally = true;
        GameFuncs.TeleportRelatively(gameObject, horizontalTeleportPos);
        gameObject.transform.position = horizontalTeleportPos;
        horizontalWall.SetActive(true);
        door1.gameObject.SetActive(false);
        door2.gameObject.SetActive(false);
    }
}
