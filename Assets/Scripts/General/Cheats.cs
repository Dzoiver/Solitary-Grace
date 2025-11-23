using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    [SerializeField] PlayerScript player;
    [SerializeField] WeaponManager weapons;
    RemoveOnStart[] lights;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F1))
        {
            player.ToggleNoclip();
            foreach (RemoveOnStart light in lights)
            {
                light.ActivateLights();
            }
        }

        if (Input.GetKeyUp(KeyCode.F3))
        {
            weapons.GiveAllWeapons();
        }
    }

    private void Awake()
    {
        lights = FindObjectsOfType<RemoveOnStart>();
    }
}
