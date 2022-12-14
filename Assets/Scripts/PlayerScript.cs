using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] GameObject useTrigger;
    [SerializeField] GameObject cyllinder;
    [SerializeField] GameObject warpTool;
    [SerializeField] CharacterController controller;
    [SerializeField] LayerMask layer;
    [SerializeField] MouseLook mouse;

    // float maxUseDistance = 1.5f;
    public float speed = 1f;
    public float gravity = -1f;
    public float jumpHeight = 3f;
    public float groundDistance = 0.1f;

    public bool allowJump = false;
    private bool allowMovement = true;
    private bool isGrounded;

    public CharacterController cControl;
    public Transform groundCheck;
    public LayerMask groundMask;

    Vector3 velocity;

    private void Start()
    {
        GameFuncs.PlayerScript = gameObject.GetComponent<PlayerScript>();
    }
    // Start is called before the first frame update
    public void Warping(bool value)
    {
        if (value == true)
            warpTool.SetActive(true);
        else
            warpTool.SetActive(false);
    }

    public void SetControl(bool allow)
    {
        if (allow)
        {
            allowMovement = true;
            mouse.AllowMove = true;
        }
        else
        {
            allowMovement = false;
            mouse.AllowMove = false;
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
            Debug.Log("Touched JumpPad");

        }
    }

    public void LiftPlayer(float amount)
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
