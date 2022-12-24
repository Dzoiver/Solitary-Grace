using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCyllinder : MonoBehaviour
{
    Vector3 vect = new Vector3(0, 0, 1);
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(vect);
    }
}
