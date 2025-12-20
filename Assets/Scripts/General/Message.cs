using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;

public class Message : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] string[] messageText;
    public UnityEvent onTrigger;

    private Sequence sequence;
    private bool isTextDisplayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTextDisplayed && other.gameObject.name == "UseCube")
        {
            sequence = DOTween.Sequence();
            isTextDisplayed = true;
            text.text = messageText[0];
            text.enabled = true;
            sequence.PrependInterval(5f).Append(text.DOFade(0, 0.1f));
            sequence.onComplete = ResetText;
            onTrigger.Invoke();
        }
    }

    private void ResetText()
    {
        text.enabled = false;
        text.DOFade(1, 0f);
        isTextDisplayed = false;
    }
}
