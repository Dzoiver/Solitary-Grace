using UnityEngine;
using GM;
using Zenject;
using UnityEditor;
using SolitaryAudio;

public class PlayerScript : MonoBehaviour
{
    [Inject] GameOver gameover;
    [SerializeField] GameObject useTrigger;
    [SerializeField] GameObject cyllinder;
    [SerializeField] GameObject warpTool;
    [SerializeField] LayerMask layer;
    [SerializeField] Transform GroundCheck;
    [SerializeField] MouseLook _mouse;
    [SerializeField] FootSteps footsteps;

    private const float BASE_SPEED = 5f;
    private const float BASE_GRAVITY = -9.81f;
    private const float GROUND_DISTANCE = 0.5f;
    private const float GROUNDED_VELOCITY_Y = -4f;
    private const float INTERACT_DISTANCE = 1.7f;

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
    WeaponManager weaponManager;
    Menu menu;
    [SerializeField] AudioClip hurt;
    [SerializeField] AudioClip bubble;
    AudioSource audio;

    public float GravityMultiplier
    {
        get => gravityMultiplier;
        set
        {
            gravityMultiplier = value;
            UpdateGravityCalculations();
        }
    }

    public float Health { get => health; set => health = value; }

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        menu = FindObjectOfType<Menu>();
        GameFuncs.PlayerScript = this;
        cameraAnimator = playerCam.GetComponent<Animator>();
        UpdateGravityCalculations();
        weaponManager = FindObjectOfType<WeaponManager>();
    }

    private void UpdateGravityCalculations()
    {
        gravityEffect = gravity * gravityMultiplier * Time.fixedDeltaTime;
    }

    public void GetDamage(float damage)
    {
        if (Health <= 0) return;

        Health = Mathf.Max(0, Health - damage);
        audio.PlayOneShot(hurt, 0.2f);
        if (Health <= 0)
        {
            Death();
        }
        else
        {
            gameover.GetDamagedRedScreen();
        }

        if (Health <= 35)
        {
            gameover.bloodstaines.SetActive(true);
        }
        else
        {
            gameover.bloodstaines.SetActive(false);
        }
        //menu.ChangeHealth(Health);
    }

    public void GiveHP(float amount)
    {
        gameover.bloodstaines.SetActive(false);
        Health = Mathf.Min(maxHealth, Health + amount);
        menu.ChangeHealth(Health);
    }

    public float GetHP()
    {
        return Health;
    }

    public bool IsDead() => Health <= 0;

    private void Death()
    {
        cameraAnimator.enabled = true;
        cameraAnimator.Play("Deathanim");
        gameover.DieFromMonster();
        GameFuncs.DisableWeapons(true);
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
        menu.ChangeHealth(Health);
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
        if (inElevator)
            return;

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
            //controller.Move(move * speed * Time.fixedDeltaTime);
            //velocity.y = 0;
            //Vector3 elevatorMovement = currentElevator.Velocity * Time.fixedDeltaTime;
            //controller.Move(elevatorMovement);
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
        if (allowMovement)
            controller.Move(move * speed * Time.deltaTime);

        if (inElevator && currentElevator.moving)
        {
            controller.Move(transform.up * currentElevator.speed * Time.deltaTime * Elevator.goingUp);
        }

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

        if (Input.GetKeyDown(KeyCode.G) && menu.inventory.Has(2, out var slot))
        {
            menu.inventory.DeleteItem(slot, 1);
            audio.PlayOneShot(bubble, 0.2f);
        }

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
        

        //HandleInteract();


        RaycastHit hit2;
        Vector3 rayOrigin2 = transform.position + Vector3.up * 0.1f;
        if (Physics.Raycast(rayOrigin2, Vector3.down, out hit2, 1.5f))
        {
            Vector3 velocity = new Vector3(controller.velocity.x, controller.velocity.y, controller.velocity.z);
            float speed = velocity.magnitude;
            Terrain terrain = hit2.collider.GetComponent<Terrain>();
            if (terrain != null)
            {
                footsteps.TryStepTerrain(terrain, speed);
                return;
            }
            // Successfully hit an object
            footsteps.TryStep(hit2.collider.gameObject, speed);
        }
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
            else // Try to use it if player's inside the trigger
            {
                //useTrigger.SetActive(true);
                //useTrigger.transform.position = transform.position;
            }
        }
    }
}
