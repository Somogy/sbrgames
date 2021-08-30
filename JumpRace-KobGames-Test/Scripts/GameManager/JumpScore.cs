using UnityEngine;

public class JumpScore : MonoBehaviour
{
    public static JumpScore instance;

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

    public void CheckJumpScore(Vector3 jumpPosition)
    {
        if (jumpPosition.x > -0.05f && jumpPosition.x < 0.05f &&
            jumpPosition.z > -0.05f && jumpPosition.z < 0.05f)
        {
            ChangeTextScore("EXCELLENT");
        }

        if (jumpPosition.x > 0.05f && jumpPosition.x < 0.25f ||
            jumpPosition.x < -0.05f && jumpPosition.x > -0.25f ||
            jumpPosition.z > 0.05f && jumpPosition.z < 0.25f ||
            jumpPosition.z < -0.05f && jumpPosition.z > -0.25f)
        {
            ChangeTextScore("GOOD");
        }
    }

    private void ChangeTextScore(string text)
    {
        UIManager.instance.StartCoroutine(UIManager.instance.ChangeScorePanel(text, 0.5f));
    }
}
