using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 movement;
    private float speed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        movement = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x += Time.deltaTime * speed;
        movement.z += Time.deltaTime * speed;
        transform.position = movement;
    }
}
