using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyCow : MonoBehaviour
{
    int health = 100;
    public int maxHealth = 100;
    float damageNumberSpeed = 0.5f;
    MeshRenderer mesh;
    [SerializeField] TextMeshPro text;
    Vector3 textStartPos;
    CowLawn game;
    private Sequence currentDamageSequence;
    //float currentDamageNumberShowTime = 0f;
    //float damageNumberShowTime = 4f;
    private void Awake()
    {
        game = FindObjectOfType<CowLawn>();
    }

    void Start()
    {
        health = maxHealth;
        textStartPos = text.transform.position;
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        text.transform.Translate(0, damageNumberSpeed * Time.deltaTime, 0);
        text.gameObject.transform.rotation = Camera.main.transform.rotation;
    }

    public void GetDamage(float damage)
    {
        int intDamage = (int)damage;
        text.text = intDamage.ToString();
        ShowDamageNumber();

        if (damage >= health)
            game.UpdateScore(health);
        else
            game.UpdateScore(intDamage);

        health -= intDamage;

        if (health <= 0)
        {
            mesh.enabled = false;
            game.EnemyKill();
        }
    }

    private void ShowDamageNumber()
    {
        text.DOKill(true);
        currentDamageSequence.Kill(true);
        text.gameObject.SetActive(true);

        text.DOColor(new Color(1f, 1f, 1f, 1f), 0f);
        text.transform.position = textStartPos;

        currentDamageSequence = DOTween.Sequence();

        currentDamageSequence
            .AppendInterval(2f) // Wait for 2 seconds
            .Append(text.DOFade(0f, 2f)) // Then fade out over 2 seconds
            .OnComplete(() => text.gameObject.SetActive(false)); // Optional: hide the object when done
    }

    IEnumerator DelayedFade()
    {
        yield return new WaitForSeconds(2f);
        text.DOFade(0f, 2f);
    }
}
