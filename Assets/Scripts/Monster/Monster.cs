using UnityEngine;
using GM;
using UnityEngine.AI;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class Monster : MonoBehaviour
{
    [SerializeField] private bool activeAI = true;
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
    [SerializeField] private float playerSearchTime = 5f;
    private float currentPlayerSearchTime = 0f;
    private bool chase = false;
    [SerializeField] LayerMask layerMask;

    [SerializeField] private bool patrol = true;
    private List<Transform> patrolPoints = new List<Transform>();
    [SerializeField] private GameObject patrolParent;
    private int currentPatrolIndex = 0;
    [SerializeField] private float patrolWait = 1f;
    private float currentPatrolWait = 0f;

    private Vector3 startPosition;
    public float allowedAngle = 45f;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
        int i = 0;
        foreach (Transform t in patrolParent.transform)
        {
            i++;
            patrolPoints.Add(t);
        }
        //rb = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!activeAI)
            return;

        if (PlayerTooClose())
        {
            agent.destination = GameFuncs.PlayerScript.transform.position;
            DamagePlayer();
        }
    }

    private void FixedUpdate()
    {
        if (!activeAI)
            return;

        if (ChasingPlayer())
        {
            agent.destination = GameFuncs.PlayerScript.transform.position;
        }
        else
        {
            Patrol();
        }
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

    public void GetDamage(float amount)
    {
        if (health - amount <= 0)
        {
            Death();
        }
        else
        {
            health -= amount;
        }
        Debug.Log("Current health: " + health);
    }

    private void Death()
    {
        gameObject.SetActive(false);
    }

    private bool ChasingPlayer()
    {
        Vector3 directionNormal = (GameFuncs.PlayerScript.gameObject.transform.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, directionNormal, out RaycastHit hit, detectRadius, layerMask))
        {
            Vector3 directionToHit = (hit.point - transform.position).normalized;
            float angleToTarget = Vector3.Angle(transform.forward, directionToHit);
            Debug.Log(angleToTarget);
            if (angleToTarget <= allowedAngle)
            {
                // Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green);

                if (hit.collider.CompareTag("Player"))
                {
                    chase = true;
                    return true;
                }
                // Object is within the allowed angle
                // Debug.Log("Hit object " + hit.collider.name + " within allowed angle.");
            }
        }

        if (chase)
        {
            currentPlayerSearchTime += Time.deltaTime;
            if (currentPlayerSearchTime > playerSearchTime)
            {
                if (!patrol)
                    agent.destination = startPosition;
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
        if (patrolPoints.Count == 0)
            return;

        agent.destination = patrolPoints[currentPatrolIndex].position;
        if (Vector3.Distance(transform.position, patrolPoints[currentPatrolIndex].transform.position) < 1.5f)
        {
            if (currentPatrolWait > patrolWait)
            {
                currentPatrolWait = 0f;
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
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

    public void Activate()
    {
        activeAI = true;
    }
}
