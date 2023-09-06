using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SystemMenu : MonoBehaviour
{
    [SerializeField] Image blackImage;
    [SerializeField] GameObject confirmPanel;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject systemPanel;

    public void OpenConfirmPanel()
    {
        confirmPanel.SetActive(true);
    }

    public void CloseConfirmPanel()
    {
        confirmPanel.SetActive(false);
    }

    public void MainMenu()
    {
        blackImage.DOFade(1f, 1f).onComplete = () => {
            SceneManager.LoadScene("MainMenu");
        };
    }

    public void CloseSystemMenu()
    {
        blackImage.DOFade(1f, 0.5f).onComplete = () => {
            menuPanel.SetActive(true);
            systemPanel.SetActive(false);
            confirmPanel.SetActive(false);
            blackImage.DOFade(0f, 0.5f).onComplete = () => {
            };
        };
    }
}
