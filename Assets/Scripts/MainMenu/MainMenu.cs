using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GM;
using DG.Tweening;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Image blackImage;
    [SerializeField] GameObject OptionsPanel;
    [SerializeField] GameObject menuPanel;
    [SerializeField] Canvas blackImageCanvas;

    private void Awake()
    {
        // gameObject.SetActive(true);
    }
    public void StartGame()
    {
        blackImageCanvas.sortingOrder = 1;
        blackImage.DOFade(1f, 3f).onComplete = () => {
            SceneManager.LoadScene("Prison");
        };
    }

    public void OpenOptions()
    {
        OptionsPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void CloseOptions()
    {
        OptionsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
