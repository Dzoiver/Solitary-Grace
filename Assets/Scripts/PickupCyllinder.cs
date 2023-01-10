using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCyllinder : MonoBehaviour
{
    Vector3 vect = new Vector3(0, 0, 1);
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        transform.Rotate(vect);
    }
}
