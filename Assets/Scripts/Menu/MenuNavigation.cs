using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SolitaryAudio;
using UnityEngine.UI;
using DG.Tweening;

public class MenuNavigation : MonoBehaviour
{
    [SerializeField] GameObject systemPanel;
    [SerializeField] Image blackImage;
    private bool fading = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenSystem()
    {
        if (!fading)
        {
            fading = true;
            AudioController.Play("menuButton");
            blackImage.DOFade(1f, 0.5f).onComplete = () => {
                systemPanel.SetActive(true);

                blackImage.DOFade(0f, 0.5f).onComplete = () => {
                    fading = false;
                };
            };
        }
    }

    public void CloseSystem()
    {
        if (!fading)
        {
            AudioController.Play("menuButton");
            

            blackImage.DOFade(1f, 0.5f).onComplete = () => {
                systemPanel.SetActive(false);

                blackImage.DOFade(0f, 0.5f).onComplete = () => {
                    fading = false;
                };
            };
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
