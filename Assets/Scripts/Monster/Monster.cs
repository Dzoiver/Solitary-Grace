using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;
using UnityEngine.AI;
using UnityEditor.UIElements;

public class Monster : MonoBehaviour
{
    private NavMeshAgent agent;
    private bool seePlayer = false;
    private float seeDistance = 10f;
    private float chaseSpeed = 2f;
    private float stopDistance = 2f;

    private float health = 100f;
    private float maxHealth = 100f;

    private float attackDamage = 50f;
    private float attackDelay = 0.3f;
    private float currentAttackDelay = 0.3f;
    private float attackCoolDown = 2f;
    private float currentAttackCoolDown = 2f;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //rb = GetComponent<Rigidbody>();
    }

    private bool PlayerDetected()
    {
        if (Vector3.Distance(transform.position, GameFuncs.PlayerScript.transform.position) < seeDistance)
        {
            return true;
        }
        
        return false;
    }

    private void DamagePlayer()
    {
        currentAttackCoolDown += Time.deltaTime;

        if (!PlayerClose())
            return;

        if (currentAttackCoolDown >= attackCoolDown) // Ready to strike player
        {
            if (currentAttackDelay >= attackDelay) // Moment damage is done
            {
                GameFuncs.PlayerScript.GetDamage(attackDamage);
                currentAttackDelay = 0f;
                currentAttackCoolDown = 0f;
            }

            currentAttackDelay += Time.deltaTime;
        }
    }


    private bool PlayerClose()
    {
        if (Vector3.Distance(transform.position, GameFuncs.PlayerScript.transform.position) < stopDistance)
        {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerDetected())
        {
            agent.destination = GameFuncs.PlayerScript.transform.position;
        }

        DamagePlayer();
    }
}
