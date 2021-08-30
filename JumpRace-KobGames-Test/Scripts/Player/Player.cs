using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    private PlayerController playerControl;
    private PlayerMovement playerMove;

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

        playerControl = GetComponent<PlayerController>();
        playerMove = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (transform.position.y < -2)
        {
            GameManager.instance.Restart();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            FinishedRace();
        }
    }

    private void FinishedRace()
    {
        GameManager.instance.EndLevel();
        ControlPlayer(true);
    }

    public void ControlPlayer(bool active)
    {
        playerControl.enabled = active;
        playerMove.enabled = active;
        PlayerMovement.instance.playerRb.useGravity = true;
    }
}
