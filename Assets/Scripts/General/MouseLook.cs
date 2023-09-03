using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] Transform PlayerBody;
    public bool AllowMove = true;

    private float MouseSensitivity = 200f;

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
        float mouseX = Input.GetAxis(xAxis) * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(yAxis) * MouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        PlayerBody.Rotate(Vector3.up * mouseX);
    }
}
