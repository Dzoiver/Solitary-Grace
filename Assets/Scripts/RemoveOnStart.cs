using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveOnStart : MonoBehaviour
{
    [SerializeField] bool isLight = false;
    [SerializeField] bool removeInStart = false;
    private void Awake()
    {
        if (!removeInStart)
            gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (removeInStart)
            gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateLights()
    {
        Debug.Log(gameObject.name + " " + isLight);
        if (isLight)
            gameObject.SetActive(true);
    }
}
