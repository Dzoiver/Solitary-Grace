using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] GameObject destinationPoint;
    [SerializeField] GameObject player;
    [SerializeField] PlayerScript pScript;
    [SerializeField] CharacterController playercont;
    [SerializeField] Image blackImage;
    Sequence seq;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        seq = DOTween.Sequence();
        seq.Append(blackImage.DOColor(new Color(0, 0, 0, 1), 1f));
        seq.onComplete = TeleportPlayer;
        pScript.AllowControl(false);
    }

    private void TeleportPlayer()
    {
        playercont.enabled = false;
        player.transform.position = destinationPoint.transform.position;
        playercont.enabled = true;
        seq.Append(blackImage.DOColor(new Color(0, 0, 0, 0), 1f));
        pScript.AllowControl(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}