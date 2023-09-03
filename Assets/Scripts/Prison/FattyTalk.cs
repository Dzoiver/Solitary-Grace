using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FattyTalk : MonoBehaviour
{
    [Inject] DialogueManager dManager;
    [SerializeField] ScriptableMes lines;
    bool firstTime = true;
    bool secondTime = true;
    bool thirdTime = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "UseCube" || dManager.IsDialoguePlayed())
            return;

        if (firstTime)
        {
            firstTime = false;
            dManager.SetDialogue(lines);
            dManager.PlayDialogue(0);
            // Play first dialogue
        }
        else if (secondTime)
        {
            dManager.PlayDialogue(1);
            secondTime = false;
        }
        else if (thirdTime)
        {
            dManager.PlayDialogue(2);
            thirdTime = false;
        }
        else
        {
            dManager.PlayDialogue(3);
        }
    }
}
