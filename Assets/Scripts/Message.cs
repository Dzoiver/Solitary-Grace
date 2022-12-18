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
    bool playing = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!playing)
        {
            playing = true;
            text.text = mes.MessageText[0];
            text.enabled = true;
            seq = DOTween.Sequence();
            seq.PrependInterval(2f);
            seq.Append(text.DOFade(0, 0.5f));
            seq.onComplete = ResetText;
        }
    }

    void ResetText()
    {
        text.enabled = false;
        text.DOFade(1, 0f);
        playing = false;
    }
}
