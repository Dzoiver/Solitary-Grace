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
    private NavMeshAgent agent;
    private Animator animator;

    [SerializeField] private bool activeAI = true;
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
    private bool healing = false;
    private bool canKill = false;
    private bool Phase1Complete = false;

    public BossDoorsController bossDoors;
    [SerializeField] BossHealer bossHealer;
    public UnityEvent onKill;

    private Vector3 startPosition;
    private Quaternion startRotation;
    public float allowedAngle = 45f;
    public AudioSource audio;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private AudioClip hurtClip;
    [SerializeField] private AudioClip stepClip;
    [SerializeField] private AudioClip castClip;
    private int fleshWallCount = 0;
    public AudioClip spawnClip;

    private const float ATTACK_RANGE = 2.68f;
    private const float RANGE_ATTACK_TIME = 0f;
    private const float HEAL_HEALTH = 800f;
    private const float HEAL_RANGE = 130f;
    private const float HEAL_SPOT_STOP_DISTANCE = 0.5f;
    private float healSpeed = 60f;
    private float currentRangeAttackTime = 10f;

    public bool OnceWoke = false;

    public UnityEvent onAggro;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] FleshWall[] walls;

    public int FleshWallCount
    {
        get => fleshWallCount;
        set
        {
            fleshWallCount = value;
            if (fleshWallCount >= 4)
                canKill = true;
        }
    }

    public float Health
    {
        get => health;
        set
        {
            health = value;
            if (healthText != null)
                healthText.text = health.ToString();
        }
    }

    public static float HEAL_HEALTH1 => HEAL_HEALTH;

    public bool Healing
    {
        get => healing;
        set => healing = value;
    }

    private const float PRISM_COOLDOWN = 16f;
    private float currentPrismTime = 0f;

    [SerializeField] private BossPrism[] prisms;
    [SerializeField] private Transform prismSummonTransform;

    private PlayerScript playerScript;
    private Vector3 healDestination = new Vector3();
    public BossState bossState = BossState.Attack;

    public enum BossState
    {
        Attack,
        Range,
        Cover,
        Healing,
        AFK
    }

    private void Awake()
    {
        if (playerScript == null)
        {
            playerScript = GameFuncs.PlayerScript;
        }

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        audio = GetComponent<AudioSource>();
        startRotation = transform.rotation;
        startPosition = transform.position;
        if (bossHealer == null)
        {
            canKill = true;
        }
    }

    void Start()
    {
        if (playerScript == null)
        {
            playerScript = GameFuncs.PlayerScript;
        }
        //rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Debug.Log(bossState);
        animator.SetFloat("Speed", agent.velocity.magnitude);
        if (bossState == BossState.Range)
            return;

        currentPrismTime += Time.deltaTime;

        if (bossState == BossState.AFK)
            onAggro.Invoke();

        if (bossState != BossState.Healing)
        {
            agent.stoppingDistance = 2f;
            bossState = BossState.Attack;
        }

        if (bossHealer == null)
        {
            CorridorLogic();
            return;
        }

        if (Health <= HEAL_RANGE && bossHealer.aliveEyes.Count > 0 && bossState != BossState.Healing)
        {
            bossState = BossState.Cover;
        }

        if (bossState == BossState.Cover)
        {
            agent.stoppingDistance = 0.1f;

            var currentPosFlat = transform.position;
            currentPosFlat.y = 0f;
            var healSpotFlat = healSpot.position;
            healSpotFlat.y = 0f;

            if (Vector3.Distance(currentPosFlat, healSpotFlat) <= HEAL_SPOT_STOP_DISTANCE)
            {
                bossState = BossState.Healing;
            }
        }

        if (currentPrismTime > PRISM_COOLDOWN
            && Vector3.Distance(transform.position, playerScript.transform.position) > 2.5f
            && Vector3.Distance(transform.position, healSpot.position) > 5f
            && bossState != BossState.Cover
            && bossState != BossState.Healing)
        {
            bossState = BossState.Range;
        }

        switch (bossState)
        {
            case BossState.Attack:
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("ATTACK"))
                {
                    agent.destination = playerScript.transform.position;
                }

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("ATTACK") &&
                    animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.6f)
                {
                    agent.destination = playerScript.transform.position;
                }

                if (Vector3.Distance(transform.position, playerScript.transform.position) <=
                    agent.stoppingDistance + 0.5f
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

            case BossState.Range:
                agent.ResetPath();
                currentPrismTime = 0f;
                animator.SetBool("Summon", true);
                break;

            case BossState.Cover:
                agent.updateRotation = true;
                bossDoors.OpenDoors();
                animator.SetBool("PlayerClose", false);
                agent.destination = healSpot.position;
                break;

            case BossState.Healing:
                float eyesMultiplier = Mathf.Pow(1 - bossHealer.GetCurrentEyes() / (float)26, 0.5f);
                Health += healSpeed * eyesMultiplier * Time.deltaTime;
                bossDoors.CloseDoors();
                break;
            default:
                break;
        }
    }

    private void CorridorLogic()
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
        if (currentPrismTime > PRISM_COOLDOWN &&
            Vector3.Distance(transform.position, playerScript.transform.position) > 3.5f &&
            Vector3.Distance(transform.position, healSpot.position) > 5f && agent.destination != healDestination)
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
    }

    public void AnimationPrismFinish()
    {
        bossState = BossState.Attack;
        animator.SetBool("Summon", false);
    }

    public void SummonPrism()
    {
        foreach (BossPrism p in prisms)
        {
            p.Summon(prismSummonTransform);
        }

        StartCoroutine(DelayLaunch());
    }

    private IEnumerator DelayLaunch()
    {
        yield return new WaitForSeconds(1f);
        foreach (BossPrism p in prisms)
        {
            p.Launch();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator DelayReset()
    {
        yield return new WaitForSeconds(1f);
        ResetMonster();
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
        {
            return;
        }

        animator.SetFloat("Speed", 0f);
        agent.ResetPath();
        bossState = BossState.AFK;
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