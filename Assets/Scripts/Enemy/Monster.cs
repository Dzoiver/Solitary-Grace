using UnityEngine;
using GM;
using UnityEngine.AI;
using UnityEngine.UIElements;
using System.Collections.Generic;
using static UnityEngine.Rendering.DebugUI;
using System.Collections;
using static UnityEngine.GraphicsBuffer;
using UnityEditor;

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

    private float attackDamage = 35f;
    private float attackDelay = 0.3f;
    private float currentAttackDelay = 0.3f;
    private float attackCoolDown = 2f;
    private float currentAttackCoolDown = 0f;
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
    private bool freeze = false;
    [SerializeField] bool randomizePatrol = false;

    private Vector3 startPosition;
    private Quaternion startRotation;
    public float allowedAngle = 45f;
    private float wakeAttackDelay = 1f;
    private float currentWakeAttackDelay = 0f;
    private AudioSource audio;
    [SerializeField] private GameObject model;
    [SerializeField] private AudioClip attackClip;
    [SerializeField] private AudioClip alarm1Clip;
    [SerializeField] private AudioClip alarm2Clip;
    [SerializeField] private AudioClip alarm3Clip;
    [SerializeField] private AudioClip painClip;
    [SerializeField] private AudioClip pain2Clip;
    [SerializeField] private AudioClip deathClip;
    BoxCollider collider;

    public Color circleColor = Color.red;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = true;
        int i = 0;
        foreach (Transform t in patrolParent.transform)
        {
            i++;
            patrolPoints.Add(t);
        }
        audio = GetComponent<AudioSource>();
        collider = GetComponent<BoxCollider>();
        //rb = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!activeAI)
            return;
        currentAttackCoolDown += Time.deltaTime;
        if (PlayerTooClose())
        {
            Alarm();
            chase = true;
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
            currentWakeAttackDelay = 0f;
            Patrol();
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw the gizmo only when the object is selected
        Gizmos.color = circleColor;

        // Draw a wire sphere; in a 2D context, this looks like a circle
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }

    private bool PlayerTooClose()
    {
        if (Vector3.Distance(transform.position, GameFuncs.PlayerScript.transform.position) < detectRadius / 4)
        {
            Vector3 directionNormal = (GameFuncs.PlayerScript.gameObject.transform.position - transform.position).normalized;
            if (Physics.Raycast(transform.position, directionNormal, out RaycastHit hit, detectRadius, layerMask))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void DamagePlayer()
    {
        currentWakeAttackDelay += Time.deltaTime;

        if (!PlayerClose())
            return;

        if (currentWakeAttackDelay < wakeAttackDelay)
            return;

        if (currentAttackCoolDown >= attackCoolDown) // Ready to strike player
        {
            if (currentAttackDelay >= attackDelay) // Moment damage is done
            {
                audio.PlayOneShot(attackClip);
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
            if (audio.clip == painClip || audio.clip == pain2Clip && audio.isPlaying)
            {

            }
            else
            {
                // Random pain sound
                int rnd = Random.Range(0, 1);
                if (rnd == 0)
                    audio.PlayOneShot(painClip);
                else if (rnd == 1)
                    audio.PlayOneShot(pain2Clip);
                // end
            }

            health -= amount;
        }
    }

    private void Death()
    {
        audio.PlayOneShot(deathClip);
        enabled = false;
        model.SetActive(false);
        collider.enabled = false;
    }

    private bool ChasingPlayer()
    {
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
                    Alarm();
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
        if (Vector3.Distance(transform.position, patrolPoints[currentPatrolIndex].transform.position) <= agent.stoppingDistance)
        {
            if (currentPatrolWait > patrolWait)
            {
                currentPatrolWait = 0f;

                if (randomizePatrol)
                    currentPatrolIndex = Random.Range(0, patrolPoints.Count - 1);
                else
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

    public void SetFreeze(bool value)
    {
        freeze = value;
    }

    public bool GetFreeze()
    {
        return freeze;
    }

    public void Alarm()
    {
        if (chase)
            return;
        chase = true;
        int rng = Random.Range(0, 2);

        if (rng == 0)
            audio.PlayOneShot(alarm1Clip);
        else if (rng == 1)
            audio.PlayOneShot(alarm2Clip);
        else if (rng == 2)
            audio.PlayOneShot(alarm3Clip);
    }
}
