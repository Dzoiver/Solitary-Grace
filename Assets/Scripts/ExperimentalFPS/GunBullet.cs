using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : Projectile
{
    float speed = 80f;
    bool launched = false;
    Vector3 Direction;
    float timeAlive = 0f;
    float timeToDie = 5f;
    float randomness = 0.1f;
    AudioSource audio;
    MeshRenderer mesh;
    BoxCollider box;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        mesh = GetComponent<MeshRenderer>();
        box = GetComponent<BoxCollider>();
    }

    public void Launch(Vector3 direction, Vector3 rotation, bool random = false)
    {
        if (random)
        {
            direction.x += Random.Range(-randomness, randomness);
            direction.y += Random.Range(-randomness, randomness);
            direction.z += Random.Range(-randomness, randomness);
        }
        Direction = direction;
        transform.rotation = Quaternion.Euler(rotation);
        launched = true;
        gameObject.SetActive(true);
        mesh.enabled = true;
        box.enabled = true;
        enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Monster>().GetDamage(17f);
            gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<Boss>().GetDamage(17f);
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.name.Contains("Player") &&
            !collision.gameObject.name.Contains("Bullet"))
        {
            enabled = false;
            mesh.enabled = false;
            box.enabled = false;
            if (collision.gameObject.layer == 0)
                audio.Play();
        }
            
    }

    void Update()
    {
        if (launched)
        {
            transform.position += Direction * speed * Time.deltaTime;
            timeAlive += Time.deltaTime;
            if (timeAlive > timeToDie)
            {
                timeAlive = 0f;
                gameObject.SetActive(false);
            }
        }
    }
}
