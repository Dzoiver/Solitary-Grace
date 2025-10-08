using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleTrigger : MonoBehaviour
{
    [SerializeField] float delayBetweenEvents = 0f;
    [SerializeField] UnityEvent onEnter;
    [SerializeField] UnityEvent onPress;
    [SerializeField] private bool triggerOnce = false;
    [SerializeField] bool disableRendering = true;
    // Start is called before the first frame update
    void Start()
    {
        if (disableRendering)
            GetComponent<MeshRenderer>().enabled = false;
    }

    IEnumerator TestCoroutine()
    {
        while (true)
        {
            yield return null;
            Debug.Log(Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onEnter.Invoke();
            if (triggerOnce)
            {
                gameObject.SetActive(false);
            }
        }

        if (other.gameObject.name == "UseCube")
        {
            onPress.Invoke();
            if (triggerOnce)
            {
                gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
