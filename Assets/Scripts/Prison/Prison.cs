using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;

public class Prison : MonoBehaviour
{
    private void Start()
    {
        if (GameFuncs.PlayerScript == null)
        {
            gameObject.SetActive(false);
            return;
        }

        if (Vector3.Distance(GameFuncs.PlayerScript.GetTransform().position, gameObject.transform.position) > 100f)
        {
            gameObject.SetActive(false);
        }
    }
}
