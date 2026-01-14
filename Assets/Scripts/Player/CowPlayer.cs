using GM;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

public class CowPlayer : MonoBehaviour
{
    [Inject] GameOver gameover;
    [SerializeField] GameObject useTrigger;
    [SerializeField] GameObject cyllinder;
    [SerializeField] GameObject warpTool;
    [SerializeField] LayerMask layer;
    [SerializeField] MouseLook _mouse;

    private const float BASE_SPEED = 5f;
    private const float BASE_GRAVITY = -9.81f;
    private const float GROUND_DISTANCE = 0.5f;
    private const float GROUNDED_VELOCITY_Y = -4f;
    private const float INTERACT_DISTANCE = 1.5f;

    private float speed = BASE_SPEED;
    private float gravity = BASE_GRAVITY;
    public float gravityMultiplier = 1.0f;
    public bool gravityAllowed = true;
    [SerializeField] private bool allowMovement = true;
    [SerializeField] private bool allowControl = true;
    private Vector3 velocity;
    public bool inElevator = false;
    public bool AllowJump = false;

    [HideInInspector] public CharacterController controller;
    [SerializeField] GameObject playerCam;
    public CowBazooka bazooka;
    Vector3 playerCamStartPos;
    public LayerMask GroundMask;

    Vector3 move;

    public Elevator currentElevator;

    private float health = 100f;
    private float maxHealth = 100f;

    private Animator cameraAnimator;
    [SerializeField] TextMeshProUGUI ammoText;
    public CowLawn cowlawn;

    private int availableAmmo = 4;

    public float GravityMultiplier
    {
        get => gravityMultiplier;
        set
        {
            gravityMultiplier = value;
        }
    }

    public int AvailableAmmo { get => availableAmmo; set
        { 
            availableAmmo = value;
            ammoText.text = "Ammunition: " + value.ToString();
        }}

    private void Awake()
    {
        cameraAnimator = playerCam.GetComponent<Animator>();
    }

    public void CameraRestore()
    {
        playerCam.transform.localPosition = playerCamStartPos;
        //playerCam.GetComponent<CameraReturnControls>().SwitchToPlayer();
    }

    public Vector3 GetCamera()
    {
        Vector3 temp = new Vector3();
        temp.x = playerCam.transform.rotation.eulerAngles.x;
        temp.y = gameObject.transform.rotation.eulerAngles.y;
        return temp;
    }
    public void SetCamera(Vector3 angle)
    {
        playerCam.transform.rotation = Quaternion.Euler(angle);
        gameObject.transform.rotation = Quaternion.Euler(angle);
    }

    public bool IsControl()
    {
        return allowControl;
    }

    /// <summary>
    /// Allowing or disabling player's movement
    /// </summary>
    /// <param name="allow"></param>
    public void SetControl(bool allow)
    {
        // Need to allow player to continue falling
        if (allow)
        {
            allowMovement = true;
            _mouse.AllowMove = true;
            allowControl = true;
        }
        else
        {
            allowMovement = false;
            _mouse.AllowMove = false;
            allowControl = false;
        }
    }

    private void Start()
    {
        AvailableAmmo = availableAmmo;
        controller = GetComponent<CharacterController>();
        playerCamStartPos = playerCam.transform.localPosition;
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        move = transform.right * x + transform.forward * z;
        if (allowMovement)
            controller.Move(move * speed * Time.deltaTime);

        if (!allowControl)
            return;

    }
}
