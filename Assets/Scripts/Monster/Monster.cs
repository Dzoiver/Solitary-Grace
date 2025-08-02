using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;

public class Monster : MonoBehaviour
{
    private bool seePlayer = false;
    private float seeDistance = 10f;
    private float chaseSpeed = 2f;
    private float stopDistance = 2f;
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }

    private bool PlayerDetected()
    {
        if (Vector3.Distance(transform.position, GameFuncs.PlayerScript.transform.position) < seeDistance)
        {
            return true;
        }
        
        return false;
    }


    private bool PlayerClose()
    {
        if (Vector3.Distance(transform.position, GameFuncs.PlayerScript.transform.position) < stopDistance)
        {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerDetected())
        {
            if (!PlayerClose())
            gameObject.transform.position = Vector3.MoveTowards(transform.position, GameFuncs.PlayerScript.transform.position, chaseSpeed * Time.deltaTime);
        }
    }
}
