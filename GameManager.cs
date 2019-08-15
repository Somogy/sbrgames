using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Games Panels")]
    [SerializeField]
    public GameObject introPanel; //This panel has a start button

    [SerializeField]
    public GameObject gamePanel; //This is the main panel for the game, has restart button, back intro button and the questions

    [SerializeField]
    public GameObject gameOverPanel; //This panel is show in the end of the game, with the points and ack intro button

    [Space(10)]
    [SerializeField]
    private GameObject[] questionsGO; //All questions were asked in gameobjects due to the small amount of them

    [Space(10)]
    [Header("Texts")]
    [SerializeField]
    private Text gameOverPhrase, scoreText; //Elements used in the game over panel

    private int actualQuestion, score; //Variables to use in game

    private void Awake()
    {
    //Checking which panels should be used
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
        //Verify when the game is over    
        if (actualQuestion >= questionsGO.Length)
        {
            GameOver();
        }
    }

    //Method linked in the correct answers
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
    
    //When a alternative of question is selected
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
            gameOverPhrase.text = "Congratulations!"; //With all hits, the player is congratulated
        }

        else
        {
            gameOverPhrase.text = "Almost there!"; //Else, this
        }
    }
    
    //Animations controllers
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
    
    //Manipulating the animators in the start game
    private IEnumerator StartGameCR()
    {
        yield return new WaitForSeconds(0.5f);
        introPanel.GetComponent<Animator>().enabled = false;
        introPanel.SetActive(false);
    }
}
