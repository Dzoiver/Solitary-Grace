using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class GameStatistics : MonoBehaviour
{
    float gameTime = 0;
    int deaths = 0;

    public float GameTime { get => gameTime; }
    public int Deaths {
        get => deaths;
        set
        {
            if (value >= 0)
                deaths = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
    }
}
