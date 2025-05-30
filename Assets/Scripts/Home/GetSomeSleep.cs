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
    private BoxCollider boxcollider;
    private DaytimeOutside daytimeScript;

    private void Awake()
    {
        daytimeScript = FindObjectOfType<DaytimeOutside>();
    }
    private void Start()
    {
        boxcollider = GetComponent<BoxCollider>();
    }

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
        daytimeScript.SetDay(false);
        prison.SetActive(true);
        GameFuncs.TeleportPlayer(destinationPoint);
        blackImage.DOColor(new Color(0, 0, 0, 0), 0.5f);
        sound.Play();
        GameFuncs.PlayerScript.SetControl(true);
        this.enabled = false;
        boxcollider.enabled = false;
    }
}
