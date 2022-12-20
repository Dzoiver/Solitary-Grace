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
    [SerializeField] GameObject player;
    [SerializeField] PlayerScript pScript;
    [SerializeField] CharacterController playercont;
    [SerializeField] GameObject dp;
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
        blackImage.DOColor(new Color(0, 0, 0, 0), 5f);
        playercont.enabled = false;
        player.transform.position = dp.transform.position;
        playercont.enabled = true;
        pScript.AllowControl(true);
    }
}
