using UnityEngine;
using GM;
using UnityEngine.Rendering.UI;
using Zenject;
using Unity.VisualScripting;

public class PlayerScript : MonoBehaviour
{
    [Inject] GameOver gameover;
    [SerializeField] GameObject useTrigger;
    [SerializeField] GameObject cyllinder;
    [SerializeField] GameObject warpTool;
    [SerializeField] LayerMask layer;
    [SerializeField] Transform GroundCheck;
    [SerializeField] MouseLook _mouse;

    private float speed = 5f;
    private float gravity = 1f;
    private float jumpHeight = 3f;
    private float groundDistance = 0.5f;
    [SerializeField] private bool allowMovement = true;
    [SerializeField] private bool allowControl = true;
    private bool isGrounded;
    private Vector3 velocity;
    private bool isNoclip = false;

    public bool AllowJump = false;
    [HideInInspector] public CharacterController controller;
    [SerializeField] GameObject playerCam;
    Vector3 playerCamStartPos;
    public LayerMask GroundMask;

    private float health = 100f;
    private float maxHealth = 100f;

    public void GetDamage(float damage)
    {
        if (health - damage <= 0 && !IsDead())
        {
            health = 0;
            Death();
        }
        else if (!IsDead())
        {
            health -= damage;
            gameover.GetDamagedRedScreen();
            // redish screen
        }
    }

    public bool IsDead()
    {
        if (health <= 0)
        {
            return true;
        }
        return false;
    }

    private void Death()
    {
        // Red screen
        // camera animation
        // Black screen
        // Respawn
        Animator anim = playerCam.GetComponent<Animator>();
        anim.enabled = true;
        anim.Play("Deathanim");
        gameover.DieFromMonster();
    }

    public void CameraRestore()
    {
        playerCam.transform.localPosition = playerCamStartPos;
        playerCam.GetComponent<CameraReturnControls>().SwitchToPlayer(true);
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

    public Transform GetTransform()
    {
        return gameObject.transform;
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
    private void Awake()
    {
        
    }

    private void Start()
    {
        GameFuncs.PlayerScript = gameObject.GetComponent<PlayerScript>();
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
        if (isNoclip == false)
        {
            isNoclip = true;
            gameObject.layer = 12; // PlayerNoclip
            Debug.Log("Noclip on");
        }
        else
        {
            isNoclip = false;
            gameObject.layer = 6; // Player
            Debug.Log("Noclip off");
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, GroundMask);
    }

    private void Update()
    {
        if (isNoclip)
        {
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

        

        if (isGrounded && velocity.y < 0 && !isNoclip) // Gravity even when grounded
        {
            velocity.y = -6f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        Vector3 move = transform.right * x + transform.forward * z;

        if (allowMovement)
            controller.Move(move * speed * Time.deltaTime);

        if (!allowControl)
            return;

        if (Input.GetButtonDown("Jump") && isGrounded && AllowJump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
        }

        if (!isNoclip)
        {
            velocity.y += Physics.gravity.y * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);

        HandleInteract();
    }

    private void HandleInteract()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Debug.DrawRay(ray.origin, ray.direction, Color.white, 5f);

            if (Physics.Raycast(ray, out hit, 1.5f, layer.value))
            {
                if (hit.collider.gameObject.layer == 3)
                {
                    useTrigger.SetActive(true);
                    useTrigger.transform.position = hit.point;
                }
            }
        }
    }
}
