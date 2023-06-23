using UnityEngine;
using DG.Tweening;
using TMPro;

public class TextShow : MonoBehaviour
{
    private Sequence sequence;
    private bool isTextDisplayed = false;
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void ResetText()
    {
        text.enabled = false;
        text.DOFade(1, 0f);
        isTextDisplayed = false;
    }

    public void DisplayText(ScriptableMes mes)
    {
        if (!isTextDisplayed)
        {
            sequence = DOTween.Sequence();
            isTextDisplayed = true;
            text.text = mes.MessageText[0];
            text.enabled = true;

            sequence.PrependInterval(2f).Append(text.DOFade(0, 0.5f));
            sequence.onComplete = ResetText;
        }
    }
}
