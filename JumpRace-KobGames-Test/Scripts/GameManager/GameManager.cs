using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private int nextStageIndex;

    [SerializeField]
    private List<CompetitorIA> competitors;

    private void Awake() => InitializeCache();

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

    public void PressingStartBtn()
    {
        Player.instance.ControlPlayer(true);

        foreach (var item in competitors)
        {
            item.enabled = true;
        }
    }

    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void EndLevel()
    {
        FinishLine.instance.ThrowConfetti();
        UIManager.instance.ProgressBar(1);
        UIManager.instance.StartCoroutine(UIManager.instance.ChangeScorePanel("LEVEL COMPLETE!!!", 2));
        StartCoroutine(ToNextLelve());
    }    

    private IEnumerator ToNextLelve()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(nextStageIndex);
    }
}
