using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using GM;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] PlayerScript player;
    [SerializeField] GameObject destination;
    [SerializeField] TextMeshProUGUI text;
    private Sequence sequence;

    public void ShowGameOver()
    {
        sequence = DOTween.Sequence();
        player.SetControl(false);
        image.gameObject.SetActive(true);
        text.DOFade(1, 3);
        sequence.Append(image.DOColor(new Color(1, 0, 0, 1), 3f)).AppendInterval(2f).onComplete = () =>
        {
            text.DOFade(0, 0.5f);
            image.DOColor(new Color(0, 0, 0, 0), 0.5f);
            GameFuncs.TeleportPlayer(destination);

            player.SetControl(true);
        };
    }
}
