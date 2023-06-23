using UnityEngine;
using Zenject;

public class SmellTrigger : MonoBehaviour
{
    [Inject] TextShow text;
    [SerializeField] ScriptableMes message;
    private bool triggeredOnce = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggeredOnce)
            return;
        triggeredOnce = true;

        text.DisplayText(message);
    }
}
