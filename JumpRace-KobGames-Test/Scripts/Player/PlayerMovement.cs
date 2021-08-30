using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [HideInInspector]
    public float forwardInput;

    [SerializeField]
    private float forwardSpeed, rotationMulty;

    [SerializeField]
    public Rigidbody playerRb;
    private Transform playerTransform;

    private void Awake() => InitializeCache();

    private void FixedUpdate()
    {
        if (forwardInput > 0)
        {
            MovePlayerForward();
        }
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

        playerRb = GetComponent<Rigidbody>();
        playerTransform = GetComponent<Transform>();
    }

    #region public methods
    public void BoostJump(int bounceForce) => playerRb.AddForce(0, bounceForce, 0);

    public void MovePlayerForward() => playerRb.AddForce(transform.forward * forwardSpeed * forwardInput * Time.deltaTime);

    public void RotatePlayer(float playerInputX) => playerTransform.Rotate(0, playerInputX * rotationMulty * Time.deltaTime, 0, Space.Self);
    #endregion
}
