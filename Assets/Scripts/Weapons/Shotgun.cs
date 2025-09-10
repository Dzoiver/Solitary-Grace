using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    string reloadAnimName = "shotgunReload";
    public override string ReloadAnimName
    {
        get => reloadAnimName;
        set => reloadAnimName = value;
    }

    string shootAnimName = "shotgunReload";
    public override string ShootAnimName
    {
        get => shootAnimName;
        set => shootAnimName = value;
    }
    public override int clipAmmo { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override int currentClip { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override int reserveAmmo { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public override int maxAmmo => throw new System.NotImplementedException();

    public override float coolDown => throw new System.NotImplementedException();

    public override float currentCoolDown { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}
