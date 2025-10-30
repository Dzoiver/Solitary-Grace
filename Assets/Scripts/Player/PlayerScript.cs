using UnityEngine;
using GM;
using Zenject;

public class PlayerScript : MonoBehaviour
{
    [Inject] GameOver gameover;
    [SerializeField] GameObject useTrigger;
    [SerializeField] GameObject cyllinder;
    [SerializeField] GameObject warpTool;
    [SerializeField] LayerMask layer;
    [SerializeField] Transform GroundCheck;
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
        GameFuncs.PlayerScript = this;
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
        playerCam.GetComponent<CameraReturnControls>().SwitchToPlayer();
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
        if (allowControl)
            return true;
        else
            return false;
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

    public void ToggleNoclip()
    {
        isNoclip = !isNoclip;
        gameObject.layer = isNoclip ? 12 : 6;
        Debug.Log($"Noclip {(isNoclip ? "on" : "off")}");
    }

    private void ApplyGravity()
    {
        if (isGrounded && velocity.y < 0 && !isNoclip) // Gravity even when grounded
        {
            velocity.y = -4f;
        }
        if (!isNoclip)
            velocity.y += gravityEffect;

        if (allowMovement)
            controller.Move(velocity * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, GroundMask);

        if (inElevator && currentElevator != null)
        {
            controller.Move(move * speed * Time.fixedDeltaTime);
            velocity.y = 0;
            Vector3 elevatorMovement = currentElevator.Velocity * Time.fixedDeltaTime;
            controller.Move(elevatorMovement);
        }
        else
        {
            ApplyGravity();
        }
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        move = transform.right * x + transform.forward * z;
        if (allowMovement && !inElevator)
            controller.Move(move * speed * Time.deltaTime);

        if (isNoclip)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 15f;
            }
            else
            {
                speed = BASE_SPEED;
            }

            isGrounded = false;

            if (Input.GetKey(KeyCode.Space))
            {
                velocity.y = 10f;
            }
            else
                velocity.y = 0f;

            if (Input.GetKey(KeyCode.LeftControl))
            {
                velocity.y = -10f;
            }
        }

        if (!allowControl)
            return;

        if (Input.GetButtonDown("Jump") && isGrounded && AllowJump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
        }

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
