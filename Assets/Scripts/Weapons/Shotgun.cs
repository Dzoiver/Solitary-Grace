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
    private int _currentClip = 5;
    private int _reserveAmmo = 49;
    private int _maxAmmo = 49;
    private float _coolDown = 0.3f;
    private float _currentCoolDown = 0f;

    Animator _weaponAnimator;
    public override int clipAmmo { get => _clipAmmo; set => _clipAmmo = value; }
    public override int currentClip { get => _currentClip; set => _currentClip = value; }
    public override int reserveAmmo { get => _reserveAmmo; set => _reserveAmmo = value; }

    public override int maxAmmo => _maxAmmo;

    public override float coolDown => _coolDown;

    public override float currentCoolDown { get => _currentCoolDown; set => _currentCoolDown = value; }

    public override Animator weaponAnimator { get => _weaponAnimator; set => _weaponAnimator = value; }

    private void Start()
    {
        canvasText.text = currentClip.ToString() + " / " + reserveAmmo.ToString();
        _weaponAnimator = GetComponent<Animator>();
        pManager = FindObjectOfType<ProjectilesManager>();

    }
    public override void SpawnBullets()
    {
        rotationBullet.x = Camera.main.transform.rotation.eulerAngles.x;
        rotationBullet.y = GameFuncs.PlayerScript.transform.rotation.eulerAngles.y;
        var bullet = pManager.GetNewBullet();
        var bullet2 = pManager.GetNewBullet();
        var bullet3 = pManager.GetNewBullet();
        var bullet4 = pManager.GetNewBullet();
        var bullet5 = pManager.GetNewBullet();
        bullet.transform.position = bulletStart.transform.position;
        bullet.Launch(-transform.right, rotationBullet, true);
        bullet2.transform.position = bulletStart.transform.position;
        bullet2.Launch(-transform.right, rotationBullet, true);
        bullet3.transform.position = bulletStart.transform.position;
        bullet3.Launch(-transform.right, rotationBullet, true);
        bullet4.transform.position = bulletStart.transform.position;
        bullet4.Launch(-transform.right, rotationBullet, true);
        bullet5.transform.position = bulletStart.transform.position;
        bullet5.Launch(-transform.right, rotationBullet, true);
    }
}
