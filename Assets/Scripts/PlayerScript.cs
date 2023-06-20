using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;
using Zenject;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] GameObject useTrigger;
    [SerializeField] GameObject cyllinder;
    [SerializeField] GameObject warpTool;
    [SerializeField] LayerMask layer;

    [Inject] private MouseLook _mouse;
    private CharacterController controller;

    private float speed = 5f;
    private float gravity = -1f;
    private float jumpHeight = 3f;
    private float groundDistance = 0.1f;

    private bool allowMovement = true;
    private bool allowControl = true;
    private bool isGrounded;

    private Vector3 velocity;

    public bool allowJump = false;
    public CharacterController cControl;
    public Transform groundCheck;
    public LayerMask groundMask;


    private void Start()
    {
        GameFuncs.PlayerScript = gameObject.GetComponent<PlayerScript>();
        controller = GetComponent<CharacterController>();
    }

    public void Warping(bool value)
    {
        if (value == true)
            warpTool.SetActive(true);
        else
            warpTool.SetActive(false);
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
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (allowMovement)
        controller.Move(move * speed * Time.deltaTime);

        if (!allowControl)
            return;

        if (Input.GetButtonDown("Jump") && isGrounded && allowJump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction, Color.white, 5f);

            if (Physics.Raycast(ray, out hit, 1.5f, layer.value))
            {
                useTrigger.SetActive(true);
                useTrigger.transform.position = hit.point;
            }
        }
    }
}
