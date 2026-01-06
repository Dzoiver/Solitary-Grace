using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] LayerMask layermask;
    ProjectilesManager pManager;
    Vector3 rotationBullet;
    [SerializeField] GameObject bulletStart;
    [SerializeField] ParticleSystem shootEffect;
    string reloadAnimName = "pistolReload2";
    public override string ReloadAnimName
    {
        get => reloadAnimName;
        set => reloadAnimName = value;
    }

    string shootAnimName = "pistolShoot";
    public override string ShootAnimName
    {
        get => shootAnimName;
        set => shootAnimName = value;
    }

    private int _clipAmmo = 7;
    private int _currentClip = 0;
    private int _reserveAmmo = 0;
    private int _maxAmmo = 999;
    private float _coolDown = 0.28f;
    private float _currentCoolDown = 0f;
    Animator WeaponAnimator;
    private string reloadSound = "Sounds/pistolReload";
    private string shootSound = "Sounds/pistolShot";
    private string emptySound = "Sounds/pistol_click";

    public override int clipAmmo { get => _clipAmmo; set => _clipAmmo = value; }

    public override int currentClip { get => _currentClip; set => _currentClip = value; }

    public override int reserveAmmo { get => _reserveAmmo; set => _reserveAmmo = value; }

    public override int maxAmmo => _maxAmmo;

    public override float coolDown => _coolDown;

    public override float currentCoolDown { get => _currentCoolDown; set => _currentCoolDown = value; }

    public override Animator weaponAnimator { get => WeaponAnimator; set => WeaponAnimator = value; }
    public override string ReloadSound { get => reloadSound; set => reloadSound = value; }
    public override string ShootSound { get => shootSound; set => shootSound = value; }

    public override string EmptySound { get => emptySound; set => emptySound = value; }

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        canvasText.text = currentClip.ToString() + " / " + reserveAmmo.ToString();
        
        pManager = FindObjectOfType<ProjectilesManager>();
    }

    private void Awake()
    {
        WeaponAnimator = GetComponent<Animator>();
        WeaponAnimator.keepAnimatorStateOnDisable = false;
    }

    public override void SpawnBullets()
    {
        shootEffect.Play();
        rotationBullet.x = Camera.main.transform.rotation.eulerAngles.x;
        rotationBullet.y = GameFuncs.PlayerScript.transform.rotation.eulerAngles.y;
        var bullet = pManager.GetNewBullet();
        bullet.transform.position = bulletStart.transform.position;
        Transform bulletSpawnTransform = bulletStart.transform;
        
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit, 200f, layermask))
        {
            targetPoint = hit.point; // If the ray hits something, shoot at that point
            Vector3 bulletDirection = (targetPoint - bulletSpawnTransform.position).normalized;

            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<Monster>().GetDamage(bullet.Damage);
            }
            if (hit.collider.CompareTag("Boss"))
            {
                hit.collider.gameObject.GetComponent<Boss>().GetDamage(bullet.Damage);
            }
            bullet.Launch(bulletDirection, rotationBullet);
        }
        else
        {
            // If the ray doesn't hit anything, shoot a certain distance forward
            targetPoint = ray.origin + ray.direction * 100f; // 100f is an example distance
            Vector3 bulletDirection = (targetPoint - bulletSpawnTransform.position).normalized;
            bullet.Launch(bulletDirection, rotationBullet);
        }
    }

    public void UpdateAmmoFromInventory()
    {
        reserveAmmo = GameFuncs.inventory.ItemAmount(6);
        canvasText.text = currentClip.ToString() + " / " + reserveAmmo.ToString();
    }
}
