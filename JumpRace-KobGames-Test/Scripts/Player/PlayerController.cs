using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float minFingerInput = 0.025f;

    private float startFingerPosX, currentFingerPosX, startFingerPosY, currentFingerPosY;

    private PlayerMovement playerMoveScript;

    private void Awake() => InitializeCache();

    private void InitializeCache() => playerMoveScript = GetComponent<PlayerMovement>();

    private void Update() => RealizedInput(); 

    private void RealizedInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startFingerPosX = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
            startFingerPosY = Camera.main.ScreenToViewportPoint(Input.mousePosition).y;
        }

        if (Input.GetMouseButton(0))
        {
            // Rotation Movement
            currentFingerPosX = Camera.main.ScreenToViewportPoint(Input.mousePosition).x - startFingerPosX;

            if (startFingerPosX != currentFingerPosX && (currentFingerPosX >= minFingerInput || currentFingerPosX <= -minFingerInput))
            {
                playerMoveScript.RotatePlayer(currentFingerPosX);
            }

            // Forward movement
            currentFingerPosY = Mathf.Abs(Camera.main.ScreenToViewportPoint(Input.mousePosition).y - startFingerPosY);

            if (startFingerPosY != currentFingerPosY && (currentFingerPosY >= minFingerInput || currentFingerPosY <= -minFingerInput))
            {
                playerMoveScript.forwardInput = 0.5f;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            playerMoveScript.forwardInput = 0;
        }
    }
}