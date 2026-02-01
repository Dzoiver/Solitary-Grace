using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillingGuardClear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clear()
    {
        StartCoroutine(Waiter());
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }
}
