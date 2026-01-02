using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BossHealer : MonoBehaviour
{

    [SerializeField] BossEye[] bossEyes;
    bool healing = false;
    bool spawnEyes = false;
    float currentEyeTime = 0f;
    float eyeTime = 0.5f;
    int spawnedEyes = 0;
    int maxSpawnedEyes = 15;
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
        if (healing)
            return;
        healing = true;
        spawnEyes = true;
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
        if (!spawnEyes)
            return;

        if (currentEyeTime > eyeTime)
        {
            spawnedEyes++;
            currentEyeTime = 0f;
            bossEyes[Random.Range(0, bossEyes.Length - 1)].OpenEye();
            if (spawnedEyes >= maxSpawnedEyes)
            {
                spawnEyes = false;
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
        //Debug.Log(currentEyes);
        return currentEyes;
    }

    public bool CantHealAnymore()
    {
        if (!healing)
            return false;
        if (!spawnEyes && currentEyes == 0)
        {
            StopHealing();
            return true;
        }
        else
            return false;
    }
}
