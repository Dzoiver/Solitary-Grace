using GM;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    [SerializeField] private bool activeAI = true;
    private NavMeshAgent agent;
    [SerializeField] private float detectRadius = 10f;

    private float health = 600f;
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
    public UnityEvent onKill;

    private Vector3 startPosition;
    private Quaternion startRotation;
    public float allowedAngle = 45f;
    public AudioSource audio;
    [SerializeField] AudioClip deathClip;
    [SerializeField] AudioClip hurtClip;
    [SerializeField] AudioClip stepClip;
    int fleshWallCount = 0;
    public AudioClip spawnClip;

    Animator animator;
    const float ATTACK_RANGE = 2.9f;

    const float RANGE_ATTACK_TIME = 0f;
    const float HEAL_HEALTH = 800f;
    const float HEAL_RANGE = 300f;
    float healSpeed = 250f;
    float currentRangeAttackTime = 10f;

    public UnityEvent onAggro;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] FleshWall[] walls;

    public int FleshWallCount { get => fleshWallCount; set { 
            fleshWallCount = value;
            if (fleshWallCount >= 4)
                canKill = true;
        } }

    public float Health { get => health; set { 
            health = value;
            if (healthText != null)
                healthText.text = health.ToString();
        } }

    public static float HEAL_HEALTH1 => HEAL_HEALTH;

    public bool Healing { get => healing; set => healing = value; }

    const float PRISM_COOLDOWN = 15f;
    float currentPrismTime = 0f;

    [SerializeField] BossPrism[] prisms;
    [SerializeField] Transform prismSummonTransform;

    PlayerScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        //rb = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        playerScript = GameFuncs.PlayerScript;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        audio = GetComponent<AudioSource>();
        startRotation = transform.rotation;
        startPosition = transform.position;
        if (bossHealer == null)
            canKill = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!activeAI)
            return;
        animator.SetFloat("Speed", agent.velocity.magnitude);
        /*
        if (PlayerClose() && !Healing)
        {

        }
        */
        DamagePlayer();
        if (bossHealer == null)
            return;

        currentPrismTime += Time.deltaTime;
        if (currentPrismTime > PRISM_COOLDOWN && Vector3.Distance(transform.position, playerScript.transform.position) > 3.5f && Vector3.Distance(transform.position, healSpot.position) > 5f)
        {
            agent.ResetPath();
            currentPrismTime = 0f;
            animator.SetBool("Summon", true);
        }

        if (BossReachedHeal()) // Boss has walked all the way to the heal spot
        {
            bossDoors.CloseDoors();
            bossHealer.SpawnEyes();
            bossHealer.enabled = true;
        }

        if (bossHealer.CantHealAnymore())
        {
            
            Healing = false;
            //Debug.Log("well i can't heal so i go to player");
            if (!animator.GetBool("Summon"))
                agent.destination = playerScript.transform.position;
            //canKill = true;

            StopHealilng();
        }
        
        if (Healing && Health >= HEAL_HEALTH1) // Heal until 800 HP is restored
        {
            //canKill = true;
            Healing = false;
            bossHealer.StopHealing();
            StopHealilng();
        }

        if (Healing && bossHealer.enabled) // Increase boss health for each eye
        {
            //Debug.Log(health);
            //Debug.Log(bossHealer.GetCurrentEyes() * healSpeed * Time.deltaTime);
            Health += healSpeed / bossHealer.GetCurrentEyes() * Time.deltaTime;
        }
        
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

        if (ChasingPlayer() && !animator.GetBool("Summon"))
        {
            onAggro.Invoke();
            animator.SetBool("Chase", true);
            AnimatorStateInfo animInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (!animInfo.IsName("Attack"))
            {
                agent.destination = GameFuncs.PlayerScript.transform.position;
            }
                
        }
        else
        {
            //Patrol();
        }
    }

    private void StopHealilng()
    {
        Healing = false;
        bossDoors.ResetDoors();
        agent.destination = GameFuncs.PlayerScript.transform.position;
        animator.SetBool("Chase", true);
    }

    private bool BossReachedHeal()
    {
        if (Vector3.Distance(transform.position, healSpot.position) <= agent.stoppingDistance + 0.15f && bossHealer.enabled)
        {
            animator.Play("IDOL 2");
            agent.ResetPath();
            Healing = true;
            animator.SetBool("Chase", false);
            return true;
        }
        else
            return false;
    }

    private bool DamagePlayer()
    {
        if (Healing)
            return false;
        print("damaging player");
        if (Vector3.Distance(transform.position, playerScript.transform.position) <= agent.stoppingDistance + 0.5f
            && !playerScript.IsDead())
        {
            agent.updateRotation = false;
            Vector3 direction = playerScript.transform.position - transform.position;
            direction.y = 0;
            if (direction != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.deltaTime);
            }

            animator.SetBool("PlayerClose", true);
            return true;
        }
        agent.updateRotation = true;
        animator.SetBool("PlayerClose", false);
        return false;
    }

    public void GetDamage(float amount)
    {
        enabled = true;
        activeAI = true;
        chase = true;
        if (Health - amount <= 0)
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
            Health -= amount;

            // Handle going to Heal spot
            if (Health < HEAL_RANGE && FleshWallCount <= 4) // When low, goes to healing spot
            {
                agent.destination = healSpot.position;
                bossDoors.OpenDoors();
                bossHealer.enabled = true;
            }
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
        agent.ResetPath();
        if (agent.remainingDistance <= ATTACK_RANGE)
        {
            playerScript.GetDamage(attackDamage);
            if (playerScript.IsDead())
                StartCoroutine(DelayReset());
        }
        currentAttackDelay = 0f;
        currentAttackCoolDown = 0f;

        if (Health < HEAL_RANGE && FleshWallCount <= 4) // When low, goes to healing spot
        {
            agent.destination = healSpot.position;
            bossDoors.OpenDoors();
            bossHealer.enabled = true;
        }
    }

    public void AnimationPrismFinish()
    {
        animator.SetBool("Summon", false);
        //agent.destination = GameFuncs.PlayerScript.transform.position;
    }

    public void SummonPrism()
    {
        foreach (BossPrism p in prisms)
        {
            p.Summon(prismSummonTransform);
        }
        StartCoroutine(DelayLaunch());
    }

    IEnumerator DelayLaunch()
    {
        yield return new WaitForSeconds(1f);
        foreach (BossPrism p in prisms)
        {
            p.Launch();
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator DelayReset()
    {
        yield return new WaitForSeconds(1f);
        ResetMonster();
    }

    private bool ChasingPlayer()
    {
        if (Healing || Health < HEAL_RANGE || animator.GetCurrentAnimatorStateInfo(0).IsName("SummonPrism"))
        {
            Debug.Log("dont chase");
            return false;
        }
            
        Vector3 directionNormal = (playerScript.gameObject.transform.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, directionNormal, out RaycastHit hit, detectRadius, layerMask))
        {
            if (hit.collider.CompareTag("Player"))
            {
                chase = true;
                return true;
            }
        }
        return chase;
    }

    public void RotateToPlayer()
    {
        Vector3 direction = playerScript.transform.position - transform.position;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);

            transform.rotation = rotation;
        }
    }

    public void Activate()
    {
        activeAI = true;
        enabled = true;
    }

    public void ResetMonster()
    {
        if (!gameObject.activeInHierarchy)
            return;
        agent.ResetPath();
        transform.position = startPosition;
        transform.rotation = startRotation;

        enabled = false;
        chase = false;
    }
}
