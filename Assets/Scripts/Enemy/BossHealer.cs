using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.XR;

public class BossHealer : MonoBehaviour
{

    [SerializeField] BossEye[] bossEyes;
    [SerializeField] Boss boss;
    public List<BossEye> aliveEyes = new List<BossEye>();
    public List<BossEye> closedEyes = new List<BossEye>();
    float currentEyeTime = 0f;
    float eyeTime = 0.6f;
    int spawnedEyes = 0; // Spawned for this session
    int maxSpawnedEyes = 22;
    float max_healingTime = 12f;
    float current_healingTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        foreach (BossEye eye in bossEyes)
        {
            aliveEyes.Add(eye);
            closedEyes.Add(eye);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (boss.bossState == Boss.BossState.Healing)
        {
            current_healingTime += Time.deltaTime;
            currentEyeTime += Time.deltaTime;

            SpawnEyes();
            StopHealCheck();
        }
    }

    public void SpawnEyes()
    {
        if (aliveEyes.Count <= 0)
            return;

        if (currentEyeTime > eyeTime)
        {
            //print("closed: " + closedEyes.Count);
            spawnedEyes++;
            currentEyeTime = 0f;
            OpenRandomEye();
        }
    }

    public void StopHealing()
    {
        boss.bossDoors.OpenDoors();
        boss.bossState = Boss.BossState.Attack;
        current_healingTime = 0f;
        foreach (BossEye eye in aliveEyes)
        {
            eye.HideEye();
        }
        spawnedEyes = 0;

    }

    public int GetCurrentEyes()
    {
        //Debug.Log(currentEyes);
        return aliveEyes.Count;
    }

    public void OpenRandomEye()
    {
        if (closedEyes.Count <= 0)
            return;
        int rng = Random.Range(0, closedEyes.Count);
        closedEyes[rng].OpenEye();
    }

    public bool StopHealCheck()
    {
        if (current_healingTime > max_healingTime || spawnedEyes >= maxSpawnedEyes || aliveEyes.Count <= 0)
        {
            if (current_healingTime > max_healingTime)
                Debug.Log("timeout");

            if (spawnedEyes >= maxSpawnedEyes)
                Debug.Log("max eyes");

            if (boss.Health <= 301f)
                boss.Health = 301f;
            StopHealing();
            return true;
        }

        return false;
    }

    public void ResetHealer()
    {
        aliveEyes.Clear();
        closedEyes.Clear();
        foreach (BossEye eye in bossEyes)
        {
            eye.Opened = false;
            eye.Killed = false;
            aliveEyes.Add(eye);
            closedEyes.Add(eye);
        }
    }
}
