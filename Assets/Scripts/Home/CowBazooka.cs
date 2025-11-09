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
    Slider slider;
    public float speed = 1f;
    bool charging = false;
    bool shoot = false;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        camStartPos = cam.transform.position;
        //Vector3 vect = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !shoot)
        {
            slider.value += speed * Time.deltaTime;
            charging = true;
        }

        if (Input.GetKeyUp(KeyCode.Space) && charging)
        {
            charging = false;
            shoot = true;
            bullet.transform.position = player.transform.position + Vector3.up * 2f;
            bullet.Launch((slider.value / slider.maxValue) * (90 - 5));
            player.SetControl(false);
            cam.transform.parent = bullet.transform;
            //Debug.Log(-bullet.transform.forward.z * 5);
            //cam.transform.localPosition = camStartPos;
            //cam.transform.rota = player.transform.rotation;
            //cam.transform.localRotation = Quaternion.Euler(50f, 0f, 0f);
        }
    }

    public void ResetBazooka()
    {
        slider.value = 0f;
        shoot = false;
    }
}
