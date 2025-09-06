using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] private float damage = 35f;
    [SerializeField] private float attackCoolDown = 0.3f;
    private float currentAttackCoolDown = 0f;
    private float attackDelay = 0.1f;
    private float currentAttackDelay = 0f;
    [SerializeField] private KnifeHitbox hitbox;
    [SerializeField] private Animator knifeAnimator;

    public float GetDamageValue()
    {
        return damage;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Hit()
    {
        if (currentAttackCoolDown > attackCoolDown)
        {
            hitbox.gameObject.SetActive(true);
            currentAttackCoolDown = 0f;
        }
    }

    IEnumerator HitWithDelay()
    {
        knifeAnimator.Play("KnifeHit");
        yield return new WaitForSeconds(attackDelay);
        Hit();
    }

    // Update is called once per frame
    void Update()
    {
        currentAttackDelay += Time.deltaTime;
        currentAttackCoolDown += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            HitWithDelay();
        }
    }
}
