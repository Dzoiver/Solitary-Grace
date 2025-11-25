using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    [SerializeField] PlayerScript player;
    [SerializeField] WeaponManager weapons;
    RemoveOnStart[] lights;
    // public GameObject globalLight;
    DaytimeOutside daytimeManager;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F1))
        {
            player.ToggleNoclip();
            
        }

        if (Input.GetKeyUp(KeyCode.F3))
        {
            weapons.GiveAllWeapons();
        }

        if (Input.GetKeyUp(KeyCode.F4))
        {
            daytimeManager.SetDay(!daytimeManager.dayTime);
        }

        if (Input.GetKeyUp(KeyCode.F5))
        {
            foreach (RemoveOnStart light in lights)
            {
                light.ActivateLights();
            }
        }
    }

    private void Awake()
    {
        daytimeManager = FindObjectOfType<DaytimeOutside>();
        lights = FindObjectsOfType<RemoveOnStart>();

    }
}
