using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextPrint;
using TMPro;
using GM;
using UnityEngine.UI;

public class InitialSetup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image blackImage;

    void Start()
    {
        TextDisplay.InitText(text);
        GameFuncs.BlackImage = blackImage;
    }
}
