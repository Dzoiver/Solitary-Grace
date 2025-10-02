using GM;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI canvasText;
    public abstract int clipAmmo { get; set; }
    public abstract int currentClip { get; set; }
    public abstract int reserveAmmo { get; set; }
    public abstract int maxAmmo { get;}
    public abstract float coolDown { get;}
    public abstract float currentCoolDown { get; set; }
    public abstract Animator weaponAnimator { get; set; }
    public abstract string ReloadAnimName { get; set; }

    public abstract string ShootAnimName { get; set; }

    private bool reloading = false;

    void Start()
    {
        canvasText.text = currentClip.ToString() + " / " + reserveAmmo.ToString();
    }

    private void Shoot()
    {
        if (!GameFuncs.PlayerScript.IsControl()) // Can't shoot if menu is opened
            return;

        if (currentClip > 0)
        {
            currentClip -= 1;
            canvasText.text = currentClip.ToString() + " / " + reserveAmmo.ToString();
            Debug.Log(weaponAnimator);
            weaponAnimator.Play(ShootAnimName, -1, 0f);
            SpawnBullets();
        }
    }

    public abstract void SpawnBullets();

    private void Reload()
    {
        if (reloading)
            return;

        if (currentClip < clipAmmo && reserveAmmo > 0)
        {
            reloading = true;
            weaponAnimator.Play(ReloadAnimName, -1, 0f);
        }
    }

    public void AddAmmo(int value)
    {
        reserveAmmo += value;
    }

    public void RemoveAmmo(int value)
    {
        reserveAmmo -= value;
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
        canvasText.text = currentClip.ToString() + " / " + reserveAmmo.ToString();
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
