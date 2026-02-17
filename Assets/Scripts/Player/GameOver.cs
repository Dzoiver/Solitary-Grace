using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using GM;
using TMPro;
using UnityEngine.Events;

public class GameOver : MonoBehaviour
{
    [SerializeField] Image fadeImage;
    [SerializeField] Image redScreenImage;
    [SerializeField] GameObject destination;
    [SerializeField] TextMeshProUGUI text; // Text you died
    public GameObject bloodstaines;
    private Sequence sequence;
    public UnityEvent onRespawn;
    GameStatistics stat;

    private void Awake()
    {
        redScreenImage.DOColor(new Color(0, 0, 0, 0), 0f);
        stat = FindAnyObjectByType<GameStatistics>();
    }

    public void NormalDeath(PlayerScript player)
    {
        stat.Deaths++;
        sequence = DOTween.Sequence();
        player.SetControl(false);
        fadeImage.gameObject.SetActive(true);
        text.DOFade(1, 3);
        sequence.Append(fadeImage.DOColor(new Color(1, 0, 0, 1), 3f)).AppendInterval(2f).onComplete = () =>
        {
            player.Health = 100f;
            onRespawn.Invoke();
            GameFuncs.DisableWeapons(true);
            text.DOFade(0, 0f);
            fadeImage.DOColor(new Color(0, 0, 0, 0), 0.5f);
            GameFuncs.TeleportPlayer(destination);
            player.currentElevator = null;
            player.inElevator = false;
            player.SetControl(true);
        };
    }

    public void GetDamagedRedScreen()
    {
        sequence = DOTween.Sequence();
        redScreenImage.gameObject.SetActive(true);
        sequence.Append(redScreenImage.DOColor(new Color(1, 0, 0, 0.25f), 0.25f)).AppendInterval(0.25f).onComplete = () =>
        {
            redScreenImage.DOColor(new Color(0, 0, 0, 0), 0.25f);
        };
    }

    public void DieFromMonster()
    {
        stat.Deaths++;
        GameFuncs.PlayerScript.SetControl(false);
        sequence = DOTween.Sequence();
        fadeImage.gameObject.SetActive(true);
        fadeImage.DOColor(new Color(0, 0, 0, 1), 1f).onComplete = () =>
        {
            onRespawn.Invoke();
            GameFuncs.PlayerScript.inElevator = false;
            GameFuncs.PlayerScript.currentElevator = null;
        };
        /*
        sequence.Append(image.DOColor(new Color(0, 0, 0, 1), 1f)).AppendInterval(2f).onComplete = () =>
        {
            //GameFuncs.PlayerScript.SetControl(true);
            //GameFuncs.PlayerScript.CameraRestore();
            //image.DOColor(new Color(0, 0, 0, 0), 0.5f);
            //GameFuncs.TeleportPlayer(destination);
        };
        */
    }
}
