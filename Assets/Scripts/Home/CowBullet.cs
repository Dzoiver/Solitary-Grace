using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowBullet : MonoBehaviour
{
    ParticleSystem particles;
    float detectionRadius = 5f;
    [SerializeField] LayerMask targetLayerMask;
    [SerializeField] CowBazooka cowBazooka;
    [SerializeField] CowPlayer player;
    Rigidbody rb;
    MeshRenderer mesh;
    [SerializeField] Camera cam;
    Vector3 rotationY;
    Quaternion bulletRotation;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        particles = GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
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
        //bulletRotation = rb.rotation;
        //bulletRotation.x = rb.velocity.y;
        //rb.rotation = bulletRotation;
    }

    private void Explode()
    {
        rb.isKinematic = true;
        mesh.enabled = false;
        particles.Play();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, targetLayerMask);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Enemy"))
            {
                float targetDistance = Vector3.Distance(transform.position, hitCollider.transform.position); // max  1*40 /5
                float normalizedDistance = targetDistance / detectionRadius;
                float damageToTarget = 40 - (normalizedDistance * 25);
                hitCollider.gameObject.GetComponent<EnemyCow>().GetDamage(damageToTarget);
            }
        }
        cam.transform.parent = player.transform;
        cam.transform.localPosition = new Vector3(0, 5.42999983f, -4.80999994f);
        cowBazooka.ResetBazooka();
        player.SetControl(true);
    }

    public void Launch(float speed)
    {
        mesh.enabled = true;
        rb.isKinematic = false;
        
        Quaternion myRotation = Quaternion.Euler(0, player.transform.rotation.eulerAngles.x, 0);
        rb.AddForce( (player.transform.forward + Vector3.up) * speed);
    }
}
