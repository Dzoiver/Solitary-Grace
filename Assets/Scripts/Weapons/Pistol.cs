using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    ProjectilesManager pManager;
    Vector3 rotationBullet;
    [SerializeField] GameObject bulletStart;
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
    private int _currentClip = 7;
    private int _reserveAmmo = 49;
    private int _maxAmmo = 49;
    private float _coolDown = 0.3f;
    private float _currentCoolDown = 0f;
    Animator WeaponAnimator;
    private string reloadSound = "Sounds/pistolReload";
    private string shootSound = "Sounds/pistolShot";

    public override int clipAmmo { get => _clipAmmo; set => _clipAmmo = value; }

    public override int currentClip { get => _currentClip; set => _currentClip = value; }

    public override int reserveAmmo { get => _reserveAmmo; set => _reserveAmmo = value; }

    public override int maxAmmo => _maxAmmo;

    public override float coolDown => _coolDown;

    public override float currentCoolDown { get => _currentCoolDown; set => _currentCoolDown = value; }

    public override Animator weaponAnimator { get => WeaponAnimator; set => WeaponAnimator = value; }
    public override string ReloadSound { get => reloadSound; set => reloadSound = value; }
    public override string ShootSound { get => shootSound; set => shootSound = value; }

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        canvasText.text = currentClip.ToString() + " / " + reserveAmmo.ToString();
        WeaponAnimator = GetComponent<Animator>();
        pManager = FindObjectOfType<ProjectilesManager>();
    }
    public override void SpawnBullets()
    {
        rotationBullet.x = Camera.main.transform.rotation.eulerAngles.x;
        rotationBullet.y = GameFuncs.PlayerScript.transform.rotation.eulerAngles.y;
        var bullet = pManager.GetNewBullet();
        bullet.transform.position = bulletStart.transform.position;
        bullet.Launch(-transform.right, rotationBullet);
    }
}
