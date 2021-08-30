using UnityEngine;

public class SpringBoardCollision : MonoBehaviour
{
    [SerializeField]
    private SpringBoard spScript;

    private bool canCheckJumpScore = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spScript.SpringUsed();
            ProgressCalculation.instance.ProgressBarUpdate(transform.position);

            if (canCheckJumpScore)
            {
                canCheckJumpScore = false;
                JumpScore.instance.CheckJumpScore(transform.InverseTransformPoint(collision.contacts[0].point));
            }
        }
    }
}
