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
    public bool moving = false;
    bool playerInElevator = false;
    [SerializeField] float floorDistance = 6f;

    private Rigidbody rb;
    private Vector3 startPosition;

    private Vector3 lastPosition;
    [SerializeField] private Vector3 firstFloor = new Vector3(0,0,0);
    public Vector3 Velocity { get; private set; }

    [SerializeField] AudioSource audio;
    [SerializeField] AudioSource audio2;
    [SerializeField] AudioClip buttonClick;
    [SerializeField] AudioClip elevatorArrive;
    [SerializeField] AudioClip horElev;
    [SerializeField] DOTweenAnimation door1;
    [SerializeField] DOTweenAnimation door2;
    private bool doorsBusy = false;
    private int nextFloor = 2;
    public Vector3 horizontalTeleportPos;
    private bool teleportedHorizontally;
    [SerializeField] GameObject horizontalWall;
    public bool removeOnStart = false;
    public bool ding = false;
    public float speed = 2f;
    static public float goingUp = 1f;
    [SerializeField] DOTweenAnimation outer1Floor0;
    [SerializeField] DOTweenAnimation outer2Floor0;
    [SerializeField] DOTweenAnimation outer1Floor1;
    [SerializeField] DOTweenAnimation outer2Floor1;
    [SerializeField] DOTweenAnimation outer1Floor2;
    [SerializeField] DOTweenAnimation outer2Floor2;

    [SerializeField] SimpleTrigger button1;
    [SerializeField] SimpleTrigger button2;
    [SerializeField] SimpleTrigger button3;
    [SerializeField] CallButton[] callButtons;
    [SerializeField] bool doorsAutoClose = true;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            //GameFuncs.PlayerScript.gameObject.transform.SetParent(null);
            playerInElevator = false;
            GameFuncs.PlayerScript.inElevator = false;
            GameFuncs.PlayerScript.currentElevator = null;
        }
        
    }

    private void Update()
    {
        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinationFloor, speed * Time.deltaTime);
            
            /*
            Vector3 newPosition = Vector3.MoveTowards(
            rb.position,
            destinationFloor,
            speed * Time.fixedDeltaTime
        );
            Velocity = (newPosition - rb.position) / Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
            */
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

    private void LateUpdate()
    {
    }

    private void FixedUpdate()
    {

    }

    public void SaveElevator()
    {
        savedPosition = gameObject.transform.position;
    }

    public void MoveToFloor(int newFloor)
    {
        /*
        if (floor == currentFloor || moving)
            return;
        if (floor > currentFloor)
            destinationFloor.y += floorDistance * (floor - 1);
        else
            destinationFloor.y -= floorDistance * (floor);
        */
        if (audio2)
        {
            audio2.clip = buttonClick;
            audio2.Play();
        }
        if (moving)
            return;
        if (newFloor > currentFloor)
            goingUp = 1f;
        else
            goingUp = -1f;

        if (callButtons.Length > 0) // Managing CallButtons
        {
            foreach (CallButton cb in callButtons)
            {
                cb.Deactivate();
            }
            callButtons[newFloor].Activate();
        }

        if (newFloor == currentFloor)
        {
            OpenDoors(); // Animation
            OpenOuterDoors(currentFloor);
        }
        else
        {
            nextFloor = newFloor;
            if (button1 != null)
                button1.SetActiveTrigger(false);
            if (button2 != null)
                button2.SetActiveTrigger(false);
            if (button3 != null)
                button3.SetActiveTrigger(false);
            StartCoroutine(OnDoorsClosed());
        }
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
        OpenOuterDoors(currentFloor);

        if (ding)
        {
            audio2.clip = elevatorArrive;
            audio2.Play();
        }

        if (button1 != null)
            button1.SetActiveTrigger(true);
        if (button2 != null)
            button2.SetActiveTrigger(true);
        if (button3 != null)
            button3.SetActiveTrigger(true);
    }

    public void OpenDoors()
    {
        if (door1 == null)
            return;
        door1.DOPlayForward();
        door2.DOPlayForward();
        if (doorsAutoClose)
        {
            StartCoroutine(TimerCloseDoor());
        }
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

    public IEnumerator TimerCloseDoor()
    {
        yield return new WaitForSeconds(8f);
        CloseDoors();
        CloseOuterDoors(0);
        CloseOuterDoors(1);
        CloseOuterDoors(2);
    }

    public IEnumerator OnDoorsClosed()
    {
        CloseDoors();
        CloseOuterDoors(currentFloor);
        yield return new WaitForSeconds(1.5f);
        destinationFloor.y = firstFloor.y + nextFloor * floorDistance;
        currentFloor = nextFloor;
        moving = true;
        audio.enabled = true;
        audio.Play();
        audio.DOFade(0.4f, 2f);
    }

    public void TeleportHorizontally()
    {
        if (teleportedHorizontally)
            return;

        audio2.clip = horElev;
        audio2.enabled = true;
        audio2.Play();
        teleportedHorizontally = true;
        GameFuncs.TeleportRelatively(gameObject, horizontalTeleportPos);
        gameObject.transform.position = horizontalTeleportPos;
        horizontalWall.SetActive(true);
        door1.gameObject.SetActive(false);
        door2.gameObject.SetActive(false);

        Camera.main.DOShakePosition(11f, 0.1f, 10, 90f, false).OnComplete(() =>
        {
            Camera.main.DOShakePosition(4f, 0.05f, 5, 90f, true);
        });
    }

    public void OpenOuterDoors(int floor)
    {
        if (floor == 0)
        {
            if (outer1Floor0 == null || outer2Floor0 == null)
                return;
            outer1Floor0.DOPlayForward();
            outer2Floor0.DOPlayForward();
        }
        if (floor == 1)
        {
            if (outer1Floor1 == null || outer2Floor1 == null)
                return;
            outer1Floor1.DOPlayForward();
            outer2Floor1.DOPlayForward();
        }
        if (floor == 2)
        {
            if (outer1Floor2 == null || outer2Floor2 == null)
                return;
            outer1Floor2.DOPlayForward();
            outer2Floor2.DOPlayForward();
        }
    }

    public void CloseOuterDoors(int floor)
    {
        if (floor == 0)
        {
            if (outer1Floor0 == null || outer2Floor0 == null)
                return;
            outer1Floor0.DOPlayBackwards();
            outer2Floor0.DOPlayBackwards();
        }
        if (floor == 1)
        {
            if (outer1Floor1 == null || outer2Floor1 == null)
                return;
            outer1Floor1.DOPlayBackwards();
            outer2Floor1.DOPlayBackwards();
        }
        if (floor == 2)
        {
            if (outer1Floor2 == null || outer2Floor2 == null)
                return;
            outer1Floor2.DOPlayBackwards();
            outer2Floor2.DOPlayBackwards();
        }
    }
}
