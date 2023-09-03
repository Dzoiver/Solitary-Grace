using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;
using DG.Tweening;
using SolitaryAudio;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    private bool fading = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameFuncs.PlayerScript.IsControl() || fading)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuPanel.activeSelf)
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
                    GameFuncs.PlayerScript.SetControl(true);
                };
            }
            else
            {
                fading = true;
                GameFuncs.PlayerScript.SetControl(true);
                AudioController.Play("closeMenu");
                Cursor.lockState = CursorLockMode.Locked;
                GameFuncs.BlackImage.DOColor(new Color(0, 0, 0, 1), 0.5f).onComplete = () => // Fadeout
                {
                    menuPanel.SetActive(false);
                    // Fadein
                    GameFuncs.BlackImage.DOColor(new Color(0, 0, 0, 0), 0.5f).onComplete = () => 
                    {
                        fading = false;
                    }; 
                };
                
            }
        }
    }
}
