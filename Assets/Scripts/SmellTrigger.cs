using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextPrint;

public class SmellTrigger : MonoBehaviour
{
    [SerializeField] ScriptableMes message;
    private bool triggeredOnce = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggeredOnce)
            return;
        triggeredOnce = true;

        TextDisplay.DisplayText(message);
    }


}
