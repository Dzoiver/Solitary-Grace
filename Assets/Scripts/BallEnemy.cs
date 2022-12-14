using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEnemy : MonoBehaviour
{
    float health = 1f;

    void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Projectile project = other.gameObject.GetComponent<Projectile>();
        if (project.projName == "Bullet")
        {
            TakeDamage(project.damage);
        }
    }
}
