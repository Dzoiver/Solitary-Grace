using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : Projectile
{
    float speed = 40f;
    bool launched = false;
    Vector3 Direction;
    float timeAlive = 0f;
    float timeToDie = 5f;

    public void Launch(Vector3 direction, Vector3 rotation)
    {
        Direction = direction;
        transform.rotation = Quaternion.Euler(rotation);
        launched = true;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Monster>().GetDamage(15f);
            gameObject.SetActive(false);
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
