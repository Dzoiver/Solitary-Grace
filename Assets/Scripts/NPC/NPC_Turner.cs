using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Turner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(GameFuncs.PlayerScript.transform.position, transform.position) > 6f)
        {
            return;
        }

        Vector3 direction = transform.position - GameFuncs.PlayerScript.transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.deltaTime);
        }
    }
}
