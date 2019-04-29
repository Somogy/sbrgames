using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Public variables
    public bool gameStarted;
    public float gameSpeed;

    // public Components
    public AudioListener audioListener;
    public GameObject countDownPanel;
    public GameObject gameOverPanel;
    public GameObject pauseMenu;
    public GameObject player;
    public Text actualScoreText;
    public Text countDown;

    // Private variables
    private float timerToPlay;

    public float timer;

    private void Awake()
    {
        instance = this;
        timerToPlay = 3;
    }

    private void Start()
    {
        AudioManager.instance.PlaySFX(5);
        countDownPanel.SetActive(true);
        PlayerPrefs.SetFloat("GameSpeed", gameSpeed);
        PlayerPrefs.SetInt("ActualScore", 0);
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        actualScoreText.text = PlayerPrefs.GetInt("ActualScore").ToString();

        // Initial countDown
        if (timerToPlay >= 0)
        {
            timerToPlay -= Time.deltaTime;
            countDown.text = Convert.ToInt32(timerToPlay).ToString();
        }

        else if (timerToPlay <= 0)
        {
            countDownPanel.SetActive(false);
            gameStarted = true;
        }


        // Pause game conditions
        if (Input.GetKey(KeyCode.Escape) && gameStarted)
        {
            Pause();
        }

        if (Time.timeScale == 0 && gameStarted)
        {
            audioListener.enabled = false;            
        }

        else
        {
            audioListener.enabled = true;
            pauseMenu.SetActive(false);
        }

        // Verify the player
        if (player == null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Continue()
    {
        Time.timeScale = 1;
    }
    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
