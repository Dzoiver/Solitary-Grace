using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract int clipAmmo { get; set; }
    public abstract int currentClip { get; set; }
    public abstract int reserveAmmo { get; set; }
    public abstract int maxAmmo { get;}
    public abstract float coolDown { get;}
    public abstract float currentCoolDown { get; set; }
    private Animator weaponAnimator;
    public abstract string ReloadAnimName { get; set; }

    public abstract string ShootAnimName { get; set; }

    private bool reloading = false;

    void Start()
    {
        weaponAnimator = GetComponent<Animator>();
    }

    private void Shoot()
    {
        clipAmmo -= 1;
        weaponAnimator.Play(ShootAnimName);
    }

    private void Reload()
    {
        if (reloading)
            return;

        if (currentClip < clipAmmo && reserveAmmo > 0)
        {
            reloading = true;
            weaponAnimator.Play(ReloadAnimName);
        }
    }

    public void AddAmmo()
    {
        reserveAmmo += clipAmmo;
    }
    
    public void FinishReloading()
    {
        int neededAmmo = clipAmmo - currentClip;
        if (reserveAmmo >= neededAmmo) // Enough ammo to reload fully
        {
            reserveAmmo -= neededAmmo;
            currentClip += neededAmmo;
        }
        else // not enough
        {
            currentClip += reserveAmmo;
            reserveAmmo = 0;
        }

        reloading = false;
    }

    public void FinishShooting()
    {

    }

    void Update()
    {
        currentCoolDown += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (currentCoolDown > coolDown && !reloading)
            {
                Shoot();
                currentCoolDown = 0f;
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            Reload();
        }
    }
}
