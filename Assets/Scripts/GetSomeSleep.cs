using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GetSomeSleep : MonoBehaviour
{
    [SerializeField] Image blackImage;
    [SerializeField] GameObject destinationPoint;
    [SerializeField] AudioSource sound;
    Sequence seq;
    GameObject player;
    PlayerScript pScript;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pScript = player.GetComponent<PlayerScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "UseCube")
        {
            seq = DOTween.Sequence();
            seq.Append(blackImage.DOColor(new Color(0, 0, 0, 1), 3f)).AppendInterval(2f);
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
        pScript.cControl.enabled = false;
        player.transform.position = destinationPoint.transform.position;
        pScript.cControl.enabled = true;
        seq.Append(blackImage.DOColor(new Color(0, 0, 0, 0), 0.5f));
        sound.Play();
        pScript.AllowControl(true);
    }
}
