using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Private Variables
    private int actualScore;
    private int newPosition;

    // Private Components
    [Header("Components for LeadBoard")]
    [SerializeField] private GameObject leadBoard; // Score Table
    [SerializeField] private GameObject inputFieldGO; // Input Field for player with a new record
    [SerializeField] private InputField inputField;
    [SerializeField] private List<Text> scoreTableText; // UI element list that will show the score table texts
    [SerializeField] private List<Text> nameTableText;

    // Used for recovery the saved scores
    public List<int> highScoreTable;
    public List<string> highNameTable;  

    private void Start()
    {
        // Reading the actual score
        actualScore = GameManager.instance.score;

        // Populate the list of high scores
        PopulateNameCollection();
        PopulateScoreCollection();        
    }

    // Reading the name inputed
    public void EnterButton()
    {
        if (inputField.text != "")
        {
            PlayerPrefs.SetString("HighName" + newPosition, inputField.text.ToUpper());
            nameTableText[newPosition].text = inputField.text.ToUpper();
        }
    }

    private void PopulateNameCollection()
    {
        for (int i = 0; i < highNameTable.Count; i++)
        {
            if (PlayerPrefs.GetString("HighName" + i) != null || PlayerPrefs.GetString("HighName" + i) != "")
            {
                highNameTable[i] = PlayerPrefs.GetString("HighName" + i);
            }

            else
            {
                highNameTable[i] = "AAA";
            }
        }
    }

    private void PopulateScoreCollection()
    {
        for (int i = 0; i < highScoreTable.Count; i++)
        {
            if (PlayerPrefs.GetInt("HighScore" + i) != null || PlayerPrefs.GetInt("HighScore" + i) != 0)
            {
                highScoreTable[i] = PlayerPrefs.GetInt("HighScore" + i);
            }

            else
            {
                highScoreTable[i] = 0;
            }
        }

        // After populate check if the actual score is enough to be recorded in the table
        CheckNewScore();
    }

    private void CheckNewScore()
    {
        for (int i = 0; i < highScoreTable.Count; i++)
        {
            // Finding where the current score fits into the table
            if (actualScore >= highScoreTable[i])
            {
                newPosition = i;
                inputFieldGO.SetActive(true);
                leadBoard.SetActive(false);

                // If is greater than first position
                if (i == 0 || actualScore <= highScoreTable[i - 1])
                {
                    UpdateNamePositions(i);
                    UpdateScorePositions(i);
                    break;
                }
            }
        }

        // Updating all texts UI to show for player
        UpdateTextUI();
    }

    // Updating all positions in the all list of high names
    private void UpdateNamePositions(int i)
    {
        for (int j = highScoreTable.Count - 1; j > i; j--)
        {
            highScoreTable[j] = highScoreTable[j - 1];
            PlayerPrefs.SetInt("HighScore" + j, highScoreTable[j - 1]);
        }

        highScoreTable[i] = actualScore;
        PlayerPrefs.SetInt("HighScore" + i, actualScore);
    }

    // Updating all positions in the all list of high scores
    private void UpdateScorePositions(int i)
    {
        for (int j = highNameTable.Count - 1; j > i; j--)
        {
            highNameTable[j] = highNameTable[j - 1];
            PlayerPrefs.SetString("HighName" + j, highNameTable[j - 1]);
        }

        highNameTable[i] = "";
    }

    // Updating all Texts of the board
    private void UpdateTextUI()
    {
        for (int i = 0; i < scoreTableText.Count; i++)
        {
            scoreTableText[i].text = highScoreTable[i].ToString();
        }

        for (int i = 0; i < nameTableText.Count; i++)
        {
            nameTableText[i].text = highNameTable[i];
        }
    }
}
