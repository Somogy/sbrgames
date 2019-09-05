using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script checks the interaction of objects by pressing, to manipulate a second game object,
either by toggling between on and off or just one of two functions */

public class FloorButtons : MonoBehaviour
{
    // Audio clips for button
    public AudioClip heldSound;
    public AudioClip looseSound;

    // Sprites to use, up and down
    public Sprite heldDownSprite;
    public Sprite looseSprite;

    // Indicate the object to interact, on/off for example
    public bool activateObject; // Ativa o objeto e outro script

    // Variable to determine whether the object is manipulative as activated or not?
    public GameObject gameobjectDisable;

    // Variable to count how many elements are pressing the button
    private float elementsOn;

    // Components of button object to use
    private AudioSource buttonAudioSource;
    private SpriteRenderer actualSprite;

    private void Awake()
    {
        // Getting components in variables to facilitate the use
        GettingComponents();
    }
    
    private void Start()
    {
        // Initiating the number of objects at zero
        elementsOn = 0;
    }

    private void Update()
    {
        // If one or more other objects are on top of the button, set the activation
        ObjectsVerify();

        // If the manipulated object is disabled, it must be enabled if the variable indicates otherwise
        ObjectManipulation();
    }

    private void GettingComponents ()
    {
        actualSprite = GetComponent<SpriteRenderer>();
        buttonAudioSource = GetComponent<AudioSource>();
    }

    private void ObjectManipulation()
    {
        if (gameobjectDisable)
        {
            if (activateObject)
            {
                gameobjectDisable.SetActive(false);
            }

            else
            {
                gameobjectDisable.SetActive(true);
            }
        }
    }

    private void ObjectsVerify()
    {
        if (elementsOn >= 1)
        {
            activateObject = true;
            actualSprite.sprite = heldDownSprite;
        }

        else
        {
            activateObject = false;
            actualSprite.sprite = looseSprite;
        }
    }

    // Are there objects that are in contact using?
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.enabled)
        {
            elementsOn--;

            if (elementsOn == 0)
            {
                buttonAudioSource.PlayOneShot(heldSound);
            }
        }
    }

    // Are there objects that are not in contact using?
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.enabled)
        {
            elementsOn--;

            if (elementsOn == 1)
            {
                buttonAudioSource.PlayOneShot(looseSound);
            }
        }
    }
}
