using GM;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using static Boss;

public class Boss : MonoBehaviour
{
    [SerializeField] private bool activeAI = true;
    private NavMeshAgent agent;
    [SerializeField] private float detectRadius = 10f;

    private float health = 600f;
    private float maxHealth = 1000f;

    public float minDamage = 35f;
    public float maxDamage = 45f;
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

    public BossDoorsController bossDoors;
    [SerializeField] BossHealer bossHealer;
    public UnityEvent onKill;

    private Vector3 startPosition;
    private Quaternion startRotation;
    public float allowedAngle = 45f;
    public AudioSource audio;
    [SerializeField] AudioClip deathClip;
    [SerializeField] AudioClip hurtClip;
    [SerializeField] AudioClip stepClip;
    [SerializeField] AudioClip castClip;
    int fleshWallCount = 0;
    public AudioClip spawnClip;

    Animator animator;
    const float ATTACK_RANGE = 2.68f;

    const float RANGE_ATTACK_TIME = 0f;
    const float HEAL_HEALTH = 800f;
    const float HEAL_RANGE = 130f;
    float healSpeed = 60f;
    float currentRangeAttackTime = 10f;

    public bool OnceWoke = false;

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

    const float PRISM_COOLDOWN = 16f;
    float currentPrismTime = 0f;

    [SerializeField] BossPrism[] prisms;
    [SerializeField] Transform prismSummonTransform;

    PlayerScript playerScript;
    Vector3 healDestination = new Vector3();
    public BossState bossState = BossState.attack;

    public enum BossState
    {
        attack,
        range,
        cover,
        healing,
        afk
    }

    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        if (playerScript == null)
            playerScript = GameFuncs.PlayerScript;
        //rb = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        if (playerScript == null)
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
        Debug.Log(bossState);
        animator.SetFloat("Speed", agent.velocity.magnitude);
        if (bossState == BossState.range)
            return;

        currentPrismTime += Time.deltaTime;

        if (bossState == BossState.afk)
            onAggro.Invoke();

        if (bossState != BossState.healing)
            bossState = BossState.attack;
        if (bossHealer == null)
        {
            CorridorLogic();
            return;
        }

        if (Health <= HEAL_RANGE && bossHealer.aliveEyes.Count > 0 && bossState != BossState.healing)
        {
            bossState = BossState.cover;
        }

        if (bossState == BossState.cover && Vector3.Distance(transform.position, healSpot.position) <= agent.stoppingDistance + 0.15f)
        {
            bossState = BossState.healing;
        }

        if (currentPrismTime > PRISM_COOLDOWN
            && Vector3.Distance(transform.position, playerScript.transform.position) > 2.5f
            && Vector3.Distance(transform.position, healSpot.position) > 5f
            && bossState != BossState.cover
            && bossState != BossState.healing)
        {
            bossState = BossState.range;
        }

