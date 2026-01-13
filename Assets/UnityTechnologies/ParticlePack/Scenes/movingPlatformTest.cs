using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatformTest : MonoBehaviour
{
    Vector3 vect = new Vector3(0f, 2f, 0f);
    float speed = 4f;
    Rigidbody rb;
    static public bool moving = false;
    float currentTime = 0f;
    float timetolift = 15f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        moving = false;
    }

    private void OnEnable()
    {
        moving = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
        //rb.MovePosition(transform.position + transform.up * Time.deltaTime * speed);
    }
}
