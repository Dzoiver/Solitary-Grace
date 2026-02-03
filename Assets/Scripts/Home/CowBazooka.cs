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
    float maxAdditionalSpeed = 55f;
    float baseSpeed = 30f;

    public bool Charging { get => charging; set => charging = value; }
    public bool Shoot { get => shoot; set => shoot = value; }

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

        if (Input.GetKey(KeyCode.Space) && !Shoot)
        {
            audio.Play();
            slider.value += speed * Time.deltaTime;
            Charging = true;
        }

        if (Input.GetKeyUp(KeyCode.Space) && Charging)
        {
            chargeValue = 17f/slider.value;
            Charging = false;
            Shoot = true;
            bullet.transform.position = player.transform.position + Vector3.up * 2f;
            //bullet.Launch((slider.value / slider.maxValue) * (90 - 5));
            float t = slider.value / slider.maxValue;
            float speed = Mathf.Pow(t, 1.3f) * maxAdditionalSpeed + baseSpeed;
            bullet.Launch(speed);
            player.SetControl(false);
            cam.transform.parent = bullet.transform;
            player.AvailableAmmo--;
        }
    }

    public void ResetBazooka()
    {
        slider.value = 0f;
        Shoot = false;
    }
}
