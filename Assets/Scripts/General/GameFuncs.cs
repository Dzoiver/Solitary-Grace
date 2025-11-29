using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering.LookDev;

namespace GM
{
    public class GameFuncs : MonoBehaviour
    {
        static public PlayerScript PlayerScript;
        static public MouseLook mouseLook;
        static public Image BlackImage;
        static public WeaponManager weaponManager;
        static public bool fading = false;
        static public void LampsChangeColor(Light[] lights, Color endColor)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].color = endColor;
            }
        }

        static public void TeleportPlayer(GameObject destination)
        {
            PlayerScript.controller.enabled = false;
            mouseLook.CenterView();
            PlayerScript.gameObject.transform.position = destination.transform.position;
            PlayerScript.gameObject.transform.rotation = destination.transform.rotation;
            PlayerScript.controller.enabled = true;
        }

        static public void TeleportRelatively(GameObject relativeObject, Vector3 relativeNewPos)
        {
            PlayerScript.controller.enabled = false;
            Vector3 relativeVector = relativeObject.transform.position - PlayerScript.gameObject.transform.position;
            PlayerScript.gameObject.transform.position = (relativeNewPos - relativeVector);
            PlayerScript.controller.enabled = true;
        }

        static public void TeleportPlayer(Vector3 destinationVector, Quaternion rot)
        {
            PlayerScript.controller.enabled = false;
            mouseLook.CenterView();
            PlayerScript.gameObject.transform.position = destinationVector;
            PlayerScript.gameObject.transform.rotation = rot;
            PlayerScript.controller.enabled = true;
        }

        static public void FadeIn(float time = 0.5f)
        {
            BlackImage.DOColor(new Color(0, 0, 0, 1), time);
        }

        static public void FadeInWhite(float time = 0.5f)
        {
            BlackImage.DOColor(new Color(1, 1, 1, 1), time);
        }

        static public void FadeOut(float time = 0.5f)
        {
            //BlackImage.color = new Color(1f, 1f, 1f);
            BlackImage.DOColor(new Color(0, 0, 0, 1f), 0f);
            BlackImage.DOColor(new Color(0, 0, 0, 0), time);
        }
        static public void LowerObject(GameObject object1, Vector3 endValue)
        {
            
        }

        static public void DisableWeapons(bool newValue = false)
        {
            weaponManager.SetUsable(!newValue);
        }
    }
}
