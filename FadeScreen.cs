using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    // Instantiating this script for external access
    public static UIFade instance;

    // Variables to determine the better speed of fade and the frame to fade effect
    public float fadeSpeed;
    public Image fadeScreen;

    // Bools to know which fade to use
    public bool fadeToBlack;
    public bool fadeFromBlack;

	
	private void Start ()
    {
	// Instantiating this script for external access
        instance = this;
	}	
	
	private void Update ()
    {
	// Fading to black
        if (fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
			
            if (fadeScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }

	// Fading from black
        if (fadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 0f)
            {
                fadeFromBlack = false;
            }
        }
    }

    // Methods to vary between toblack or fromblack
    private void FadeToBlack()
    {
        fadeToBlack = true;
        fadeFromBlack = false;
    }

    private void FadeFromBlack()
    {
        fadeToBlack = false;
        fadeFromBlack = true;
    }
}
