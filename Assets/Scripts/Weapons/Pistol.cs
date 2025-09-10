using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
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
    private float _coolDown = 0.5f;
    private float _currentCoolDown = 0f;

    public override int clipAmmo { get => _clipAmmo; set => _clipAmmo = value; }

    public override int currentClip { get => _currentClip; set => _currentClip = value; }

    public override int reserveAmmo { get => _reserveAmmo; set => _reserveAmmo = value; }

    public override int maxAmmo => _maxAmmo;

    public override float coolDown => _coolDown;

    public override float currentCoolDown { get => _currentCoolDown; set => _currentCoolDown = value; }
}
