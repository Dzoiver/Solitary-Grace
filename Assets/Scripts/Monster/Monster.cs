using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;
using UnityEngine.AI;
using UnityEditor.UIElements;
using System;
using UnityEditor;

public class Monster : MonoBehaviour
{
    private NavMeshAgent agent;
    private bool seePlayer = false;
    [SerializeField] private float detectRadius = 10f;
    private float chaseSpeed = 2f;
    private float stopDistance = 2f;

    private float health = 100f;
    private float maxHealth = 100f;

    private float attackDamage = 50f;
    private float attackDelay = 0.3f;
    private float currentAttackDelay = 0.3f;
    private float attackCoolDown = 2f;
    private float currentAttackCoolDown = 2f;
    private float playerSearchTime = 5f;
    private float currentPlayerSearchTime = 0f;
    private bool chase = false;
    [SerializeField] LayerMask layerMask;

    [SerializeField] private bool patrol = true;
    [SerializeField] private GameObject[] patrolPoints;
    private int currentPatrolIndex = 0;
    [SerializeField] private float patrolWait = 1f;
    private float currentPatrolWait = 0f;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //rb = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
    }

    private bool PlayerTooClose()
    {
        if (Vector3.Distance(transform.position, GameFuncs.PlayerScript.transform.position) < detectRadius / 4)
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

    private bool ChasingPlayer()
    {
        Vector3 directionNormal = (GameFuncs.PlayerScript.gameObject.transform.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, directionNormal, out RaycastHit hit, detectRadius, layerMask))
        {
            if (hit.collider.CompareTag("Player"))
            {
                chase = true;
                return true;
            }
        }

        if (chase)
        {
            currentPlayerSearchTime += Time.deltaTime;
            if (currentPlayerSearchTime > playerSearchTime)
            {
                chase = false;
                currentPlayerSearchTime = 0f;
            }
        }
        return chase;
    }

    private void Patrol()
    {
        if (!patrol)
            return;

        agent.destination = patrolPoints[currentPatrolIndex].transform.position;
        if (Vector3.Distance(transform.position, patrolPoints[currentPatrolIndex].transform.position) < 1.5f)
        {
            if (currentPatrolWait > patrolWait)
            {
                currentPatrolWait = 0f;
                if (currentPatrolIndex + 1 < patrolPoints.Length)
                {
                    currentPatrolIndex++;
                }
                else
                {
                    currentPatrolIndex = 0;
                }
            }
            currentPatrolWait += Time.deltaTime;
        }
    }
    

    private bool PlayerClose()
    {
        if (Vector3.Distance(transform.position, GameFuncs.PlayerScript.transform.position) < stopDistance
            && !GameFuncs.PlayerScript.IsDead())
        {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerTooClose())
        {
            agent.destination = GameFuncs.PlayerScript.transform.position;
            DamagePlayer();
        }
    }

    private void FixedUpdate()
    {
        if (ChasingPlayer())
        {
            agent.destination = GameFuncs.PlayerScript.transform.position;
        }
        else
        {
            Patrol();
        }
    }
}
