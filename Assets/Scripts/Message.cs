using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Message : MonoBehaviour
{
    [SerializeField] bool isConfirm = false;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] ScriptableMes mes;

    private Sequence sequence;
    private bool isTextDisplayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTextDisplayed && other.gameObject.name == "UseCube")
        {
            sequence = DOTween.Sequence();
            isTextDisplayed = true;
            text.text = mes.MessageText[0];
            text.enabled = true;
            sequence.PrependInterval(2f).Append(text.DOFade(0, 0.5f));
            sequence.onComplete = ResetText;
        }
    }

    private void ResetText()
    {
        text.enabled = false;
        text.DOFade(1, 0f);
        isTextDisplayed = false;
    }
}
