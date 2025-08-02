using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;
using DG.Tweening;
using SolitaryAudio;
using UnityEngine.Events;
using static UnityEditor.Progress;
using Zenject;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject systemPanel;
    [SerializeField] GameObject confirmPanel;
    [SerializeField] GameObject confirmPanelItem;
    [Inject] Inventory inventory;
    ScriptableItem bufferItem;
    private bool fading = false;
    // Start is called before the first frame update
    void Start()
    {
        menuPanel.SetActive(false);
        confirmPanelItem.SetActive(false);
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

    public void ConfirmBox(ScriptableItem item)
    {
        bufferItem = item; // Buffers item which player decides to take or not
        OpenMenu();
        confirmPanelItem.SetActive(true);
        confirmPanelItem.GetComponent<ConfirmationBox>().ChangeItemName(item.name);
    }

    public void YesConfirm()
    {
        inventory.AddItem(bufferItem);
        CloseMenu();
    }

    public void NoConfirm()
    {

        CloseMenu();
    }

    public void CloseMenu()
    {
        confirmPanelItem.SetActive(false);
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
