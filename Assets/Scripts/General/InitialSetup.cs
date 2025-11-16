using UnityEngine;
using GM;
using UnityEngine.UI;
using TMPro;

public class InitialSetup : MonoBehaviour
{
    [SerializeField] Image blackImage;
    [SerializeField] TextMeshProUGUI text;

    void Awake()
    {
        GameFuncs.BlackImage = blackImage;
    }

    public void TheEnd()
    {
        text.gameObject.SetActive(true);
        GameFuncs.PlayerScript.SetControl(false);
    }
}
