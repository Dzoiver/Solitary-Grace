using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    float currentTime = 0f;
    float flyTimeMax = 60f;
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        transform.Translate(5f * Time.deltaTime, 0, 0);
        if (currentTime > flyTimeMax)
        {
            Respawn();
            currentTime = 0f;
        }
    }

    public void Respawn()
    {
        transform.position = startPos;
    }
}
