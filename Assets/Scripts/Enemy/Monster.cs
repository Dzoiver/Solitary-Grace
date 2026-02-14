using UnityEngine;
using GM;
using UnityEngine.AI;
using UnityEngine.UIElements;
using System.Collections.Generic;
using static UnityEngine.Rendering.DebugUI;
using System.Collections;
using static UnityEngine.GraphicsBuffer;
using UnityEditor;
using UnityEngine.Events;

public class Monster : MonoBehaviour
{
    [SerializeField] private bool activeAI = true;
    private NavMeshAgent agent;
    [SerializeField] private float detectRadius = 10f;
    private float chaseSpeed = 2f;

    private float health = 100f;
    private float maxHealth = 100f;

    private float minDamage = 25f;
    private float maxDamage = 40f;
    private float attackDelay = 0.3f;
    private float currentAttackDelay = 0.15f;
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
    CapsuleCollider collider;
    Rigidbody rb;
    [SerializeField] FootSteps footsteps;

    [SerializeField] Animator animator;

    [SerializeField] private bool pretending = false;
    public UnityEvent onKill;
    [SerializeField] bool hospitalSleep = false;

    public Color circleColor = Color.red;

    public bool Freeze { get => freeze; set => freeze = value; }

    public bool IsDead => health <= 0f;

    public bool Pretending { get => pretending; set
        {
            pretending = value;

            if (pretending)
            {
                animator.SetBool("Pretending", true);
                //ActiveAI = false;
            } 
            else
            {
                animator.SetBool("Pretending", false);
            }
        }
    }

    public bool ActiveAI { get => activeAI; set => activeAI = value; }
    public bool Patrol1 { get => patrol; set => patrol = value; }

    // Start is called before the first frame update
    void Start()
    {
        if (hospitalSleep)
            animator.SetBool("Stretcher", true);
        agent = GetComponent<NavMeshAgent>();
        int i = 0;
        foreach (Transform t in patrolParent.transform)
        {
            i++;
            patrolPoints.Add(t);
        }
        audio = GetComponent<AudioSource>();
        collider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();

        if (pretending)
            Pretending = true;
    }

    private void Awake()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void OnEnable()
    {
        if (IsDead)
        {
            animator.Play("Death", 0, 1);
            return;
        }
        else if (pretending)
        {
            Pretending = true;
        }
    }

    void Update()
    {
        if (!ActiveAI || IsDead)
            return;
        currentAttackCoolDown += Time.deltaTime;
        if (PlayerTooClose())
        {
            agent.updateRotation = false;
            Vector3 direction = GameFuncs.PlayerScript.transform.position - transform.position;
            direction.y = 0;
            if (direction != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.fixedDeltaTime);
            }

            Pretending = false;
            Alarm();
            chase = true;
            agent.destination = GameFuncs.PlayerScript.transform.position;
            DamagePlayer();
        }
        else
            agent.updateRotation = true;
        animator.SetFloat("Velocity", agent.velocity.magnitude);


        // FootSteps
        RaycastHit hit2;
        Vector3 rayOrigin2 = transform.position + Vector3.up * 0.1f;
        if (Physics.Raycast(rayOrigin2, Vector3.down, out hit2, 2f))
        {
            Terrain terrain = hit2.collider.GetComponent<Terrain>();
            if (terrain != null)
            {
                //Debug.Log(agent.velocity.magnitude);
                footsteps.TryStepTerrain(terrain, agent.velocity.magnitude);
                return;
            }
            // Successfully hit an object
            footsteps.TryStep(hit2.collider.gameObject, agent.velocity.magnitude);
        }
    }

    private void FixedUpdate()
    {
        if (!ActiveAI || IsDead)
            return;

        AnimatorStateInfo animInfo = animator.GetCurrentAnimatorStateInfo(0);
        

        if (animInfo.IsName("Attack") && animInfo.normalizedTime < 0.5f)
        {
            agent.isStopped = true;
            return;
        }

        if (ChasingPlayer())
        {
            agent.updateRotation = true;
            agent.isStopped = false;
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
        Gizmos.color = circleColor;
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
        if (IsDead)
            return;
        currentWakeAttackDelay += Time.deltaTime;
        if (!PlayerClose())
        {
            animator.SetBool("Attack", false);
            return;
        }
        
        if (currentWakeAttackDelay < wakeAttackDelay)
        {
            return;
        }

        if (currentAttackCoolDown >= attackCoolDown) // Ready to strike player
        {
            animator.SetBool("Attack", true);
            agent.ResetPath();

            if (currentAttackDelay >= attackDelay) // Moment damage is done
            {
                //audio.PlayOneShot(attackClip);
                //GameFuncs.PlayerScript.GetDamage(attackDamage);
            }

            currentAttackDelay += Time.deltaTime;
        }
    }

    public void AnimDamage()
    {
        if (IsDead)
            return;
        audio.PlayOneShot(attackClip);
        if (Vector3.Distance(transform.position, GameFuncs.PlayerScript.transform.position) < agent.stoppingDistance)
        {
            GameFuncs.PlayerScript.Health -= Random.Range(minDamage, maxDamage);
        }
        animator.SetBool("Attack", false);
        currentAttackDelay = 0f;
        currentAttackCoolDown = 0f;
    }

    public void GetDamage(float amount)
    {
        if (animator.GetBool("Dead"))
        {
            return;
        }
        Pretending = false;
        chase = true;

        health -= amount;
        if (health <= 0f)
        {
            health = 0f;
            Death();
        }
        else
        {
            if (false)
            {
                
            }
            else
            {
                // Random pain sound
                int rnd = Random.Range(0, 2);
                if (rnd == 0)
                    audio.PlayOneShot(painClip);
                else if (rnd == 1)
                    audio.PlayOneShot(pain2Clip);
                // end
            }
        }
    }

    private void Death()
    {
        onKill.Invoke();
        agent.isStopped = true;
        agent.enabled = false;
        animator.SetBool("Dead", true);
        audio.PlayOneShot(deathClip);
    }

    public void DisableCollider()
    {
        collider.enabled = false;
    }

    private bool ChasingPlayer()
    {
        if (Pretending)
            return false;
        Vector3 directionToPlayer = (GameFuncs.PlayerScript.gameObject.transform.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, detectRadius, layerMask))
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
                if (!Patrol1)
                    agent.destination = startPosition;
                chase = false;
                currentPlayerSearchTime = 0f;
            }
        }
        return chase;
    }

    private void Patrol()
    {
        if (!Patrol1)
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
                    currentPatrolIndex = Random.Range(0, patrolPoints.Count);
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
        ActiveAI = true;
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

    public void Alarm()
    {
        if (chase || Pretending)
            return;
        chase = true;
        int rng = Random.Range(0, 3);

        if (rng == 0)
            audio.PlayOneShot(alarm1Clip);
        else if (rng == 1)
            audio.PlayOneShot(alarm2Clip);
        else if (rng == 2)
            audio.PlayOneShot(alarm3Clip);
    }
}
