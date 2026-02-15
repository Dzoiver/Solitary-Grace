using GM;
using UnityEngine;
using Zenject;

public class DeathTrigger : MonoBehaviour
{
    [Inject] GameOver gameover;
    [SerializeField] AudioSource source;
    public bool autoDelete = false;
    private bool startTimer = false;
    float currentTime = 0f;
    [SerializeField] float activationTime = 6f;
    BoxCollider box;
    [SerializeField] Guardian guardian;
    public bool triggerOnce = false;

    private void Awake()
    {
        box = GetComponent<BoxCollider>();
        if (autoDelete)
        {
            box.enabled = false;
            //gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (guardian != null)
                guardian.KillPlayer();
            source.Play(); // Scream sound
            // gameover.gameObject.SetActive(true);
            //gameover.DieFromMonster();
            gameover.NormalDeath(other.gameObject.GetComponent<PlayerScript>());
            if (triggerOnce)
                box.enabled = false;
        }
    }

    private void Update()
    {
        if (!startTimer)
            return;
        if (currentTime > activationTime)
        {
            StopTimer();
            box.enabled = true;
        }
        currentTime += Time.deltaTime;
    }

    public void StartTimer()
    {
        startTimer = true;
    }

    public void StopTimer()
    {
        startTimer = false;
        currentTime = 0f;
        box.enabled = false;
    }
}
