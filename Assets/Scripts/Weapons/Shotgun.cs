using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    ProjectilesManager pManager;
    Vector3 rotationBullet;
    string reloadAnimName = "shotgunReload";
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
    private int _currentClip = 7;
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
        _weaponAnimator = GetComponent<Animator>();
    }
    public override void SpawnBullets()
    {
        rotationBullet.x = Camera.main.transform.rotation.eulerAngles.x;
        rotationBullet.y = GameFuncs.PlayerScript.transform.rotation.eulerAngles.y;
        var bullet = pManager.GetNewBullet();
        bullet.transform.position = transform.position;
        bullet.Launch(-transform.right, rotationBullet);
    }
}
