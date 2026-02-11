using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPrism : MonoBehaviour
{
    AudioSource audio;
    [SerializeField] AudioClip wallSound;
    [SerializeField] AudioClip playerHit;
    [SerializeField] AudioClip launchClip;
    bool launched = false;
    float prismSpeed = 15f;
    float minDamage = 10f;
    float maxDamage = 20f;
    private float rotationSpeed = 3f;
    Rigidbody rb;
    // Start is called before the first frame update
    private void Awake()
    {
        enabled = false;
    }
    void Start()
    {
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        //Physics.IgnoreCollision(GetComponent<Collider>(), GameFuncs.PlayerScript.GetComponent<Collider>(), true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //transform.LookAt(GameFuncs.PlayerScript.transform.position);
        Vector3 direction = GameFuncs.PlayerScript.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.forward) *
                              Quaternion.Euler(90, 0, 0);
        //transform.rotation = targetRotation * Quaternion.Euler(90, 0, 0); // Корректировка

        transform.rotation = Quaternion.Slerp(
        transform.rotation,
        targetRotation,
        rotationSpeed * Time.fixedDeltaTime
    );


        if (!launched)
        {
            transform.Translate(0f, -3f * Time.fixedDeltaTime, 0f, Space.World);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, GameFuncs.PlayerScript.transform.position, prismSpeed * Time.fixedDeltaTime);
        /*
        Vector3 direction = GameFuncs.PlayerScript.transform.position - transform.position;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);

            transform.rotation = rotation;
        */
    }

    public void Launch()
    {
        audio.PlayOneShot(launchClip, 0.2f);
        launched = true;
        rb.constraints = RigidbodyConstraints.None;
    }

    public void Summon(Transform newTransform)
    {
        gameObject.SetActive(true);
        enabled = true;
        transform.position = newTransform.position;
        
        transform.position = transform.position + Random.insideUnitSphere * 1.9f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && enabled)
        {
            audio.PlayOneShot(playerHit);
            GameFuncs.PlayerScript.GetDamage(Random.Range(minDamage, maxDamage));
            enabled = false;
            launched = false;
            gameObject.SetActive(false);
        }
        else if (!other.CompareTag("Boss"))
        {
            if (launched)
            {
                audio.PlayOneShot(wallSound);
                enabled = false;
                launched = false;
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }
}
