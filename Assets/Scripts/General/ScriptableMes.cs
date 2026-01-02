using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "Message", menuName = "ScriptableObjects/Message", order = 1)]
public class ScriptableMes : ScriptableObject
{

    public LocalizedString localizedMessageText;
    public string MessageText
    {
        get
        {
            return localizedMessageText.GetLocalizedString();
        }
    }
}
