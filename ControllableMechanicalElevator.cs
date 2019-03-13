using System.Collections;
using UnityEngine;

public class ControllableMechanicalElevator : MonoBehaviour
{
    // Audio Clips for elevator
    public AudioClip stopSound;
    
    public int requestedFloor;

    // Variables to floors
    private float firstFloor;
    private float secondFloor;
    private float thirdFloor;
    private float fourthFloor;
    private float fifthFloor;

    // Variables to use
    public float movement;
    private bool goingDown;
    private bool readyToRequest;
    private bool requestMade;
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
		// Taking game components
        elevatorPanel = GameObject.Find("ElevatorPanel");
        panelAnim = GameObject.Find("ElevatorPanel").GetComponent<Animator>();
        transportedObjectTransform = GetComponent<Transform>();
        elevatorAudio = GetComponent<AudioSource>();
        elevatorTransform = GetComponent<Transform>();
    }

    private void Start ()
    {
		// Initiating the arrays for transported objectos 
        transportedName = new string[10];
        transportedObject = new GameObject[10];

		// y-axis values
        readyToRequest = true;
        requestMade = false;
        firstFloor = 0.2f;
        secondFloor = 2f;
        thirdFloor = 3f;
        fourthFloor = 5f;
        fifthFloor = 7.76f;
    }

    private void Update ()
    {
		// Checking a requesting
        if (!requestMade && readyToRequest)
        {
            requestedFloor = elevatorPanel.GetComponent<ElevatorPanel>().requestedFloor; // Getting variable of elevator panel       
        }

		// Determining whether the requested floor is different so that the movement is initiated
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
    }

    private void Moving()
    {
        requestMade = false;
    
		// Checking whether the movement should be negative or positive on the y-axis
	    if (Time.timeScale != 0)
		{
			if (requestedFloor > actualFloor)        
            {
				goingDown = false;
				movement = 0.03f;        
			}
			
			else
			{
				goingDown = true;
				movement = -0.03f;
			}
		}
        
		// Case the game is paused, the moviment is 0
        if (Time.timeScale == 0)
        {
            movement = 0f;
        }

		// Only actrive in this range
        if (requestedFloor >= 1 && requestedFloor <= 5)
        {
            float[] floors = new float[] { firstFloor, secondFloor, thirdFloor, fourthFloor, fifthFloor }; //cria uma vetor com as coordenadas dos andares

            // It has to decrease 1 of the requestedFloor because the vector index starts at 0 and the floors at 1. Then index 0 = walk 1 = firsfloor
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

	// Checking collision with different objects to make the inertial object in relation to the elevator, transforming into child object
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

	// If the collision is over, reversing child object
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

	// Coroutine necessary to correctly handle the sound
    IEnumerator Arrived()
    {
        elevatorAudio.PlayOneShot(stopSound);
        yield return new WaitForSeconds(0.5f);
        elevatorAudio.enabled = false;
        readyToRequest = true;
    }
}
