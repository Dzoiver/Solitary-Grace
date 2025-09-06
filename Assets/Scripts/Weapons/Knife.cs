using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] private float damage = 35f;
    [SerializeField] private float attackDelay = 0.3f;
    private float currentAttackDelay = 0f;
    [SerializeField] private KnifeHitbox hitbox;

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
            hitbox.gameObject.SetActive(true);
            currentAttackDelay = 0f;
        }
    }


    // Update is called once per frame
    void Update()
    {
        currentAttackDelay += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Hit();
        }
    }
}
