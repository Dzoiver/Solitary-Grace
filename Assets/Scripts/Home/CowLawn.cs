using GM;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class CowLawn : MonoBehaviour
{
    [SerializeField] GameObject computer;
    [SerializeField] GameObject osCanvas;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI recordText;
    public CowPlayer player;
    public GameObject gameoverPanel;
    [SerializeField] TextMeshProUGUI gameoverText;
    AudioSource audio;
    [SerializeField] string newscoreText = "Congratulations!\nYou beat your highest score!\nNew record is: ";
    [SerializeField] string nothighscoreText = "Congratulations! Your highest score is: ";

    int score = 0;
    int enemyCount = 3;
    int record = 0;

    [SerializeField] GameObject enemiesParent;
    EnemyCow[] enemies;
    [SerializeField] GameObject directionalLight;
    [SerializeField] Camera maincam;
    [SerializeField] Camera wincam;
    public int Score { get => score; set
        {
            score = value;
            scoreText.text = "Score: " + Score.ToString();
        } }

    public int Record { get => record; set
        {
            record = value;
            recordText.text = "Record: " + record.ToString();
             } }

    private void Awake()
    {
        gameObject.SetActive(false);
        enemies = enemiesParent.GetComponentsInChildren<EnemyCow>();
    }
    // Start is called before the first frame update
    void Start()
    {
        gameoverPanel.SetActive(false);
        audio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        directionalLight.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(DelayExit());
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            RestartGame();
        }
    }

    IEnumerator DelayExit()
    {
        Cursor.lockState = CursorLockMode.None;
        osCanvas.SetActive(true);
        computer.SetActive(true);
        gameObject.SetActive(false);
        GameFuncs.PlayerScript.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
    }

    public void UpdateScore(int newScore)
    {
        Score += newScore;
    }

    public void EnemyKill()
    {
        enemyCount--;
        if (enemyCount <= 0)
            GameOver();
    }

    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        player.bazooka.enabled = false;
        gameoverText.text = nothighscoreText + Record.ToString();
        if (Score > Record)
        {
            Record = Score;
            gameoverText.text = newscoreText + Record.ToString();
        }
        gameoverPanel.SetActive(true);
        player.SetControl(false);
        audio.Play();
        wincam.enabled = true;
        maincam.enabled = false;
    }

    public void RestartGame()
    {
        if (player.bazooka.Shoot)
            return;

        Cursor.lockState = CursorLockMode.Locked;
        player.SetControl(true);
        player.AvailableAmmo = 4;
        player.bazooka.enabled = true;
        player.bazooka.ResetBazooka();
        gameoverPanel.SetActive(false);
        Score = 0;
        enemyCount = 0;
        foreach (EnemyCow cow in enemies)
        {
            enemyCount++;
            cow.ResetCow();
        }
        wincam.enabled = false;
        maincam.enabled = true;
    }
}
