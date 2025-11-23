using GM;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] GameObject oldSpawn;
    [SerializeField] GameObject newSpawn;
    GetSomeSleep sleep;
    public UnityEvent onTeleport;
    // Start is called before the first frame update
    void Start()
    {
        sleep = FindObjectOfType<GetSomeSleep>();
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
        sleep.checkpoint = this;
        oldSpawn.transform.position = newSpawn.transform.position;
        oldSpawn.transform.rotation = newSpawn.transform.rotation;
    }

    public void OnTeleportInvoke()
    {
        onTeleport.Invoke();
    }
}
