using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Isntance of this Script
    public static GameManager instance;

    //Publics Components
    public GameObject gameOverPanel;
    public GameObject enemySpawnObject;
    public GameObject mobsSpawnObject;

    private void Awake()
    {
        instance = this;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        enemySpawnObject.SetActive(false);
        mobsSpawnObject.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void StartGame()
    {
        gameOverPanel.SetActive(false);
    }
}
