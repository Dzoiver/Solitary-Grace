using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace TextPrint
{
    public class TextDisplay
    {
        static private Sequence sequence;
        static private bool isTextDisplayed = false;
        static private TextMeshProUGUI text;

        public static void InitText(TextMeshProUGUI textPro)
        {
            text = textPro;
        }

        public static void ResetText()
        {
            text.enabled = false;
            text.DOFade(1, 0f);
            isTextDisplayed = false;
        }

        public static void DisplayText(ScriptableMes mes)
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
}
