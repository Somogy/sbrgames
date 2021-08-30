using UnityEngine;

public class CompetitorIA : MonoBehaviour
{
    [SerializeField]
    private int bounceForce, forwardSpeed, jumpTimes;

    private bool canMove = false;
    private int jumpRepeat, springBoardList;
    private int springIndex = 0;

    private Rigidbody competitorRb;
    private Transform nextDestination;

    private void Awake() => InitializeCache();

    //private void Start() => competitorRb.useGravity = true;

    private void InitializeCache()
    {
        jumpRepeat = jumpTimes;
        competitorRb = GetComponent<Rigidbody>();
        springBoardList = SpringBoardsManager.instance.springsBoardsTransform.Count;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            MoveFoward();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("SpringBoards"))
        {
            BoostingEffect();

            if (this.enabled)
            {
                if (jumpTimes > 0)
                {
                    jumpTimes--;
                    canMove = false;
                }

                else if (jumpTimes == 0)
                {
                    canMove = true;
                    jumpTimes = jumpRepeat;
                    CheckSpringIndex(springIndex++);
                }
            }            
        }
    }

    public void BoostingEffect() => competitorRb.AddForce(0, bounceForce, 0);

    private void CheckSpringIndex(int index)
    {
        if (index < springBoardList - 1)
        {
            nextDestination = SpringBoardsManager.instance.springsBoardsTransform[springIndex];
        }

        else
        {
            nextDestination = ProgressCalculation.instance.endPosition;
        }
    }

    private void MoveFoward()
    {
        competitorRb.AddForce(forwardSpeed * Time.deltaTime * (nextDestination.position - transform.position));
    }
}
