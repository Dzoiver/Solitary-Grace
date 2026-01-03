using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WeaponManager : MonoBehaviour
{
    Inventory inventory;
    [SerializeField] GameObject pistol;
    [SerializeField] GameObject knife;
    [SerializeField] GameObject shotgun;
    [SerializeField] ScriptableItem item1;
    [SerializeField] ScriptableItem item2;
    [SerializeField] ScriptableItem item3;
    public Pistol pistolScript;
    public Shotgun shotgunScript;
    public static bool canUseWeapon = true;
    public bool canAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        GameFuncs.weaponManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canUseWeapon)
            return;

        //if (!pistolScript.IsReloading() || !shotgunScript.IsReloading())
            //return;

        if (Input.GetKeyDown(KeyCode.Alpha1) && inventory.Has((int)ItemNames.Knife))
        {
            if (knife.activeSelf)
            {
                HideAll();
            }
            else
            {
                HideAll();
                knife.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && inventory.Has((int)ItemNames.Pistol))
        {
            if (pistol.activeSelf)
            {
                HideAll();
            }
            else
            {
                HideAll();
                pistol.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && inventory.Has((int)ItemNames.Shotgun))
        {
            if (shotgun.activeSelf)
            {
                HideAll();
            }
            else
            {
                HideAll();
                shotgun.SetActive(true);
            }
        }
    }

    private void HideAll()
    {
        pistol.SetActive(false);
        knife.SetActive(false);
        shotgun.SetActive(false);
    }

    public void GiveAllWeapons()
    {
        inventory.TryPickup(item1);
        inventory.TryPickup(item2);
        inventory.TryPickup(item3);
    }

    public void SetUsable(bool newValue)
    {
        canUseWeapon = newValue;
        if (!newValue)
            HideAll();
    }
}
