using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using GM;
using SolitaryAudio;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] static AudioClip openSound;
    [SerializeField] GameObject destinationPoint;
    [SerializeField] Image blackImage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "UseCube") // If a player presses E button on the door
        {
            GameFuncs.PlayerScript.SetControl(false);
            AudioController.Play("doorOpen");
            blackImage.DOColor(new Color(0, 0, 0, 1), 1f).onComplete = () => // Fadeout
            {
                AudioController.Play("doorClose");
                GameFuncs.TeleportPlayer(destinationPoint);
                blackImage.DOColor(new Color(0, 0, 0, 0), 1f); // Fadein
                GameFuncs.PlayerScript.SetControl(true);
            };
        }
    }
}
