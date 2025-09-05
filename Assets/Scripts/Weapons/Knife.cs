using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private bool equipped = false;
    [SerializeField] private float damage = 5f;
    [SerializeField] private float attackDelay = 0.3f;
    private float currentAttackDelay = 0f;
    [SerializeField] private GameObject hitbox;

    public float GetDamageValue()
    {
        return damage;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Hit()
    {
        if (currentAttackDelay > attackDelay)
        {
            hitbox.SetActive(true);
            currentAttackDelay = 0f;
        }
    }


    // Update is called once per frame
    void Update()
    {
        currentAttackDelay += Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.Mouse0) && equipped)
        {
            Hit();
        }
    }
}
