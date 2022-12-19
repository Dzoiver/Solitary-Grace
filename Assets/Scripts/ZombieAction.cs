using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ZombieAction : MonoBehaviour
{
    [SerializeField] Vector3 endMove;
    [SerializeField] Vector3 endRotate;
    [SerializeField] Image blackImage;
    [SerializeField] GameObject audio;
    public void HeadMoveScary()
    {
        audio.SetActive(true);
        transform.DOMove(endMove, 8f);
        transform.DORotate(endRotate, 10f).onComplete = Fadeout;
    }

    public void Fadeout()
    {
        blackImage.DOColor(new Color(0, 0, 0, 1), 0.5f);
    }
}
