using UnityEngine;
using GM;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;
using UnityEngine.Localization.Settings;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Toggle = UnityEngine.UI.Toggle;

public class InitialSetup : MonoBehaviour
{
    [SerializeField] Image blackImage;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI otherStatistics;
    [SerializeField] TextMeshProUGUI liveTimer;
    [SerializeField] TextMeshProUGUI menuTimer;
    [SerializeField] Toggle toggleTimer;
    GameStatistics statistics;
    int hours;
    int minutes;
    int seconds;
    int milliseconds;
    TimeSpan timeSpan;
    public int targetFPS = 250;
    private static bool english = true;

    public static bool English { get => english; set => english = value; }

    void Awake()
    {
        GameFuncs.BlackImage = blackImage;
        statistics = FindObjectOfType<GameStatistics>();
        Application.targetFrameRate = targetFPS;

        if (PlayerPrefs.HasKey("TIMER"))
        {
            int timerPref = PlayerPrefs.GetInt("TIMER");
            if (timerPref == 1 && liveTimer != null)
            {
                if (toggleTimer != null)
                    toggleTimer.isOn = true;
                liveTimer.gameObject.SetActive(true);
            }
        }
    }

    public void ToggleTimer()
    {
        if (liveTimer.gameObject.activeSelf)
        {
            PlayerPrefs.SetInt("TIMER", 0);
            liveTimer.gameObject.SetActive(false);
        }
        else
        {
            liveTimer.gameObject.SetActive(true);
            PlayerPrefs.SetInt("TIMER", 1);
        }
        PlayerPrefs.Save();
    }

    private void Update()
    {
        if (liveTimer == null)
            return;

        if (menuTimer == null)
            return;

        if (!liveTimer.gameObject.activeSelf && !menuTimer.gameObject.activeSelf)
            return;
        timeSpan = TimeSpan.FromSeconds(statistics.GameTime);
        hours = timeSpan.Hours;
        minutes = timeSpan.Minutes;
        seconds = timeSpan.Seconds;
        liveTimer.text = $"{hours:00}:{minutes:00}:{seconds:00}";
        menuTimer.text = $"{hours:00}:{minutes:00}:{seconds:00}";
    }

    public void TheEnd()
    {
        text.gameObject.SetActive(true);
        otherStatistics.gameObject.SetActive(true);
        timeSpan = TimeSpan.FromSeconds(statistics.GameTime);
        hours = timeSpan.Hours;
        minutes = timeSpan.Minutes;
        seconds = timeSpan.Seconds;
        milliseconds = timeSpan.Milliseconds;

        otherStatistics.text = "Deaths: " + statistics.Deaths + "\nTime: " + $"{hours:00}:{minutes:00}:{seconds:00}.{milliseconds}";

        text.DOFade(1f, 5f).onComplete = () =>
        {
            otherStatistics.DOFade(1f, 3f);
        };
        
        GameFuncs.PlayerScript.SetControl(false);
        enabled = false;
    }

    public void LanguageRU()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales.Find(loc => loc.Identifier.Code == "ru-RU");
    }

    public void LanguageEN()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales.Find(loc => loc.Identifier.Code == "en-US");
    }
}
