using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField]
    private Image barImage;

    [SerializeField]
    private Text scoreText;

    private void Awake()
    {
        InitializeCache();

        barImage.fillAmount = 0;
    }

    private void InitializeCache()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;
        }
    }

    public void ProgressBar(float raceProgress) => barImage.fillAmount = raceProgress;

    public IEnumerator ChangeScorePanel(string scoreType, float timeToShow)
    {
        scoreText.text = scoreType;

        yield return new WaitForSeconds(timeToShow);

        scoreText.text = "";
    }
}
