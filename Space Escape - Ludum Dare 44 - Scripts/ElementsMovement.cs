using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsMovement : MonoBehaviour
{
    // Public variables
    [Tooltip("Base speed of the ship")]
    public float speedMov;

    // Private components
    private Transform thisObjTransf;

    private void Awake()
    {
        thisObjTransf = GetComponent<Transform>();
        speedMov = PlayerPrefs.GetFloat("GameSpeed");
    }

    private void Update()
    {     
        if (thisObjTransf.transform.position.x <= -20f)
        {
            Destroy(gameObject);
        }

        if (Time.timeScale == 0)
        {
            thisObjTransf.transform.position = new Vector2(transform.position.x, transform.position.y);
        }

        else
        {
            Movement();
        }
    }

    public void Movement()
    {
        thisObjTransf.transform.position = new Vector2(thisObjTransf.transform.position.x - speedMov * Time.deltaTime, thisObjTransf.transform.position.y);
    }
}
