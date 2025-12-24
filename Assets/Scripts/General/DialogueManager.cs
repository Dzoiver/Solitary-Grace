using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GM;
using TMPro;
using DG.Tweening;
using System.Security.Cryptography;

public class DialogueManager : MonoBehaviour
{
    private bool isTextDisplayed = false;
    private Sequence sequence;
    private ScriptableMes dialogue;
    string stringText = null;
    [SerializeField] TextMeshProUGUI text;


    public bool IsDialoguePlayed()
    {
        return isTextDisplayed;
    }
    
    public void SetDialogue(ScriptableMes mes)
    {
        dialogue = mes;
    }

    public void SetDialogue(string mes)
    {
        stringText = mes;
    }

    private void ResetText()
    {
        text.enabled = false;
        text.DOFade(1, 0f);
        isTextDisplayed = false;
    }

    public bool PlayDialogue(int dialogueID)
    {
        if (isTextDisplayed == true)
        {
            return true;
        }

        if (dialogue == null && stringText == null)
            return true;

        sequence = DOTween.Sequence();
        isTextDisplayed = true;
        Debug.Log("dialog = " + (dialogue == true));
        text.enabled = true;

        if (dialogue != null)
        {
            text.text = dialogue.MessageText[dialogueID];
        }
        else if (stringText != null)
        {
            text.text = stringText;
        }
        
        sequence.PrependInterval(3f).Append(text.DOFade(0, 0.1f));
        sequence.onComplete = ResetText;

        return true;
    }

    public bool PlayDialogue(string line)
    {
        if (isTextDisplayed == true)
        {
            return true;
        }

        sequence = DOTween.Sequence();
        isTextDisplayed = true;
        text.text = line;
        text.enabled = true;
        sequence.PrependInterval(3f).Append(text.DOFade(0, 0.5f));
        sequence.onComplete = ResetText;

        return true;
    }

    public bool PlayDialogue()
    {
        if (isTextDisplayed == true)
        {
            return true;
        }

        if (stringText == null)
            return true;

        sequence = DOTween.Sequence();
        isTextDisplayed = true;
        if (stringText != null)
            text.text = stringText;
        text.enabled = true;
        sequence.PrependInterval(3f).Append(text.DOFade(0, 0.5f));
        sequence.onComplete = ResetText;

        return true;
    }
}
