using GM;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] Transform PlayerBody;
    public bool AllowMove = true;
    [SerializeField] MouseLook otherCamera;
    public float preferenceSens = 1f;
    Animator anim;
    [SerializeField] bool allowY = true;
    private float MouseSensitivity = 2f;

    float xRotation = 0f;
    void Start()
    {
        anim = GetComponent<Animator>();
        if (GameFuncs.mouseLook == null)
            GameFuncs.mouseLook = this;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void DisableAnimator()
    {
        anim.enabled = false;
    }

    public void CenterView()
    {
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Y rotation
        xRotation = 0f; // Centering view
    }

    const string xAxis = "Mouse X";
    const string yAxis = "Mouse Y";

    void Update()
    {
        if (!AllowMove || PlayerBody == null)
            return;
        float mouseX = Input.GetAxis(xAxis) * MouseSensitivity * preferenceSens;
        float mouseY = Input.GetAxis(yAxis) * MouseSensitivity * preferenceSens;
        if (allowY)
        {
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Y rotation
        }

        PlayerBody.Rotate(Vector3.up * mouseX); // X rotation
    }
}
