using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace GM
{
    public class GameFuncs : MonoBehaviour
    {
        static public PlayerScript PlayerScript;
        static public Image BlackImage;
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
            PlayerScript.gameObject.transform.position = destination.transform.position;
            PlayerScript.controller.enabled = true;
        }

        static public void LowerObject(GameObject object1, Vector3 endValue)
        {
            
        }
    }
}
