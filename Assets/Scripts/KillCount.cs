using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KillCount : MonoBehaviour
{
    int counter = 0;
    [SerializeField] Monster[] monsters;
    [SerializeField] UnityEvent onKillAll;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Monster m in monsters)
        {
            m.onKill.AddListener(EnemyKilled);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnemyKilled()
    {
        counter++;
        if (counter >= monsters.Length)
        {
            onKillAll.Invoke();
            enabled = false;
        }
    }
}
