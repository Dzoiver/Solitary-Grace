using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using GM;

public class GetSomeSleep : MonoBehaviour
{
    [SerializeField] Image blackImage;
    [SerializeField] GameObject destinationPoint;
    [SerializeField] AudioSource sound;
    [SerializeField] GameObject prison;
    private Sequence sequence;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "UseCube") // Player presses E
        {
            sequence = DOTween.Sequence();
            GameFuncs.PlayerScript.SetControl(false);
            sequence.Append(blackImage.DOColor(new Color(0, 0, 0, 1), 3f)).AppendInterval(2f).onComplete = GoToPrison;
        }
    }

    private void GoToPrison()
    {
        prison.SetActive(true);
        GameFuncs.TeleportPlayer(destinationPoint);
        blackImage.DOColor(new Color(0, 0, 0, 0), 0.5f);
        sound.Play();
        GameFuncs.PlayerScript.SetControl(true);
    }
}
