using GM;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] Transform PlayerBody;
    public bool AllowMove = true;
    [SerializeField] MouseLook otherCamera;
    public float preferenceSens = 1f;

    private float MouseSensitivity = 2f;

    float xRotation = 0f;
    void Start()
    {
        GameFuncs.mouseLook = this;
        Cursor.lockState = CursorLockMode.Locked;
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
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Y rotation

        PlayerBody.Rotate(Vector3.up * mouseX); // X rotation
    }
}
