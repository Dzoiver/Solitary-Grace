using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    [SerializeField] PlayerScript player;
    [SerializeField] WeaponManager weapons;
    RemoveOnStart[] lights;
    // public GameObject globalLight;
    DaytimeOutside daytimeManager;
    SceneView lastActiveSceneView;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            player.ToggleNoclip();
            
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            weapons.GiveAllWeapons();
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            daytimeManager.SetDay(!daytimeManager.dayTime);
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            foreach (RemoveOnStart light in lights)
            {
                light.ActivateLights();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.F6))
        {
            GameFuncs.TeleportPlayerNoRotate(lastActiveSceneView.camera.gameObject);
        }
        

     }

    private void Awake()
    {
        daytimeManager = FindObjectOfType<DaytimeOutside>();
        lights = FindObjectsOfType<RemoveOnStart>();
        lastActiveSceneView = SceneView.lastActiveSceneView;
    }
}
