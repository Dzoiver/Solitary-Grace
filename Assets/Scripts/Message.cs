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

    Sequence seq;
    bool isTextDisplayed = false;
    private void Start()
    {
        seq = DOTween.Sequence();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isTextDisplayed && other.gameObject.name == "UseCube")
        {
            isTextDisplayed = true;
            text.text = mes.MessageText[0];
            text.enabled = true;
            seq.PrependInterval(2f).Append(text.DOFade(0, 0.5f));
            seq.onComplete = ResetText;
        }
    }

    void ResetText()
    {
        text.enabled = false;
        text.DOFade(1, 0f);
        isTextDisplayed = false;
    }
}
