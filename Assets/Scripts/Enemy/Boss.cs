using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    [SerializeField] private bool activeAI = true;
    private NavMeshAgent agent;
    private bool seePlayer = false;
    [SerializeField] private float detectRadius = 10f;

    private float health = 600f;
    private float maxHealth = 1000f;

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
    [SerializeField] Transform healSpot;
    bool healing = false;
    bool canKill = false;
    bool Phase1Complete = false;

    [SerializeField] BossDoorsController bossDoors;
    [SerializeField] BossHealer bossHealer;
    float healSpeed = 10f;
    public UnityEvent onKill;

    private Vector3 startPosition;
    private Quaternion startRotation;
    public float allowedAngle = 45f;
    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation;
        agent = GetComponent<NavMeshAgent>();
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
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!activeAI)
            return;

        if (PlayerTooClose() && !healing)
        {
            agent.destination = GameFuncs.PlayerScript.transform.position;
            DamagePlayer();
        }

        if (health < 300 && !Phase1Complete) // When low, goes to healing spot
        {
            healing = true;
            agent.destination = healSpot.position;
            bossDoors.OpenDoors();
        }

        if (BossReachedHeal() && healing) // Boss has walked all the way to the heal spot
        {
            canKill = true;
            bossDoors.CloseDoors();
            bossHealer.StartHealing();
        }

        if (healing && health >= 800) // Heal until 800 HP is restored
        {
            StopHealilng();
        }

        if (healing) // Increase boss health for each eye
        {
            //Debug.Log(health);
            //Debug.Log(bossHealer.GetCurrentEyes() * healSpeed * Time.deltaTime);
            health += bossHealer.GetCurrentEyes() * healSpeed * Time.deltaTime;
        }

        if (bossHealer.CantHealAnymore())
        {
            StopHealilng();
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

    private void StopHealilng()
    {
        print("fuck this i stop heal");
        bossHealer.StopHealing();
        Phase1Complete = true;
        healing = false;
        bossDoors.ResetDoors();
        agent.destination = GameFuncs.PlayerScript.transform.position;
    }

    private bool BossReachedHeal()
    {
        if (Vector3.Distance(transform.position, healSpot.position) <= agent.stoppingDistance + 0.15f)
        {
            return true;
        }
        else
            return false;
    }

    private bool PlayerTooClose()
    {
        if (healing)
            return false;

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
        chase = true;
        if (health - amount <= 0)
        {
            Death();
        }
        else
        {
            health -= amount;
        }
    }

    private void Death()
    {
        if (!canKill)
            return;
        onKill.Invoke();
        gameObject.SetActive(false);
    }

    private bool ChasingPlayer()
    {
        if (healing)
            return false;
        Vector3 directionNormal = (GameFuncs.PlayerScript.gameObject.transform.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, directionNormal, out RaycastHit hit, detectRadius, layerMask))
        {
            Vector3 directionToHit = (hit.point - transform.position).normalized;
            float angleToTarget = Vector3.Angle(transform.forward, directionToHit);
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
        if (Vector3.Distance(transform.position, patrolPoints[currentPatrolIndex].transform.position) < agent.stoppingDistance)
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
        if (Vector3.Distance(transform.position, GameFuncs.PlayerScript.transform.position) < agent.stoppingDistance
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

    public void ResetMonster()
    {
        if (!gameObject.activeInHierarchy)
            return;
        currentPatrolIndex = 0;
        agent.destination = startPosition;
        transform.position = startPosition;
        transform.rotation = startRotation;
        chase = false;
    }
}
