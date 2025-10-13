using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Camera cameraBeginning;
    [SerializeField] Camera cameraPrison;
    enum CameraStage
    {
        Beginning,
        Prison,
    }
    [SerializeField] CameraStage camera;
    private Vector3 movement;
    private float speed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        if (camera == CameraStage.Beginning)
        {
            cameraBeginning.gameObject.SetActive(true);
        }
        if (camera == CameraStage.Prison)
        {
            speed = 0.1f;
            cameraPrison.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (camera == CameraStage.Beginning)
        {
            movement = cameraBeginning.transform.position;
            movement.x += Time.deltaTime * speed;
            movement.z += Time.deltaTime * speed;
            cameraBeginning.transform.position = movement;
        }
        if (camera == CameraStage.Prison)
        {
            movement = cameraPrison.transform.position;
            movement.y -= Time.deltaTime * speed;
            cameraPrison.transform.position = movement;
        }
    }
}
