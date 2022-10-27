using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Message : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] ScriptableMes mes;
    Sequence seq;
    bool playing = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
