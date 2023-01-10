using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField] ProjectilesManager bulletManager;
    [SerializeField] PlayerScript player;
    bool isFire = false;
    float bulletRate = 0.15f;
    float currentTime = 0f;
    Vector3 direction;
    Vector3 rotationBullet;

    private void Shoot()
    {
        currentTime = 0f;
        var bullet = bulletManager.GetNewBullet();
        bullet.transform.position = transform.position;
        direction.x = player.transform.rotation.y;
        direction.y = Camera.main.transform.rotation.x;
        rotationBullet.x = Camera.main.transform.rotation.eulerAngles.x;
        rotationBullet.y = player.transform.rotation.eulerAngles.y;
        bullet.Launch(transform.up, rotationBullet);
    }
    void Update()
    {
        isFire = Input.GetKey(KeyCode.Mouse0);
        if (isFire && currentTime > bulletRate)
        {
            Shoot();
        }
        currentTime += Time.deltaTime;
    }
}
