using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BossHealer : MonoBehaviour
{

    [SerializeField] GameObject[] bossEyes;
    bool healing = false;
    float currentEyeTime = 0f;
    float eyeTime = 500f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (healing)
        {
            currentEyeTime += Time.deltaTime;
            
        }
    }

    public void StartHealing()
    {

    }

    public void StopHealing()
    {

    }

    public void PushEyes()
    {
        if (currentEyeTime > eyeTime)
        {
            // bossEyes[Random.Range(0, bossEyes.Length - 1)]
        }
    }
}
