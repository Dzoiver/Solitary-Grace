using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;

public class Prison : MonoBehaviour
{
    [SerializeField] bool hiddenIfPlayerFar = true;
    private void Start()
    {
        if (!hiddenIfPlayerFar)
        {
            return;
        }
        if (GameFuncs.PlayerScript == null)
        {
            gameObject.SetActive(false);
            return;
        }

        if (Vector3.Distance(GameFuncs.PlayerScript.transform.position, gameObject.transform.position) > 100f)
        {
            gameObject.SetActive(false);
        }
    }
}
