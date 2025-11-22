using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] GameObject oldSpawn;
    [SerializeField] GameObject newSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSpawn()
    {
        oldSpawn.transform.position = newSpawn.transform.position;
        oldSpawn.transform.rotation = newSpawn.transform.rotation;
    }
}
