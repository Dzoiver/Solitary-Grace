using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextPrint;
using TMPro;

public class InitialSetup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    void Start()
    {
        TextDisplay.InitText(text);
    }
}
