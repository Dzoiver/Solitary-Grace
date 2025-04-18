using UnityEngine;
using GM;

public class PlayerScript : MonoBehaviour
{
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
    private bool allowMovement = true;
    private bool allowControl = true;
    private bool isGrounded;
    private Vector3 velocity;

    public bool AllowJump = false;
    [HideInInspector] public CharacterController controller;
    public LayerMask GroundMask;

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
        GameFuncs.PlayerScript = gameObject.GetComponent<PlayerScript>();
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
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

    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, GroundMask);
    }

    private void Update()
    {
        if (isGrounded && velocity.y < 0)
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
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
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
