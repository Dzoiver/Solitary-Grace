using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class TextShow : MonoBehaviour
{
    private Sequence sequence;
    private bool isTextDisplayed = false;
    private TextMeshProUGUI text;
    public TextMeshProUGUI centerText;
    [SerializeField] Image arrow;

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
            arrow.gameObject.SetActive(true);
            sequence = DOTween.Sequence();
            isTextDisplayed = true;
            text.text = mes.MessageText;
            text.enabled = true;

            sequence.PrependInterval(3f).Append(text.DOFade(0, 0.5f));
            sequence.onComplete = ResetText;
        }
    }
}
