using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Knife : MonoBehaviour
{
    [SerializeField] private float damage = 20f;
    [SerializeField] private float attackCoolDown = 0.7f;
    private float currentAttackCoolDown = 0f;
    private float attackDelay = 0.25f;
    private float currentAttackDelay = 0f;
    [SerializeField] private KnifeHitbox hitbox;
    [SerializeField] private Animator knifeAnimator;
    AudioSource audio;

    public float GetDamageValue()
    {
        return damage;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        audio.PlayOneShot(Resources.Load<AudioClip>("Sounds/SwordEquip"));
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
        if (currentAttackCoolDown > attackCoolDown)
        {
            currentAttackCoolDown = 0f;
            knifeAnimator.Play("KnifeHit5", -1, 0f);
            yield return new WaitForSeconds(attackDelay);
            hitbox.gameObject.SetActive(true);
        }
    }

    public void KnifeSound(AudioClip clip)
    {
        audio.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        currentAttackDelay += Time.deltaTime;
        currentAttackCoolDown += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0) && GameFuncs.weaponManager.canAttack)
        {
            StartCoroutine(HitWithDelay());
        }
    }
}
