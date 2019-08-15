using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Games Panels")]
    [SerializeField]
    public GameObject introPanel;

    [SerializeField]
    public GameObject gamePanel;

    [SerializeField]
    public GameObject gameOverPanel;

    [Space(10)]
    [SerializeField]
    private GameObject[] questionsGO;

    [Space(10)]
    [Header("Texts")]
    [SerializeField]
    private Text gameOverPhrase, scoreText;

    private int actualQuestion, score;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("Restart") == 1)
        {
            introPanel.SetActive(false);
            gamePanel.SetActive(true);
            PlayerPrefs.SetInt("Restart", 0);
        }
    }

    private void Start()
    {
        actualQuestion = 0;
        score = 0;
    }

    private void Update()
    {
        if (actualQuestion >= questionsGO.Length)
        {
            GameOver();
        }
    }

    // Alternatives Methods Buttons
    public void CorrectAnswer()
    {
        score++;
    }

    public void StartGame()
    {
        introPanel.GetComponent<Animator>().enabled = true;
        gamePanel.SetActive(true);
        StartCoroutine(StartGameCR());
    }

    public void SelectedAlternative()
    {
        StartCoroutine(QuestionsTransition());
        
        QuestionAnimOff();
    }

    // Menu Methods Buttons
    public void IntroPanelReturn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartGame()
    {
        PlayerPrefs.SetInt("Restart", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);      
    }

    // Game Manager Methods
    private void GameOver()
    {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);

        scoreText.text = score.ToString();

        if (score == 5)
        {
            gameOverPhrase.text = "Parabéns acertou todas!";
        }

        else
        {
            gameOverPhrase.text = "Foi quase!!";
        }

        Debug.Log("Fim de jogo");
    }

    private void QuestionAnimOff()
    {
        if (actualQuestion == 0)
        {
            questionsGO[actualQuestion].GetComponent<Animator>().enabled = true;
        }

        else
        {
            questionsGO[actualQuestion].GetComponent<Animator>().SetBool("Off", true);
        }
    }

    private void QuestionAnimOn()
    {
        if (actualQuestion < 5)
        {
            questionsGO[actualQuestion].GetComponent<Animator>().enabled = true;
        }
    }

    private IEnumerator QuestionsTransition()
    {
        yield return new WaitForSeconds(0.3f);

        questionsGO[actualQuestion].SetActive(false);

        actualQuestion++;

        if (actualQuestion < questionsGO.Length)
        {
            questionsGO[actualQuestion].SetActive(true);
        }

        QuestionAnimOn();
    }

    private IEnumerator StartGameCR()
    {
        yield return new WaitForSeconds(0.5f);
        introPanel.GetComponent<Animator>().enabled = false;
        introPanel.SetActive(false);
    }
}
