using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] Transform PlayerBody;
    public bool AllowMove = true;
    [SerializeField] MouseLook otherCamera;

    private float MouseSensitivity = 2f;

    float xRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    const string xAxis = "Mouse X";
    const string yAxis = "Mouse Y";

    void Update()
    {
        if (!AllowMove || PlayerBody == null)
            return;
        float mouseX = Input.GetAxis(xAxis) * MouseSensitivity;
        float mouseY = Input.GetAxis(yAxis) * MouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Y rotation

        PlayerBody.Rotate(Vector3.up * mouseX); // X rotation
    }
}
