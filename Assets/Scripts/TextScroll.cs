using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScroll : MonoBehaviour
{
    [SerializeField] GameObject dialogue;
    [SerializeField] Image portrait;
    [SerializeField] Text textMessage;

    public delegate void DialogueHandler();
    public event DialogueHandler ExecFunc;

    List<string> messagesList;

    float letterAppearTime = 0.03f;
    float timePassed = 0f;

    int textCursor = 0;
    int listIndex = 0;

    string messageText = "";

    bool canMoveDuringDialogue = false;
    bool currMessCompleted = false;
    bool play = false;

    public bool Play
    {
        get { return play; }
    }

    public void fillPlayDial(List<string> list, bool movable, Sprite spriteIm)
    {
        dialogue.SetActive(true);
        messagesList = list;
        messageText = messagesList[0];
        canMoveDuringDialogue = movable;
        //if (!canMoveDuringDialogue)
        //    Playerscript.instance.allowControl = false;
        play = true;
        if (spriteIm != null)
        {
            portrait.sprite = spriteIm;
        }
    }
    void LateUpdate()
    {
        if (play)
            DrawText();
    }

    public void DrawText()
    {
        if (Input.GetKeyDown("space") && textCursor > 0)
        {
            if (currMessCompleted)
            {
                if (listIndex >= messagesList.Count - 1) // If no messages left, hide
                {
                    play = false;
                    textCursor = 0;
                    currMessCompleted = false;
                    textMessage.text = "";
                    dialogue.SetActive(false);
                    listIndex = 0;
                    // Playerscript.instance.allowControl = true;
                    ExecFunc?.Invoke();
                    ExecFunc = null;
                    return;
                }
                else
                {
                    // Reset vars before next message
                    listIndex++;
                    messageText = messagesList[listIndex];
                    textMessage.text = "";
                    textCursor = 0;
                    currMessCompleted = false;
                }
            }
            else
            {
                currMessCompleted = true;
                timePassed = 0f;
            }
        }

        timePassed += Time.deltaTime;
        // Time to draw next letter
        if (timePassed > letterAppearTime && textCursor < messageText.Length && !currMessCompleted)
        {
            textMessage.text += messageText.Substring(textCursor, 1);
            textCursor++;
            timePassed = 0f;
        } // Message completed drawing
        else if (textCursor >= messageText.Length && !currMessCompleted)
        {
            currMessCompleted = true;
        } // Message skipped
        else if (currMessCompleted && textCursor < messageText.Length)
        {
            textMessage.text = messagesList[listIndex];
        }
    }
}
