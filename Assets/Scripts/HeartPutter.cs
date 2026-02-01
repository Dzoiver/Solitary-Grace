using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeartPutter : MonoBehaviour
{
    public UnityEvent onPut;
    [SerializeField] GameObject heart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PutHeart()
    {
        StartCoroutine(Waiter());
    }

    IEnumerator Waiter()
    {
        heart.SetActive(true);
        yield return new WaitForSeconds(2f);
        GameFuncs.FadeIn(0.5f);
        yield return new WaitForSeconds(0.5f);
        heart.SetActive(false);
        GameFuncs.FadeOut(0.5f);
        onPut.Invoke();
        // Wait for 4 seconds
        
    }
}
