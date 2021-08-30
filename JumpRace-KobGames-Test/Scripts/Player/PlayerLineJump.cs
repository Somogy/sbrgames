using UnityEngine;

public class PlayerLineJump : MonoBehaviour
{
    [SerializeField]
    private LayerMask layersToInteract;

    private LineRenderer lRenderer;

    private void Awake() => InitializeCache();

    private void Update() => CheckSpring();

    private void InitializeCache() => lRenderer = GetComponent<LineRenderer>();

    private void CheckSpring()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 250f, layersToInteract))
        {
            lRenderer.SetPosition(1, new Vector3(0, -hit.distance, 0));

            if (hit.collider.CompareTag("SpringBoards"))
            {
                ChangeJumpLineColor(Color.green);
            }

            if (hit.collider.CompareTag("SpringSafe"))
            {
                ChangeJumpLineColor(Color.yellow);
            }
        }

        else
        {
            ChangeJumpLineColor(Color.red);
            lRenderer.SetPosition(1, new Vector3(0, -100, 0));
        }
    }

    private void ChangeJumpLineColor(Color colorLineRenderer) => lRenderer.material.SetColor("_EmissionColor", colorLineRenderer);
}
