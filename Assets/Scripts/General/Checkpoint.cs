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
    GetSomeSleep[] sleeps;
    public UnityEvent onTeleport;
    private void Awake()
    {
        sleeps = FindObjectsOfType<GetSomeSleep>();
        foreach (GetSomeSleep sl in sleeps)
        {
            if (sl.gameObject.name == "GetSleep2")
            {
                Debug.Log("found good sleep");
                sleep = sl;
            }
        }
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
        Debug.Log("Updated checkpoint");
        sleep.checkpoint = this;
        oldSpawn.transform.position = newSpawn.transform.position;
        oldSpawn.transform.rotation = newSpawn.transform.rotation;
    }

    public void OnTeleportInvoke()
    {
        onTeleport.Invoke();
    }
}
