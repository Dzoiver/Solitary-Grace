using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BossHealer : MonoBehaviour
{

    [SerializeField] BossEye[] bossEyes;
    bool healing = false;
    float currentEyeTime = 0f;
    float eyeTime = 0.5f;
    int spawnedEyes = 0;
    int maxSpawnedEyes = 10;
    int currentEyes;
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
            PushEyes();
        }
    }

    public void StartHealing()
    {
        healing = true;
    }

    public void StopHealing()
    {
        healing = false;
        foreach (BossEye eye in bossEyes)
        {
            eye.CloseEye();
        }
    }

    public void PushEyes()
    {
        if (currentEyeTime > eyeTime)
        {
            spawnedEyes++;
            currentEyeTime = 0f;
            bossEyes[Random.Range(0, bossEyes.Length - 1)].OpenEye();
            if (spawnedEyes > maxSpawnedEyes)
            {
                healing = false;
            }
        }
    }

    public void AddEye()
    {
        currentEyes++;
    }

    public void DeleteEye()
    {
        currentEyes--;
    }

    public int GetCurrentEyes()
    {
        return currentEyes;
    }
}
