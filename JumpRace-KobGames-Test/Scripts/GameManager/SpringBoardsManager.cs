using System.Collections.Generic;
using UnityEngine;

public class SpringBoardsManager : MonoBehaviour
{
    public static SpringBoardsManager instance;

    [SerializeField]
    private List<GameObject> springsBoards;

    [HideInInspector]
    public List<Transform> springsBoardsTransform;

    private Vector3 nextBoardTransform;

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

        for (int i = 0; i < springsBoards.Count; i++)
        {
            springsBoardsTransform.Add(springsBoards[i].GetComponent<Transform>());
        }
    }

    private void Start() => NumberingConnectingLine();

    private void NumberingConnectingLine()
    {
        for (int i = 0; i < springsBoards.Count; i++)
        {
            if (i + 1 < springsBoards.Count)
            {
                nextBoardTransform = springsBoardsTransform[i + 1].position;
            }

            SpringBoard board = springsBoards[i].GetComponent<SpringBoard>();

            board.springNumber.text = (i + 1).ToString();
            board.GetComponent<SpringBoard>().lineConnection.SetPosition(1, new Vector3(nextBoardTransform.x, nextBoardTransform.y, nextBoardTransform.z));
        }
    }
}
