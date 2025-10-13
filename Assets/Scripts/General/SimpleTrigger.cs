using GM;
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
    public bool active = true;
    BoxCollider collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
        if (disableRendering)
            GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!active)
            return;

        if (other.CompareTag("Player"))
        {
            onEnter.Invoke();
            if (triggerOnce)
            {
                collider.enabled = false;
            }
        }

        if (other.gameObject.name == "UseCube")
        {
            onPress.Invoke();
            if (triggerOnce)
            {
                collider.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActiveTrigger() => active = true;

    public void DisablePlayer() => GameFuncs.PlayerScript.SetControl(false);
    IEnumerator TestCoroutine()
    {
        while (true)
        {
            yield return null;
            Debug.Log(Time.deltaTime);
        }
    }
}
