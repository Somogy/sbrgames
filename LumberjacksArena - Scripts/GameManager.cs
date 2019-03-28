using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool fight;
    private int enemyScore, playerScore;

    public Animator enemyAnim;
    public CapsuleCollider2D playerColl;
    public GameObject fightAgainButton, punchButton;
    public Rigidbody2D enemyRb2D, playerRb2D;
    public Text panel, enemyScorePanel, playerScorePanel;

    private void Start()
    {
        instance = this;
        fight = false;
        StartCoroutine(GamePreparation());

        enemyScore = PlayerPrefs.GetInt("EnemyScore");
        playerScore = PlayerPrefs.GetInt("PlayerScore");
    }

    private void Update()
    {
        if (!Enemy.instance.alive || !Player.instance.alive || Player.instance.touchRedLine)
        {
            if (fight)
            {
                StartCoroutine(FightFinished());
            }
        }       
    }

    public void Rematch()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        fightAgainButton.SetActive(false);
    }

    private IEnumerator GamePreparation()
    {
        if (PlayerPrefs.GetInt("FirstPlay") != 1)
        {
            enemyScorePanel.text = "";
            playerScorePanel.text = "";
            panel.resizeTextForBestFit = true;
            panel.text = "An abducted man, alone in a test chamber...";
            yield return new WaitForSeconds(4f);
            panel.text = "He will follow your finger on the left side of the screen";
            yield return new WaitForSeconds(6f);
            panel.text = "Press X to Punch";
            yield return new WaitForSeconds(4f);
            panel.text = "Do not go beyond the red line...";
            yield return new WaitForSeconds(4f);
            panel.text = "...and do not touch the blue laser";
            yield return new WaitForSeconds(4f);
            panel.text = "Touch this panel to Restart the match";
            yield return new WaitForSeconds(4f);
            PlayerPrefs.SetInt("FirstPlay", 1);
            panel.resizeTextForBestFit = false;
        }

        panel.text = "Get Ready!";

        yield return new WaitForSeconds(1f);
        panel.text = "Fight!";
        enemyScorePanel.text = enemyScore.ToString();
        playerScorePanel.text = playerScore.ToString();

        
        fight = true;
        enemyAnim.SetBool("Fight", true);
        punchButton.SetActive(true);

        yield return new WaitForSeconds(1f);
        panel.text = "KILL!";
    }

    private IEnumerator FightFinished()
    {
        fight = false;

        if (!Enemy.instance.alive)
        {
            panel.text = "You WIN!";
            playerRb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            playerColl.enabled = false;
            PlayerPrefs.SetInt("PlayerScore", playerScore + 1);
        }

        if (!Player.instance.alive)
        {
            panel.text = "You LOST!";
            enemyRb2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (Player.instance.touchRedLine)
        {
            panel.text = "Exceeded RED LINE!";
        }

        if (Player.instance.laserDeath)
        {
            panel.text = "You touch the LASER WALL!";            
        }

        if (!Player.instance.alive || Player.instance.touchRedLine || Player.instance.laserDeath)
        {

            PlayerPrefs.SetInt("EnemyScore", enemyScore + 1);
        }

        yield return new WaitForSeconds(2f);
        
        panel.text = "Fight Again?";
        fightAgainButton.SetActive(true);
    }
}
