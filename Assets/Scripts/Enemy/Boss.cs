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
    [SerializeField] private float detectRadius = 10f;

    public float health = 600f;
    private float maxHealth = 1000f;

    public float attackDamage = 40f;
    private float attackDelay = 0.3f;
    private float currentAttackDelay = 0.3f;
    private float attackCoolDown = 2f;
    private float currentAttackCoolDown = 2f;
    [SerializeField] private float playerSearchTime = 5f;
    private float currentPlayerSearchTime = 0f;
    private bool chase = false;
    [SerializeField] LayerMask layerMask;
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
    public AudioSource audio;
    [SerializeField] AudioClip deathClip;
    [SerializeField] AudioClip hurtClip;
    [SerializeField] AudioClip stepClip;
    public AudioClip spawnClip;

    Animator animator;
    const float ATTACK_RANGE = 2.9f;

    const float RANGE_ATTACK_TIME = 0f;
    float currentRangeAttackTime = 10f;

    public UnityEvent onAggro;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        startRotation = transform.rotation;
        int i = 0;
        
        //rb = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        audio = GetComponent<AudioSource>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!activeAI)
            return;

        if (PlayerClose() && !healing)
        {

        }
        if (health < 300 && !Phase1Complete) // When low, goes to healing spot
        {
            healing = true;
            agent.destination = healSpot.position;
            bossDoors.OpenDoors();
        }

        if (BossReachedHeal() && healing) // Boss has walked all the way to the heal spot
        {
            bossDoors.CloseDoors();
            bossHealer.StartHealing();
        }

        if (healing && health >= 800) // Heal until 800 HP is restored
        {
            canKill = true;
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
            canKill = true;
            StopHealilng();
        }
        animator.SetFloat("Speed", agent.speed);
    }

    private void OnEnable()
    {
        agent.updateRotation = false;
        Vector3 direction = GameFuncs.PlayerScript.transform.position - transform.position;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);

            transform.rotation = rotation;
        }
        agent.updateRotation = true;
    }

    private void FixedUpdate()
    {
        if (!activeAI)
            return;

        if (ChasingPlayer())
        {
            onAggro.Invoke();
            animator.SetBool("Chase", true);
            agent.destination = GameFuncs.PlayerScript.transform.position;
        }
        else
        {
            //Patrol();
        }
    }

    private void StopHealilng()
    {
        bossHealer.StopHealing();
        Phase1Complete = true;
        healing = false;
        bossDoors.ResetDoors();
        agent.destination = GameFuncs.PlayerScript.transform.position;
        animator.SetBool("Chase", true);
    }

    private bool BossReachedHeal()
    {
        if (Vector3.Distance(transform.position, healSpot.position) <= agent.stoppingDistance + 0.15f)
        {
            animator.SetBool("Chase", false);
            return true;
        }
        else
            return false;
    }

    private bool PlayerClose()
    {
        if (healing)
            return false;

        if (Vector3.Distance(transform.position, GameFuncs.PlayerScript.transform.position) < agent.stoppingDistance
            && !GameFuncs.PlayerScript.IsDead())
        {
            agent.updateRotation = false;
            Vector3 direction = GameFuncs.PlayerScript.transform.position - transform.position;
            direction.y = 0;
            if (direction != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.fixedDeltaTime);
            }

            animator.SetBool("PlayerClose", true);
            return true;
        }
        agent.updateRotation = true;
        animator.SetBool("PlayerClose", false);
        return false;
    }

    private void DamagePlayer()
    {
        return;
        //currentAttackCoolDown += Time.deltaTime;
        if (!PlayerClose())
        {
            return;
        }
        return;
        if (currentAttackCoolDown >= attackCoolDown) // Ready to strike player
        {
            animator.SetBool("Idle", true);
            if (currentAttackDelay >= attackDelay) // Moment damage is done
            {
                
            }

            currentAttackDelay += Time.deltaTime;
        }
    }

    public void GetDamage(float amount)
    {
        enabled = true;
        activeAI = true;
        chase = true;
        if (health - amount <= 0)
        {
            if (!canKill)
                return;
            animator.SetBool("Dead", true);
            agent.isStopped = true;
            audio.PlayOneShot(deathClip, 0.4f);
        }
        else
        {
            audio.PlayOneShot(hurtClip, 1f);
            health -= amount;
        }
    }

    public void Step()
    {
        //audio.PlayOneShot(stepClip, 0.7f);
    }

    public void Death()
    {
        onKill.Invoke();
        enabled = false;
    }

    public void AnimationAttack()
    {
        if (agent.remainingDistance <= ATTACK_RANGE)
        {
            GameFuncs.PlayerScript.GetDamage(attackDamage);
        }
        currentAttackDelay = 0f;
        currentAttackCoolDown = 0f;
    }

    private bool ChasingPlayer()
    {
        if (healing)
            return false;
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

    public void Activate()
    {
        activeAI = true;
    }

    public void ResetMonster()
    {
        if (!gameObject.activeInHierarchy)
            return;
        agent.destination = startPosition;
        transform.position = startPosition;
        enabled = false;
        transform.rotation = startRotation;
        chase = false;
    }
}
