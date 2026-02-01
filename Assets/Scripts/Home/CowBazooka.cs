using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CowBazooka : MonoBehaviour
{
    [SerializeField] CowBullet bullet;
    [SerializeField] CowPlayer player;
    [SerializeField] Camera cam;
    Vector3 camStartPos;
    public Slider slider;
    public float chargeValue = 0f;
    public float speed = 1f;
    bool charging = false;
    bool shoot = false;
    AudioSource audio;

    void Start()
    {
        slider = GetComponent<Slider>();
        camStartPos = cam.transform.position;
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (player.AvailableAmmo <= 0)
        {
            return;
        }

        if (Input.GetKey(KeyCode.Space) && !shoot)
        {
            audio.Play();
            slider.value += speed * Time.deltaTime;
            charging = true;
        }

        if (Input.GetKeyUp(KeyCode.Space) && charging)
        {
            chargeValue = 17f/slider.value;
            charging = false;
            shoot = true;
            bullet.transform.position = player.transform.position + Vector3.up * 2f;
            bullet.Launch((slider.value / slider.maxValue) * (90 - 5));
            player.SetControl(false);
            cam.transform.parent = bullet.transform;
            player.AvailableAmmo--;
        }
    }

    public void ResetBazooka()
    {
        slider.value = 0f;
        shoot = false;
    }
}
