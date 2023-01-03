using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Lever : MonoBehaviour
{
    [SerializeField] GameObject wall;
    [SerializeField] GameObject endValueObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        wall.transform.DOMove(endValueObject.transform.position, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
