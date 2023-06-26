using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : Projectile
{
    float speed = 40f;
    bool launched = false;
    Vector3 Direction;

    public void Launch(Vector3 direction, Vector3 rotation)
    {
        Direction = direction;
        transform.rotation = Quaternion.Euler(rotation);
        launched = true;
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (launched)
        {
            transform.position += Direction * speed * Time.deltaTime;
        }
    }
}
