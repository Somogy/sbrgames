using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpringBoard : MonoBehaviour
{
    [Header("Spring Board attributes")]
    [SerializeField]
    private int bounceForce = 750;

    [SerializeField]
    private int springDurability = 3;

    [SerializeField]
    private List<Color> springColors;

    public LineRenderer lineConnection;
    public TextMeshProUGUI springNumber;

    [Header("Spring Area attributes"), SerializeField]
    private Renderer targetAreaRenderer;

    [SerializeField]
    private bool springStart = false;

    private Animator brokenAnim;

    private void Awake() => InitializeCache();

    private void Start()
    {
        if (!springStart)
        {
            if (springDurability > 1)
            {
                ChangeSpringBoardColor(springColors.Count - 1);
            }

            else
            {
                ChangeSpringBoardColor(springColors.Count - 2);
            }
        }        
    }

    private void InitializeCache()
    {
        brokenAnim = GetComponent<Animator>();
        lineConnection.SetPosition(0, new Vector3(transform.position.x, transform.position.y, transform.position.z));
    }

    private void LossDurability()
    {
        springDurability--;
        ChangeSpringBoardColor(springDurability);
    }

    public void SpringUsed()
    {
        PlayerMovement.instance.BoostJump(bounceForce);

        if (!springStart)
        {
            if (springDurability > 0)
            {
                LossDurability();
            }

            else
            {
                brokenAnim.enabled = true;
            }
        }
    }

    private void ChangeSpringBoardColor(int colorIndex)
    {
        targetAreaRenderer.material.SetColor("_Color", springColors[colorIndex]);
    }
}
