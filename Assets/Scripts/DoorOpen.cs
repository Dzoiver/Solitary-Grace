using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] GameObject destinationPoint;
    [SerializeField] Image blackImage;
    CharacterController playercont;
    GameObject player;
    PlayerScript pScript;
    Sequence seq;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pScript = player.GetComponent<PlayerScript>();
        playercont = pScript.cControl;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "UseCube")
        {
            seq = DOTween.Sequence();
            seq.Append(blackImage.DOColor(new Color(0, 0, 0, 1), 1f));
            seq.onComplete = TeleportPlayer;
            pScript.AllowControl(false);
        }

        if (gameObject.name == "Teleport")
        {
            TeleportPlayer();
            pScript.Warping(true);
        }
    }

    private void TeleportPlayer()
    {
        playercont.enabled = false;
        player.transform.position = destinationPoint.transform.position;
        playercont.enabled = true;
        seq.Append(blackImage.DOColor(new Color(0, 0, 0, 0), 1f));
        pScript.AllowControl(true);
    }
}
