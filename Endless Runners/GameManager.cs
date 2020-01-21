using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Instance this to access variables
    public static GameManager instance;

    // Public Variables
    [SerializeField] public int score; // To use in actual score

    [HideInInspector] public bool inGame; // 
    [HideInInspector] public float newSpeed;

    // Private Variables
    [Header("Interval time to the projectiles have a new speed")]
    [SerializeField] private float intervalNewSpeed;
    private float timerNewSpeed;

    // Private Components
    [Header("Animators")]
    [SerializeField] private Animator bgMenuAnim;

    [Header("Game Objects")]
    [SerializeField] private GameObject gameOverPanel;

    [Tooltip("In game UI Panel")]
    [SerializeField] private GameObject gameUI;

    [Header("Sprite Mask")]
    [Tooltip("Sprite Mask used to hide the caracter of customization menu")]
    [SerializeField] private SpriteMask bgGameMask;

    [Header("UI Elements")]
    [Tooltip("To show score of actual game playing")]
    [SerializeField] private Text scoreText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // For default values in game
        InitialVariablesValues();
    }

    private void Update()
    {
        // Updating the score
        scoreText.text = score.ToString();

        // To calculate the new speed for the projectiles
        TimerProjectileSpeed();

        if (Input.GetKey(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();
        }

        if (Input.GetKey(KeyCode.R))
        {
            PlayerPrefs.SetInt("HighScore" + 0, 50);
            PlayerPrefs.SetInt("HighScore" + 1, 40);
            PlayerPrefs.SetInt("HighScore" + 2, 30);
            PlayerPrefs.SetInt("HighScore" + 3, 20);
            PlayerPrefs.SetInt("HighScore" + 4, 10);

            PlayerPrefs.SetString("HighName" + 0, "AAA");
            PlayerPrefs.SetString("HighName" + 1, "BBB");
            PlayerPrefs.SetString("HighName" + 2, "CCC");
            PlayerPrefs.SetString("HighName" + 3, "DDD");
            PlayerPrefs.SetString("HighName" + 4, "EEE");
        }
    }

    // Method for "Pause" button
    public void PauseGame()
    {
        Time.timeScale = 0;
        bgGameMask.enabled = false;
        bgMenuAnim.SetBool("alphaIn", false);
    }

    // For button FINISH in the customization character menu, check if the game is already in progress, if not the presentation of the edited character happens
    public void FinishButton()
    {
        // If the game is already playing
        if (inGame)
        {
            Time.timeScale = 1;
            bgMenuAnim.SetBool("alphaIn", true);
            CustomCharMenu.instance.charMenuGO.SetActive(false);
            gameUI.SetActive(true);
        }

        // If the game is play for the first time, show the character presentation
        else
        {
            StartCoroutine(CharacterPresentation());
            inGame = true;        
        }
    }

    // Method for "Try Again" button
    public void TryAgain()
    {
        StartCoroutine(PlayAgain());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
      
    // Initial variables
    private void InitialVariablesValues()
    {
        // Need to check if the game is playing
        inGame = false;

        // Game in pause, to show the Custom Menu of Character
        //Time.timeScale = 0;
    }

    // Calculate the new speed for the projectiles
    private void TimerProjectileSpeed()
    {
        timerNewSpeed += Time.deltaTime;

        if (timerNewSpeed >= intervalNewSpeed)
        {
            timerNewSpeed = 0;

            newSpeed += newSpeed + 0.25f;
        }
    }

    // Game Over Method
    public IEnumerator GameOver()
    {
        gameUI.SetActive(false); // Desactive the in game UI
        yield return new WaitForSeconds(0.15f);

        Time.timeScale = 0;
        gameOverPanel.SetActive(true); // Active the "Game Over Panel" with the Leader Board
    }

    // In the first play, show character presentation
    private IEnumerator CharacterPresentation()
    {
        CustomCharMenu.instance.charMenuIdle.enabled = false;
        CustomCharMenu.instance.charPresentation.enabled = true;
        yield return new WaitForSecondsRealtime(2f);

        bgGameMask.enabled = true; // Mask to hide the caracter of customization menu
        yield return new WaitForSecondsRealtime(0.2f);

        CustomCharMenu.instance.charMenuGO.SetActive(false);
        gameUI.SetActive(true); // Active UI in game
        bgMenuAnim.SetBool("alphaIn", true); // Fade to alpha the back ground of the customization menu
        Time.timeScale = 1;
    }

    // Method for "Play Again"
    private IEnumerator PlayAgain()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
