using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    Monster[] monsters;
    Monster[] monstersFreezed;
    private void Awake()
    {
        monsters = FindObjectsOfType<Monster>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FreezeMonsters()
    {
        foreach (Monster monster in monsters)
        {
            if (monster.gameObject.activeSelf)
            {
                monster.SetFreeze(true);
                monster.gameObject.SetActive(false);
            }
        }
    }

    public void UnfreezeMonsters()
    {
        foreach (Monster monster in monsters)
        {
            if (monster.GetFreeze())
            {
                monster.SetFreeze(false);
                monster.gameObject.SetActive(true);
            }
        }
    }
}
