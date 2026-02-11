using GM;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BossBehindSpawner : MonoBehaviour
{
    Transform[] spawnPoints;
    [SerializeField] Boss boss;
    [SerializeField] UnityEvent onSpawn;
    Ray ray;
    float currentTrySpawn = 0f;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GetComponentsInChildren<Transform>(true);
    }

    // Update is called once per frame
    void Update()
    {
        currentTrySpawn += Time.deltaTime;
        FindPlaceToSpawn();

        ActivateBossAtView();
    }

    private void ActivateBossAtView()
    {
        if (boss.enabled)
            return;

        if (boss.gameObject.activeSelf)
        {
            ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            Vector3 cameraPosition = Camera.main.transform.position;
            Vector3 lookDirection = ray.direction.normalized;

            Vector3 bossDirection = boss.transform.position - cameraPosition;

            float angleToTarget = Vector3.Angle(lookDirection, bossDirection.normalized);
            if (angleToTarget < 55f)
            {
                onSpawn.Invoke();
                boss.enabled = true;
                enabled = false;
            }
        }
    }

    public void FindPlaceToSpawn()
    {
        if (currentTrySpawn < 0.5f || boss.gameObject.activeSelf)
            return;
        Vector3 guessSpawn;
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 lookDirection = ray.direction.normalized;
        

        foreach (Transform child in transform)
        {
            Vector3 toSpawnPoint = child.position - cameraPosition;

            guessSpawn = child.transform.position;
            //Debug.Log(child.name);
            //Vector3 spawnPointDirection = (hit.point - guessSpawn).normalized;
            float angleToTarget = Vector3.Angle(lookDirection, toSpawnPoint.normalized);
            if (angleToTarget > 65 && Vector3.Distance(GameFuncs.PlayerScript.transform.position, guessSpawn) > 4f)
            {
                boss.transform.position = guessSpawn;
                boss.gameObject.SetActive(true);
                boss.audio.PlayOneShot(boss.spawnClip, 0.2f);
                //boss.enabled = true;
                break;
            }
        }
        currentTrySpawn = 0f;
        //
        //
    }
}
