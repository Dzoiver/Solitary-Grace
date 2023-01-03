using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GM
{
    public class GameFuncs : MonoBehaviour
    { 
        static public void LampsChangeColor(Light[] lights, Color endColor)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].color = endColor;
            }
        }

        static public void TeleportPlayer()
        {

        }

        static public void LowerObject(GameObject object1, Vector3 endValue)
        {
            
        }
    }
}
