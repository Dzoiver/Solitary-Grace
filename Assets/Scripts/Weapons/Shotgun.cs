using GM;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shotgun : Weapon
{
    ProjectilesManager pManager;
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
        _weaponAnimator = GetComponent<Animator>();
        pManager = FindObjectOfType<ProjectilesManager>();
    }

    // ShotgunReload
    public override void SpawnBullets()
    {
        rotationBullet.x = Camera.main.transform.rotation.eulerAngles.x;
        rotationBullet.y = GameFuncs.PlayerScript.transform.rotation.eulerAngles.y;
        var bullet = pManager.GetNewBullet();
        var bullet2 = pManager.GetNewBullet();
        var bullet3 = pManager.GetNewBullet();
        var bullet4 = pManager.GetNewBullet();
        var bullet5 = pManager.GetNewBullet();

        Transform bulletSpawnTransform = bulletStart.transform;

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point; // If the ray hits something, shoot at that point
            Vector3 bulletDirection = (targetPoint - bulletSpawnTransform.position).normalized;

            bullet.transform.position = bulletStart.transform.position;
            bullet.Launch(bulletDirection, rotationBullet, true);
            bullet2.transform.position = bulletStart.transform.position;
            bullet2.Launch(bulletDirection, rotationBullet, true);
            bullet3.transform.position = bulletStart.transform.position;
            bullet3.Launch(bulletDirection, rotationBullet, true);
            bullet4.transform.position = bulletStart.transform.position;
            bullet4.Launch(bulletDirection, rotationBullet, true);
            bullet5.transform.position = bulletStart.transform.position;
            bullet5.Launch(bulletDirection, rotationBullet, true);
        }
        else
        {

            // If the ray doesn't hit anything, shoot a certain distance forward
            targetPoint = ray.origin + ray.direction * 100f; // 100f is an example distance
            Vector3 bulletDirection = (targetPoint - bulletSpawnTransform.position).normalized;

            bullet.transform.position = bulletStart.transform.position;
            bullet.Launch(bulletDirection, rotationBullet, true);
            bullet2.transform.position = bulletStart.transform.position;
            bullet2.Launch(bulletDirection, rotationBullet, true);
            bullet3.transform.position = bulletStart.transform.position;
            bullet3.Launch(bulletDirection, rotationBullet, true);
            bullet4.transform.position = bulletStart.transform.position;
            bullet4.Launch(bulletDirection, rotationBullet, true);
            bullet5.transform.position = bulletStart.transform.position;
            bullet5.Launch(bulletDirection, rotationBullet, true);
        }
    }

    public void UpdateAmmoFromInventory()
    {
        reserveAmmo = GameFuncs.inventory.ItemAmount(7);
        canvasText.text = currentClip.ToString() + " / " + reserveAmmo.ToString();
    }
}
