using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FattyTalk : MonoBehaviour
{
    [Inject] DialogueManager dManager;
    [SerializeField] string[] texts;
    int index = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "UseCube" || dManager.IsDialoguePlayed())
            return;
        //other.gameObject.SetActive(false);
        dManager.SetDialogue(texts[index]);
        dManager.PlayDialogue();
        if (index < texts.Length - 1)
            index++;
    }
}
