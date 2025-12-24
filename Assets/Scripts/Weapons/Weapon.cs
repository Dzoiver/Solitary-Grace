using GM;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI canvasText;
    [SerializeField] public AudioSource audio;
    public Inventory inventory;
    public abstract int clipAmmo { get; set; }
    public abstract int currentClip { get; set; }
    public abstract int reserveAmmo { get; set; }
    public abstract int maxAmmo { get;}
    public abstract float coolDown { get;}
    public abstract float currentCoolDown { get; set; }
    public abstract Animator weaponAnimator { get; set; }
    public abstract string ReloadAnimName { get; set; }

    public abstract string ShootAnimName { get; set; }
    public abstract string ReloadSound { get; set; }
    public abstract string ShootSound { get; set; }

    public abstract string EmptySound { get; set; }

    private bool reloading = false;

    private int weaponID = 6; // Pistol

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
    private void Shoot()
    {
        if (!GameFuncs.PlayerScript.IsControl()) // Can't shoot if menu is opened
            return;
        if (currentClip > 0)
        {
            audio.PlayOneShot(Resources.Load<AudioClip>(ShootSound));
            currentClip -= 1;
            canvasText.text = currentClip.ToString() + " / " + GameFuncs.inventory.ItemAmount(weaponID).ToString();
            weaponAnimator.Play(ShootAnimName, -1, 0f);
            SpawnBullets();
        }
        else
        {
            audio.PlayOneShot(Resources.Load<AudioClip>(EmptySound));
        }
    }

    public abstract void SpawnBullets();

    private void Reload()
    {
        if (reloading)
            return;

        if (name == "Shotgun")
            weaponID = 7;


        if (currentClip < clipAmmo && GameFuncs.inventory.ItemAmount(weaponID) > 0)
        {
            audio.PlayOneShot(Resources.Load<AudioClip>(ReloadSound));
            reloading = true;
            weaponAnimator.Play(ReloadAnimName, -1, 0f);
        }
    }
    
    public void FinishReloading()
    {
        int neededAmmo = clipAmmo - currentClip;
        int inventoryAmmo = GameFuncs.inventory.ItemAmount(weaponID);
        if (inventoryAmmo >= neededAmmo) // Enough ammo to reload fully
        {
            //inventory.DeleteItem(6, neededAmmo);
            GameFuncs.inventory.DecreaseCount(neededAmmo, weaponID);
            currentClip += neededAmmo;
        }
        else // not enough
        {
            currentClip += inventoryAmmo;
            GameFuncs.inventory.DecreaseCount(999, weaponID);
            //inventory.DeleteItem(6, 9999);
        }
        canvasText.text = currentClip.ToString() + " / " + GameFuncs.inventory.ItemAmount(weaponID).ToString();
        reloading = false;
    }

    public void FinishShooting()
    {

    }
}
