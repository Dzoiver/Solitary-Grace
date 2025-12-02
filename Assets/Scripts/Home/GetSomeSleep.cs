using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using GM;
using UnityEngine.Events;

public class GetSomeSleep : MonoBehaviour
{
    [SerializeField] Image blackImage;
    [SerializeField] GameObject destinationPoint;
    [SerializeField] AudioSource sound = null;
    [SerializeField] GameObject prison;
    [SerializeField] GameObject musicHome = null;
    private Sequence sequence;
    private BoxCollider boxcollider;
    private DaytimeOutside daytimeScript;
    public UnityEvent onSleep;
    public Checkpoint checkpoint;

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
            if (checkpoint != null)
                checkpoint.OnTeleportInvoke();
        }
    }

    private void GoToPrison()
    {
        Debug.Log("invoking onsleep");
        onSleep.Invoke();
        if (musicHome != null)
            musicHome.SetActive(false);
        daytimeScript.SetDay(false);
        prison.SetActive(true);
        GameFuncs.TeleportPlayer(destinationPoint);
        blackImage.DOColor(new Color(0, 0, 0, 0), 0.5f);
        if (sound != null)
            sound.Play();
        GameFuncs.PlayerScript.SetControl(true);
        GameFuncs.DisableWeapons(false);
        //boxcollider.enabled = false;
    }
}
