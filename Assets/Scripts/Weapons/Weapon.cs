using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private int clipAmmo = 5;
    private int currentClip = 5;
    private int reserveAmmo = 0;
    private int maxAmmo = 30;
    private float coolDown = 0.5f;
    private float currentCoolDown = 0f;
    private Animator weaponAnimator;

    private bool reloading = false;

    void Start()
    {
        
    }

    private void Shoot()
    {
        clipAmmo -= 1;
        weaponAnimator.Play("Shoot");
    }

    private void Reload()
    {
        if (reloading)
            return;

        if (currentClip < clipAmmo && reserveAmmo > 0)
        {
            reloading = true;
            weaponAnimator.Play("Reload");
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
