using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prison : MonoBehaviour
{
    public bool isLoaded = false;
    void Awake()
    {
        if (!isLoaded)
        gameObject.SetActive(false);
    }
}
