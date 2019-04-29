using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShot : MonoBehaviour
{
    // Public variables
    public float shotSpeed;

    public bool playerShot;
    private void Start()
    {
        if (playerShot)
        {
            shotSpeed *= 0.05f;
        }

        else
        {
            shotSpeed *= -0.05f;
        }
    }
    private void Update()
    {
        if (playerShot)
        {
            transform.position = new Vector2(transform.position.x + shotSpeed, transform.position.y);

            if (transform.position.x >= 9.15f)
            {
                Destroy(gameObject);
            }
        }

        else
        {
            transform.position = new Vector2(transform.position.x + shotSpeed, transform.position.y);

            if (transform.position.x <= -9f)
            {
                Destroy(gameObject);
            }
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Fuel")
        {
            AudioManager.instance.PlaySFX(1);
            Destroy(this.gameObject);
        }
    }
}
