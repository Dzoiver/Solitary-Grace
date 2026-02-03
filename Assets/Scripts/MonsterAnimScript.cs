using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimScript : MonoBehaviour
{
    [SerializeField] Monster monster;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        monster.AnimDamage();
    }

    public void Death()
    {
        monster.DisableCollider();
    }

    public void Activate()
    {
        monster.ActiveAI = true;
    }
}
