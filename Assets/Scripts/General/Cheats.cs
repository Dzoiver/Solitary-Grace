using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    [SerializeField] PlayerScript player;
    [SerializeField] WeaponManager weapons;
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
    }
}
