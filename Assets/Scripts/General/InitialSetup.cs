using UnityEngine;
using GM;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class InitialSetup : MonoBehaviour
{
    [SerializeField] Image blackImage;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI otherStatistics;
    GameStatistics statistics;
    int hours;
    int minutes;
    int seconds;
    int milliseconds;
    TimeSpan timeSpan;

    void Awake()
    {
        GameFuncs.BlackImage = blackImage;
        statistics = FindObjectOfType<GameStatistics>();
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
    }
}
