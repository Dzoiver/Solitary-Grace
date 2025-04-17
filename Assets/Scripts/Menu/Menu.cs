using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;
using DG.Tweening;
using SolitaryAudio;
using UnityEngine.Events;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject systemPanel;
    [SerializeField] GameObject confirmPanel;
    private bool fading = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenMenu()
    {
        fading = true;
        GameFuncs.PlayerScript.SetControl(false);
        AudioController.Play("openMenu");
        Cursor.lockState = CursorLockMode.None;
        GameFuncs.BlackImage.DOColor(new Color(0, 0, 0, 1), 0.5f).onComplete = () => // Fadeout
        {
            menuPanel.SetActive(true);
            // Fadein
            GameFuncs.BlackImage.DOColor(new Color(0, 0, 0, 0), 0.5f).onComplete = () =>
            {
                fading = false;
            };
        };
    }

    public void CloseMenu()
    {
        fading = true;
        AudioController.Play("closeMenu");
        Cursor.lockState = CursorLockMode.Locked;
        GameFuncs.BlackImage.DOColor(new Color(0, 0, 0, 1), 0.5f).onComplete = () => // Fadeout
        {
            confirmPanel.SetActive(false);
            systemPanel.SetActive(false);
            menuPanel.SetActive(false);

            // Fadein
            GameFuncs.PlayerScript.SetControl(true);
            GameFuncs.BlackImage.DOColor(new Color(0, 0, 0, 0), 0.5f).onComplete = () =>
            {
                fading = false;
            };
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (fading)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuPanel.activeSelf && GameFuncs.PlayerScript.IsControl())
            {
                OpenMenu();
            }
            else
            {
                CloseMenu();
            }
        }
    }
}
