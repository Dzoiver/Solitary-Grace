using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using GM;

public class ZombieAction : MonoBehaviour
{
    private Vector3 initialPosition;
    [SerializeField] Vector3 endMove;
    [SerializeField] Vector3 endRotate;
    [SerializeField] Image blackImage;
    [SerializeField] GameObject audio;
    [SerializeField] GameObject dp;
    [SerializeField] GameObject quakeWorld;
    [SerializeField] GameObject house;

    private void Start()
    {
        initialPosition = transform.position;
    }
    public void HeadMoveScary()
    {
        audio.SetActive(true);
        transform.DOMove(endMove, 8f);
        transform.DORotate(endRotate, 10f).onComplete = Fadeout;
    }

    public void Fadeout()
    {
        blackImage.DOColor(new Color(0, 0, 0, 1), 0.5f).onComplete = MovePlayer;
    }

    private void MovePlayer()
    {
        house.SetActive(true);
        transform.position = initialPosition;
        // quakeWorld.SetActive(true);
        blackImage.DOColor(new Color(0, 0, 0, 0), 5f); // Fadein
        GameFuncs.TeleportPlayer(dp);
        // GameFuncs.PlayerScript.AllowJump = true;
        GameFuncs.PlayerScript.SetControl(true);
    }
}
