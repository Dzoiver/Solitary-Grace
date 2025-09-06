using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeHitbox : MonoBehaviour
{
    [SerializeField] Knife knife;
    private float hitboxLingerTime = 0.1f;
    private float currentHitboxLingerTime = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("hit");
            other.gameObject.GetComponent<Monster>().GetDamage(knife.GetDamageValue());
        }
    }

    private void Awake()
    {
        gameObject.SetActive(false);
        GetComponent<MeshRenderer>().enabled = false;
    }

    void Update()
    {
        currentHitboxLingerTime += Time.deltaTime;
        if (currentHitboxLingerTime > hitboxLingerTime)
        {
            gameObject.SetActive(false);
            currentHitboxLingerTime = 0f;
        }
    }
}
