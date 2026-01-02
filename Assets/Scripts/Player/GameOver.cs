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
    [SerializeField] Image image;
    [SerializeField] GameObject destination;
    [SerializeField] TextMeshProUGUI text; // Text you died
    private Sequence sequence;
    public UnityEvent onRespawn;
    GameStatistics stat;

    private void Awake()
    {
        stat = FindAnyObjectByType<GameStatistics>();
    }

    public void NormalDeath(PlayerScript player)
    {
        stat.Deaths++;
        sequence = DOTween.Sequence();
        player.SetControl(false);
        image.gameObject.SetActive(true);
        text.DOFade(1, 3);
        sequence.Append(image.DOColor(new Color(1, 0, 0, 1), 3f)).AppendInterval(2f).onComplete = () =>
        {
            onRespawn.Invoke();
            GameFuncs.DisableWeapons(true);
            text.DOFade(0, 0f);
            image.DOColor(new Color(0, 0, 0, 0), 0.5f);
            GameFuncs.TeleportPlayer(destination);

            player.SetControl(true);
        };
    }

    public void GetDamagedRedScreen()
    {
        sequence = DOTween.Sequence();
        image.gameObject.SetActive(true);
        sequence.Append(image.DOColor(new Color(1, 0, 0, 0.25f), 0.25f)).AppendInterval(0.25f).onComplete = () =>
        {
            image.DOColor(new Color(0, 0, 0, 0), 0.25f);
        };
    }

    public void DieFromMonster()
    {
        stat.Deaths++;
        sequence = DOTween.Sequence();
        image.gameObject.SetActive(true);
        GameFuncs.PlayerScript.SetControl(false);
        image.DOColor(new Color(0, 0, 0, 1), 1f).onComplete = () =>
        {
            onRespawn.Invoke();
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
