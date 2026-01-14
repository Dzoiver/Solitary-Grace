using DG.Tweening;
using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class Computer : MonoBehaviour
{
    public static GameObject notificationBell;
    [SerializeField] GameObject osCanvas;
    [SerializeField] GameObject gameCamera;
    [SerializeField] ImageBoard browserImage;
    [SerializeField] GameObject browserStream;

    int gameClicks = 0;
    int imageBoardClicks = 0;
    int streamClicks = 0;
    float currentTime = 0f;
    float resetClicksTime = 0.35f;
    float currentTimeInputDelay = 0f;
    float TimeInputDelay = 0.2f; // delay to not break control or something

    public UnityEvent onTurnOn;
    [SerializeField] Renderer notification;

    private void Awake()
    {
        notificationBell = notification.gameObject;
        //gameObject.SetActive(false);
        osCanvas.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        notification.material.DOFade(0f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        notification.gameObject.SetActive(false);
        browserImage.NotificationCount = 0;
    }

    private void OnEnable()
    {
        currentTimeInputDelay = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        currentTimeInputDelay += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape) && currentTimeInputDelay > TimeInputDelay && osCanvas.activeSelf)
        {
            StartCoroutine(DelayFunc());
        }

        if (gameClicks > 0 || imageBoardClicks > 0 || streamClicks > 0)
        {
            currentTime += Time.deltaTime;
        }

        if (currentTime > resetClicksTime)
        {
            gameClicks = 0;
            imageBoardClicks = 0;
            streamClicks = 0;
            currentTime = 0f;
        }
    }

    public void GameClick()
    {
        gameClicks++;
        if (gameClicks >= 2)
        {
            OpenGame();
        }
    }

    public void ImageClick()
    {
        imageBoardClicks++;
        if (imageBoardClicks >= 2)
        {
            OpenImageBoard();
        }
    }

    public void StreamClick()
    {
        streamClicks++;
        if (streamClicks >= 2)
        {
            OpenStreams();
        }
    }


    public void TurnOn()
    {
        onTurnOn.Invoke();
        Cursor.lockState = CursorLockMode.None;
        GameFuncs.PlayerScript.SetControl(false);
        browserImage.gameObject.SetActive(false);
        notificationBell.SetActive(false);
        osCanvas.SetActive(true);
    }

    public void OpenStreams()
    {
        browserStream.SetActive(true);
    }

    public void OpenImageBoard()
    {
        browserImage.gameObject.SetActive(true);
    }

    public void OpenGame()
    {
        osCanvas.SetActive(false);
        GameFuncs.PlayerScript.gameObject.SetActive(false);
        gameCamera.SetActive(true);
        gameObject.SetActive(false);
    }

    IEnumerator DelayFunc()
    {
        osCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(0.5f);
        GameFuncs.PlayerScript.SetControl(true);
    }
}
