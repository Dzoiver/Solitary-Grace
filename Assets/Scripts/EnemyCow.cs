using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyCow : MonoBehaviour
{
    float health = 100f;
    float maxHealth = 100f;
    float damageNumberSpeed = 0.5f;
    MeshRenderer mesh;
    [SerializeField] TextMeshPro text;
    Vector3 textStartPos;
    //float currentDamageNumberShowTime = 0f;
    //float damageNumberShowTime = 4f;
    // Start is called before the first frame update
    void Start()
    {
        textStartPos = text.transform.position;
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        text.transform.Translate(0, damageNumberSpeed * Time.deltaTime, 0);
    }

    public void GetDamage(float damage)
    {
        text.text = Mathf.Round(damage).ToString();
        ShowDamageNumber();
        health -= damage;
        if (health <= 0)
        {
            mesh.enabled = false;
        }
    }

    private void ShowDamageNumber()
    {
        text.gameObject.SetActive(true);
        text.DOColor(new Color(1f, 1f, 1f, 1f), 0f);
        text.transform.position = textStartPos;
        StartCoroutine(DelayedFade());
    }

    IEnumerator DelayedFade()
    {
        yield return new WaitForSeconds(2f);
        text.DOFade(0f, 2f);
    }
}
