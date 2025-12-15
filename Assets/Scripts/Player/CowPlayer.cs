using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private float jumpHeight = 3f;
    private float groundDistance = 0.5f;
    [SerializeField] private bool allowMovement = true;
    [SerializeField] private bool allowControl = true;
    private bool isGrounded;
    private Vector3 velocity;
    private bool isNoclip = false;
    public bool inElevator = false;
    public bool AllowJump = false;

    [HideInInspector] public CharacterController controller;
    [SerializeField] GameObject playerCam;
    Vector3 playerCamStartPos;
    public LayerMask GroundMask;

    Vector3 move;

    public Elevator currentElevator;

    private float health = 100f;
    private float maxHealth = 100f;

    private Animator cameraAnimator;
    private float gravityEffect;

    public float GravityMultiplier
    {
        get => gravityMultiplier;
        set
        {
            gravityMultiplier = value;
            UpdateGravityCalculations();
        }
    }

    private void Awake()
    {
        cameraAnimator = playerCam.GetComponent<Animator>();
        UpdateGravityCalculations();
    }

    private void UpdateGravityCalculations()
    {
        gravityEffect = gravity * gravityMultiplier * Time.fixedDeltaTime;
    }

    public void GetDamage(float damage)
    {
        if (health <= 0) return;

        health = Mathf.Max(0, health - damage);

        if (health <= 0)
        {
            Death();
        }
        else
        {
            gameover.GetDamagedRedScreen();
        }
    }

    public void GiveHP(float amount)
    {
        health = Mathf.Min(maxHealth, health + amount);
    }

    public bool IsDead() => health <= 0;

    private void Death()
    {
        cameraAnimator.enabled = true;
        cameraAnimator.Play("Deathanim");
        gameover.DieFromMonster();
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

    public void Warping(bool value)
    {
        if (value == true)
            warpTool.SetActive(true);
        else
            warpTool.SetActive(false);
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
        controller = GetComponent<CharacterController>();
        playerCamStartPos = playerCam.transform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "GunCyllinder")
        {
            cyllinder.SetActive(true);
        }
        if (other.gameObject.name == "JumpPad")
        {
            LiftPlayer(8f);
        }
    }

    private void LiftPlayer(float amount)
    {
        velocity.y = Mathf.Sqrt(amount * -2f * gravity);
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

        /*
        if (inElevator)
        {
            controller.Move(currentElevator.Velocity);
        }
        else
        {
            ApplyGravity();
        }
        */


        HandleInteract();
    }

    private void LateUpdate()
    {

    }

    private void HandleInteract()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Debug.DrawRay(ray.origin, ray.direction, Color.white, 5f);

            if (Physics.Raycast(ray, out hit, INTERACT_DISTANCE, layer.value))
            {
                if (hit.collider.gameObject.layer == 3) // Layer 3 - Interactable
                {
                    useTrigger.SetActive(true);
                    useTrigger.transform.position = hit.point;
                }
            }
        }
    }
}
