using UnityEngine;

public class ProgressCalculation : MonoBehaviour
{
    public static ProgressCalculation instance;

    public Transform startPosition, endPosition;

    private float raceLength;

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

    private void Start() => raceLength = Vector3.Distance(startPosition.position, endPosition.position);

    public void ProgressBarUpdate(Vector3 playerCurrentPos)
    {
        UIManager.instance.ProgressBar(Mathf.Abs(Vector3.Distance(playerCurrentPos, endPosition.position) / raceLength - 1));
    }
}
