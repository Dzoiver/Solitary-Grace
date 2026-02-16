using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowBullet : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    float detectionRadius = 5f;
    [SerializeField] LayerMask targetLayerMask;
    [SerializeField] CowBazooka cowBazooka;
    [SerializeField] CowPlayer player;
    [SerializeField] float damage = 35;
    Rigidbody rb;
    [SerializeField] GameObject bulletModel;
    [SerializeField] Camera cam;
    Vector3 rotationY;
    Quaternion bulletRotation;
    AudioSource audio;
    [SerializeField] AudioClip explosionSound;
    float lifeTime = 5f;
    float currentLifeTime = 0f;
    public Color circleColor = Color.red;

    float rotationSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        audio = GetComponent<AudioSource>();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = circleColor;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "CowPlayer")
        {
            Explode();
        }
    }
    void Update()
    {
        if (bulletModel.activeSelf)
        {
            currentLifeTime += Time.deltaTime;
            bulletModel.transform.Rotate(45f * Time.deltaTime * cowBazooka.chargeValue, 0f, 0f, Space.Self); // cowBazooka.slider.maxValue / cowBazooka.chargeValue
        }
        if (currentLifeTime > lifeTime)
        {
            Explode();
        }
            
        
    }

    private void Explode()
    {
        currentLifeTime = 0f;
        rb.isKinematic = true;
        bulletModel.SetActive(false);
        particles.Play();
        audio.clip = explosionSound;
        audio.Play();
        cam.transform.parent = player.transform;
        cam.transform.localPosition = new Vector3(0, 3.42999983f, -4.80999994f);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, targetLayerMask);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Enemy"))
            {
                float targetDistance = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (targetDistance < 2.3f)
                    hitCollider.gameObject.GetComponent<EnemyCow>().GetDamage(35f);
                else
                {
                    Debug.Log(targetDistance);
                    float normalizedDistance = Mathf.Clamp01(targetDistance / detectionRadius);
                    float damageMultiplier = 1 - (normalizedDistance * normalizedDistance * 0.5f);
                    float damageToTarget = damage * Mathf.Max(0.2f, damageMultiplier);
                    hitCollider.gameObject.GetComponent<EnemyCow>().GetDamage(damageToTarget);
                }
            }
        }
        cowBazooka.ResetBazooka();
        if (!player.cowlawn.gameoverPanel.activeSelf)
            player.SetControl(true);

        if (player.AvailableAmmo <= 0 && !player.cowlawn.gameoverPanel.activeSelf)
            player.cowlawn.GameOver();
    }

    public void Launch(float speed)
    {
        currentLifeTime = 0f;
        bulletModel.SetActive(true);
        bulletModel.transform.rotation = Quaternion.Euler(90f, bulletModel.transform.rotation.eulerAngles.y, bulletModel.transform.rotation.eulerAngles.z);
        rb.isKinematic = false;
        
        Quaternion myRotation = Quaternion.Euler(0, player.transform.rotation.eulerAngles.x, 0);
        rb.AddForce( (player.transform.forward + Vector3.up) * speed);
    }

    public void ResetProjectile()
    {
        bulletModel.SetActive(false);
        rb.isKinematic = true;
        particles.Stop();
        cam.transform.parent = player.transform;
        cam.transform.localPosition = new Vector3(0, 5.42999983f, -4.80999994f);
    }
}
