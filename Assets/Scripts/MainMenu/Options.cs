using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;
using DG.Tweening;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] GameObject optionsPanel;
    [SerializeField] GameObject menuPanel;
    [SerializeField] Image blackImage;
    private void Awake()
    {
        if (gameObject.name == "OptionsPanel")
        {
            // gameObject.SetActive(false);
        }
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
}
