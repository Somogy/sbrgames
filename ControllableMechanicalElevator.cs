using System.Collections;
using UnityEngine;

public class ControllableMechanicalElevator : MonoBehaviour
{
    [Header("Audio Clips for elevator")]
    public AudioClip stopSound;
    
    public int requestedFloor;

    // Floats to floors
    private float firstFloor;
    private float secondFloor;
    private float thirdFloor;
    private float fourthFloor;
    private float fifthFloor;

    // Variables to use
    private bool goingDown;
    private bool readyToRequest;
    private bool requestMade;
    public float movement;
    private int actualFloor;

    // Transported Object Components
    public string desTransportedName;
    public string[] transportedName;
    public GameObject[] transportedObject;
    private Transform transportedObjectTransform;

    // Components to use
    private Animator panelAnim;
    private AudioSource elevatorAudio;
    private GameObject elevatorPanel;
    private Transform elevatorTransform;

    private void Awake()
    {
        elevatorPanel = GameObject.Find("ElevatorPanel");
        panelAnim = GameObject.Find("ElevatorPanel").GetComponent<Animator>();
        transportedObjectTransform = GetComponent<Transform>();

        elevatorAudio = GetComponent<AudioSource>();
        elevatorTransform = GetComponent<Transform>();
    }

    private void Start ()
    {
        transportedName = new string[10];
        transportedObject = new GameObject[10];

        readyToRequest = true;
        requestMade = false;
        firstFloor = 0.222f;
        secondFloor = 2.084f;
        thirdFloor = 3.954f;
        fourthFloor = 5.87f;
        fifthFloor = 7.76f;
    }

    private void Update ()
    {
        if (!requestMade && readyToRequest)
        {
            requestedFloor = elevatorPanel.GetComponent<ElevatorPanel>().requestedFloor; // variavelLocal = gameObjectAcessadoAqui.GetComponent<NomeDoScript>().variavelRemota;            
        }

        if (requestedFloor != actualFloor)
        {
            readyToRequest = false;
            requestMade = true;
            panelAnim.SetInteger("RequestedFloor", requestedFloor);
        }

        if (requestMade)
        {
            Moving();
        }

        if (movement != 0f)
        {
            elevatorAudio.enabled = true;
        }

        transform.position = new Vector2(transform.position.x, transform.position.y + movement);
        //Debug.Log("Andar selecionado: " + requestedFloor);
        //Debug.Log("Andar atual: " + actualFloor);
        //Debug.Log("Velocidade atual: " + movement);
        //Debug.Log("Solicitação realizada: " + requestMade);
    }

    private void Moving()
    {
        requestMade = false;
    
        if ((requestedFloor > actualFloor) && Time.timeScale != 0)
        {
            goingDown = false;
            movement = 3f / 100;
        }

        if ((requestedFloor < actualFloor) && Time.timeScale != 0)
        {
            goingDown = true;
            movement = -3f / 100;
        }

        if (Time.timeScale == 0)
        {
            movement = 0f;
        }

        if (requestedFloor >= 1 && requestedFloor <= 5) //só aciona nesse intervalo
        {
            float[] floors = new float[] { firstFloor, secondFloor, thirdFloor, fourthFloor, fifthFloor }; //cria uma vetor com as coordenadas dos andares

            // Tem que diminuir 1 do requestedFloor porque o indice do vetor comeca em 0 e os andares em 1. Então indice 0 = andar 1 = firsfloor
            if ((elevatorTransform.localPosition.y >= floors[requestedFloor - 1] && !goingDown) || (elevatorTransform.localPosition.y <= floors[requestedFloor - 1] && goingDown))
            {
                Move();
            }
        }
    }

    private void Move()
    {
        StartCoroutine("Arrived");
        panelAnim.SetInteger("RequestedFloor", 0);
        actualFloor = requestedFloor;
        movement = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.enabled && (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Objects" || collision.gameObject.tag == "Lethal"))
        {
            for (int i = 0; i < transportedName.Length; i++)
            {
                if (transportedObject[i] == null)
                {
                    transportedName[i] = collision.gameObject.name;
                    transportedObject[i] = GameObject.Find(transportedName[i]);
                    transportedObject[i].transform.SetParent(transportedObjectTransform);
                    return;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Objects" || collision.gameObject.tag == "Lethal")
        {
            desTransportedName = collision.gameObject.name;

            for (int i = 0; i < transportedName.Length; i++)
            {
                if (transportedName[i] == desTransportedName)
                {
                    transportedObject[i].transform.parent = null;
                    transportedName[i] = "";
                    transportedObject[i] = null;
                }
            }
        }
    }

    IEnumerator Arrived()
    {
        elevatorAudio.PlayOneShot(stopSound);
        yield return new WaitForSeconds(0.5f);
        elevatorAudio.enabled = false;
        readyToRequest = true;
    }
}
