using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [SerializeField] GameObject osCanvas;
    [SerializeField] GameObject gameCamera;
    [SerializeField] GameObject browserImage;
    [SerializeField] GameObject browserStream;
    int gameClicks = 0;
    int imageBoardClicks = 0;
    int streamClicks = 0;
    float currentTime = 0f;
    float resetClicksTime = 0.3f;
    float currentTimeInputDelay = 0f;
    float TimeInputDelay = 0.2f;

    private void Awake()
    {
        //gameObject.SetActive(false);
        osCanvas.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {

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
        Cursor.lockState = CursorLockMode.None;
        GameFuncs.PlayerScript.SetControl(false);
        osCanvas.SetActive(true);
    }

    public void OpenStreams()
    {
        browserStream.SetActive(true);
    }

    public void OpenImageBoard()
    {
        browserImage.SetActive(true);
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
