using GM;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shotgun : Weapon
{
    ProjectilesManager pManager;
    [SerializeField] ParticleSystem shotEffect;
    [SerializeField] GameObject bulletStart;
    Vector3 rotationBullet;
    string reloadAnimName = "shutgunReload";
    public override string ReloadAnimName
    {
        get => reloadAnimName;
        set => reloadAnimName = value;
    }

    string shootAnimName = "shotgunShoot";
    public override string ShootAnimName
    {
        get => shootAnimName;
        set => shootAnimName = value;
    }

    private int _clipAmmo = 5;
    private int _currentClip = 0;
    private int _reserveAmmo = 0;
    private int _maxAmmo = 999;
    private float _coolDown = 0.3f;
    private float _currentCoolDown = 0f;
    private string reloadSound = "Sounds/ShotgunReload";
    private string shootSound = "Sounds/ShotgunShoot";
    private string emptySound = "Sounds/drobo_click";
    float randomness = 0.07f;

    Animator _weaponAnimator;
    public override int clipAmmo { get => _clipAmmo; set => _clipAmmo = value; }
    public override int currentClip { get => _currentClip; set => _currentClip = value; }
    public override int reserveAmmo { get => _reserveAmmo; set => _reserveAmmo = value; }

    public override int maxAmmo => _maxAmmo;

    public override float coolDown => _coolDown;

    public override float currentCoolDown { get => _currentCoolDown; set => _currentCoolDown = value; }

    public override string ReloadSound { get => reloadSound; set => reloadSound = value; }
    public override string ShootSound { get => shootSound; set => shootSound = value; }

    public override string EmptySound { get => emptySound; set => emptySound = value; }

    public override Animator weaponAnimator { get => _weaponAnimator; set => _weaponAnimator = value; }

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        canvasText.text = currentClip.ToString() + " / " + reserveAmmo.ToString();
        pManager = FindObjectOfType<ProjectilesManager>();
    }

    private void Awake()
    {
        _weaponAnimator = GetComponent<Animator>();
    }

    // ShotgunReload
    public override void SpawnBullets()
    {
        shotEffect.Play();
        rotationBullet.x = Camera.main.transform.rotation.eulerAngles.x;
        rotationBullet.y = GameFuncs.PlayerScript.transform.rotation.eulerAngles.y;
        var bullet2 = pManager.GetNewBullet();
        var bullet3 = pManager.GetNewBullet();
        var bullet4 = pManager.GetNewBullet();
        var bullet5 = pManager.GetNewBullet();

        Transform bulletSpawnTransform = bulletStart.transform;

        Ray ray;
        RaycastHit hit;
        Vector3 targetPoint;
        Vector3 bulletDirection;

        for (int i = 0; i < 5; i++)
        {
            ray = Camera.main.ViewportPointToRay(new Vector3(0.5f + UnityEngine.Random.Range(-randomness, randomness),
            0.5f + UnityEngine.Random.Range(-randomness, randomness), 0));

            if (Physics.Raycast(ray, out hit))
            {
                targetPoint = hit.point;
                bulletDirection = (targetPoint - bulletSpawnTransform.position).normalized;

                var bullet = pManager.GetNewBullet();
                if (hit.collider.CompareTag("Enemy"))
                {
                    hit.collider.gameObject.GetComponent<Monster>().GetDamage(bullet.MaxDamage);
                }
                if (hit.collider.CompareTag("Boss"))
                {
                    hit.collider.gameObject.GetComponent<Boss>().GetDamage(bullet.MaxDamage);
                }

                bullet.transform.position = bulletStart.transform.position;
                bullet.Launch(bulletDirection, rotationBullet);
            }
        }
    }

    public void UpdateAmmoFromInventory()
    {
        reserveAmmo = GameFuncs.inventory.ItemAmount(7);
        canvasText.text = currentClip.ToString() + " / " + reserveAmmo.ToString();
    }
}
