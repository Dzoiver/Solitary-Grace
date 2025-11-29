using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleEvent : MonoBehaviour
{
    [SerializeField] string description;
    [SerializeField] UnityEvent onEvent;
    [SerializeField] UnityEvent onEvent2;
    [SerializeField] float delay = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartEvent()
    {
        StartCoroutine(WaitAndStart());
    }

    public void StartEvent2()
    {
        onEvent2.Invoke();
    }

    private IEnumerator WaitAndStart()
    {
        yield return new WaitForSeconds(delay);
        onEvent.Invoke();
        
    }
}
