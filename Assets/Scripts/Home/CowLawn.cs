using GM;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CowLawn : MonoBehaviour
{
    [SerializeField] GameObject computer;
    [SerializeField] GameObject osCanvas;
    [SerializeField] TextMeshProUGUI scoreText;
    public CowPlayer player;
    [SerializeField] GameObject gameoverPanel;
    [SerializeField] TextMeshProUGUI gameoverText;
    AudioSource audio;
    [SerializeField] string newscoreText = "Congratulations!\nYou beat your highest score!\nNew record is: ";
    [SerializeField] string nothighscoreText = "Congratulations! Your highest score is: ";

    int score = 0;
    int enemyCount = 3;
    int record = 0;

    private void Awake()
    {
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        gameoverPanel.SetActive(false);
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(DelayExit());
        }
    }

    IEnumerator DelayExit()
    {
        osCanvas.SetActive(true);
        computer.SetActive(true);
        gameObject.SetActive(false);
        GameFuncs.PlayerScript.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
    }

    public void UpdateScore(int newScore)
    {
        score += newScore;
        scoreText.text = "Score: " + score.ToString();
    }

    public void EnemyKill()
    {
        enemyCount--;
        if (enemyCount <= 0)
            GameOver();
    }

    private void GameOver()
    {
        gameoverText.text = nothighscoreText + record.ToString();
        if (score > record)
        {
            record = score;
            gameoverText.text = newscoreText + record.ToString();
        }
        gameoverPanel.SetActive(true);
        player.SetControl(false);
        audio.Play();
    }

    public void StartGame()
    {

    }
}