        switch (bossState)
        {
            case BossState.attack:
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("ATTACK"))
                    agent.destination = playerScript.transform.position;
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("ATTACK") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.6f)
                    agent.destination = playerScript.transform.position;
                OnceWoke = true;
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
                }
                else
                {
                    agent.updateRotation = true;
                    animator.SetBool("PlayerClose", false);
                }

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("ATTACK"))
                {
                    agent.updateRotation = false;
                    Vector3 direction = playerScript.transform.position - transform.position;
                    direction.y = 0;
                    if (direction != Vector3.zero)
                    {
                        Quaternion rotation = Quaternion.LookRotation(direction);

                        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.deltaTime);
                    }
                }
                    break;

            case BossState.range:
                agent.ResetPath();
                currentPrismTime = 0f;
                animator.SetBool("Summon", true);
                break;

            case BossState.cover:
                agent.updateRotation = true;
                bossDoors.OpenDoors();
                animator.SetBool("PlayerClose", false);
                agent.destination = healSpot.position;
                break;

            case BossState.healing:
                float eyesMultiplier = Mathf.Pow(1 - bossHealer.GetCurrentEyes() / (float)26, 0.5f);
                Health += healSpeed * eyesMultiplier * Time.deltaTime;
                bossDoors.CloseDoors();
                break;
            default:
                // Code to execute if no case matches
                break;
        }

        return;

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


        if (BossReachedHeal()) // Boss has walked all the way to the heal spot
        {
            bossDoors.CloseDoors();
            bossHealer.SpawnEyes();
            bossHealer.enabled = true;
        }

        if (bossHealer.StopHealCheck())
        {
            print("Phase: Cant heal anymore");
            Healing = false;
            //Debug.Log("well i can't heal so i go to player");
            if (!animator.GetBool("Summon"))
                agent.destination = playerScript.transform.position;
            //canKill = true;

            StopHealilng();
        }
        
        if (Healing && Health >= HEAL_HEALTH1) // Heal until 800 HP is restored
        {
            print("Phase: Restored Health");
            //canKill = true;
            Healing = false;
            bossHealer.StopHealing();
            StopHealilng();
        }

        if (Healing && bossHealer.enabled) // Increase boss health for each eye
        {
            print("Phase: Healing");
            //Debug.Log(health);
            //Debug.Log(bossHealer.GetCurrentEyes() * healSpeed * Time.deltaTime);
            float eyesMultiplier = Mathf.Pow(1 - bossHealer.GetCurrentEyes() / (float)26, 0.5f);
            Health += healSpeed * eyesMultiplier * Time.deltaTime;
        }
    }

    public void CorridorLogic()
    {
        agent.destination = playerScript.transform.position;
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
        }
        else
        {
            Debug.Log("update rotation");
            agent.updateRotation = true;
            animator.SetBool("PlayerClose", false);
        }
    }

    public void RangeAttack()
    {
        if (currentPrismTime > PRISM_COOLDOWN && Vector3.Distance(transform.position, playerScript.transform.position) > 3.5f && Vector3.Distance(transform.position, healSpot.position) > 5f && agent.destination != healDestination)
        {
            agent.ResetPath();
            currentPrismTime = 0f;
            animator.SetBool("Summon", true);
        }
    }

    private void OnEnable()
    {
        agent.updateRotation = false;
        Vector3 direction;
        if (playerScript == null)
            direction = GameFuncs.PlayerScript.transform.position - transform.position;
        else
            direction = playerScript.transform.position - transform.position;
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
        return;

        if (!activeAI)
            return;

        if (ChasingPlayer() && !animator.GetBool("Summon"))
        {
            onAggro.Invoke();
            OnceWoke = true;
            animator.SetBool("Chase", true);
            AnimatorStateInfo animInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (!animInfo.IsName("Attack"))
            {
                agent.destination = playerScript.transform.position;
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
        if (!animator.GetBool("Summon"))
        {
            agent.destination = playerScript.transform.position;
            animator.SetBool("Chase", true);
        }
    }

    private bool BossReachedHeal()
    {
        if (Vector3.Distance(transform.position, healSpot.position) <= agent.stoppingDistance + 0.15f && bossHealer.enabled)
        {
            print("Phase: ReachedHeal");
            animator.Play("IDOL 2");
            agent.ResetPath();
            Healing = true;
            agent.updateRotation = true;
            animator.SetBool("PlayerClose", false);
            return true;
        }
        else
            return false;
    }

    private bool DamagePlayer()
    {
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
            Debug.Log("rotating");
            animator.SetBool("PlayerClose", true);
            return true;
        }
        else
        {
            agent.updateRotation = true;
            animator.SetBool("PlayerClose", false);
        }
        return false;
    }

    public void GetDamage(float amount)
    {
        enabled = true;
        activeAI = true;
        chase = true;
        if (Health - amount <= 0)
        {
            Health = 0f;
            if (FleshWallCount >= 4)
            {
                animator.SetBool("Dead", true);
                agent.isStopped = true;
                audio.PlayOneShot(deathClip, 0.4f);
            }
        }
        else
        {
            audio.PlayOneShot(hurtClip, 1f);
            Health -= amount;
            /*
            // Handle going to Heal spot
            if (Health < HEAL_RANGE && FleshWallCount <= 4) // When low, goes to healing spot
            {
                agent.destination = healSpot.position;
                healDestination = agent.destination;
                bossDoors.OpenDoors();
                bossHealer.enabled = true;
            }
            */
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
        if (Vector3.Distance(playerScript.transform.position, transform.position) <= ATTACK_RANGE)
        {
            playerScript.Health -= Random.Range(minDamage, maxDamage);
            if (playerScript.IsDead())
                StartCoroutine(DelayReset());
        }
        else
        {
            animator.SetBool("PlayerClose", false);
        }
        currentAttackDelay = 0f;
        currentAttackCoolDown = 0f;
        /*
        if (Health < HEAL_RANGE && FleshWallCount <= 4) // When low, goes to healing spot
        {
            agent.destination = healSpot.position;
            healDestination = agent.destination;
            bossDoors.OpenDoors();
            bossHealer.enabled = true;
        }
        */
    }

    public void AnimationPrismFinish()
    {
        bossState = BossState.attack;
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
        animator.SetFloat("Speed", 0f);
        agent.ResetPath();
        bossState = BossState.afk;
        transform.position = startPosition;
        transform.rotation = startRotation;

        enabled = false;
        chase = false;
    }

    public void ForceAggro()
    {
        if (gameObject.activeSelf)
            agent.destination = playerScript.transform.position;
    }
}
