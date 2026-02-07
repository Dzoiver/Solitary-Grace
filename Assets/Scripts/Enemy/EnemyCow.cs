using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyCow : MonoBehaviour
{
    int health = 100;
    public int maxHealth = 100;
    float damageNumberSpeed = 0.5f;
    [SerializeField] TextMeshPro text;
    Vector3 textStartPos;
    CowLawn game;
    private Sequence currentDamageSequence;
    [SerializeField] GameObject model;
    AudioSource audio;
    public bool patrol = false;
    [SerializeField] GameObject[] patrolPoints;
    NavMeshAgent agent;
    int currentPatrolIndex = 0;
    float currentPatrolWait = 0f;
    public float patrolWait = 0.5f;
    //float currentDamageNumberShowTime = 0f;
    //float damageNumberShowTime = 4f;
    private void Awake()
    {
        game = FindObjectOfType<CowLawn>();
        audio = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        health = maxHealth;
        textStartPos = text.transform.position;
        currentPatrolIndex = Random.Range(0, patrolPoints.Length);
        agent.destination = patrolPoints[currentPatrolIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        text.transform.Translate(0f, damageNumberSpeed * Time.deltaTime, 0f);
        text.gameObject.transform.rotation = Camera.main.transform.rotation;

        Patrol();
    }

    public void GetDamage(float damage)
    {
        if (!model.activeSelf)
            return;
        audio.Play();
        int intDamage = (int)damage;
        text.text = intDamage.ToString();
        ShowDamageNumber();

        if (damage >= health)
            game.UpdateScore(health);
        else
            game.UpdateScore(intDamage);

        health -= intDamage;

        if (health <= 0)
        {
            model.SetActive(false);
            game.EnemyKill();
            agent.isStopped = true;
        }
    }

    private void ShowDamageNumber()
    {
        text.DOKill(true);
        currentDamageSequence.Kill(true);
        text.gameObject.SetActive(true);

        text.DOColor(new Color(1f, 1f, 1f, 1f), 0f);
        text.transform.position = new Vector3(text.transform.position.x, textStartPos.y, text.transform.position.z);

        currentDamageSequence = DOTween.Sequence();

        currentDamageSequence
            .AppendInterval(2f) // Wait for 2 seconds
            .Append(text.DOFade(0f, 2f)) // Then fade out over 2 seconds
            .OnComplete(() => text.gameObject.SetActive(false)); // Optional: hide the object when done
    }

    public void ResetCow()
    {
        model.SetActive(true);
        health = maxHealth;
        agent.isStopped = false;
    }

    private void Patrol()
    {
        if (!patrol)
            return;
        if (patrolPoints.Length == 0)
            return;

        if (Vector3.Distance(transform.position, patrolPoints[currentPatrolIndex].transform.position) <= 1.5f)
        {
            if (currentPatrolWait > patrolWait)
            {
                currentPatrolWait = 0f;

                currentPatrolIndex = Random.Range(0, patrolPoints.Length);
                agent.destination = patrolPoints[currentPatrolIndex].transform.position;
            }
            currentPatrolWait += Time.deltaTime;
        }
    }

    IEnumerator DelayedFade()
    {
        yield return new WaitForSeconds(2f);
        text.DOFade(0f, 2f);
    }
}
