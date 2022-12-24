using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    bool fire = false;
    float bulletRate = 0.15f;
    float currentTime = 0f;
    [SerializeField] ProjectilesManager bulletManager;
    [SerializeField] PlayerScript player;
    Vector3 direction;
    Vector3 rotationBullet;

    private void Shoot()
    {
        currentTime = 0f;
        GunBullet bullet = bulletManager.GetNewBullet();
        bullet.transform.position = transform.position;
        //direction.x = player.;
        direction.y = Camera.main.transform.rotation.x;
        direction.x = player.transform.rotation.y;
        //direction.y = pCamera.MouseY;
        rotationBullet.y = player.transform.rotation.eulerAngles.y;
        rotationBullet.x = Camera.main.transform.rotation.eulerAngles.x;
        // rotationBullet = Quaternion.ToEulerAngles(Quaternion.Euler(rotationBullet));
        bullet.Launch(transform.up, rotationBullet);
    }
    void Update()
    {
        fire = Input.GetKey(KeyCode.Mouse0);
        if (fire && currentTime > bulletRate)
        {
            Shoot();
        }
        currentTime += Time.deltaTime;
    }
}
