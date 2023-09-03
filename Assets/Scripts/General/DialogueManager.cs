using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GM;
using TMPro;
using DG.Tweening;

public class DialogueManager : MonoBehaviour
{
    private bool isTextDisplayed = false;
    private Sequence sequence;
    private ScriptableMes dialogue;
    [SerializeField] TextMeshProUGUI text;


    public bool IsDialoguePlayed()
    {
        return isTextDisplayed;
    }
    
    public void SetDialogue(ScriptableMes mes)
    {
        Debug.Log(mes);
        dialogue = mes;
        
    }

    private void ResetText()
    {
        text.enabled = false;
        text.DOFade(1, 0f);
        isTextDisplayed = false;
    }

    public bool PlayDialogue(int dialogueID)
    {
        if (dialogue == null || isTextDisplayed == true)
        {
            return true;
        }

        sequence = DOTween.Sequence();
        text.text = dialogue.MessageText[dialogueID];
        text.enabled = true;
        isTextDisplayed = true;

        sequence.PrependInterval(2f).Append(text.DOFade(0, 0.5f));
        sequence.onComplete = ResetText;

        return true;
    }
}
