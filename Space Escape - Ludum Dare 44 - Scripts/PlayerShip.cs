using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShip : MonoBehaviour
{
    // Public variables
    [Header("Fuel Expense")]
    public float boost;
    public float back;
    public float down;
    public float foward;
    public float fire;
    public float up;

    [Header("Others Specs")]
    public float fireRate;
    public float fuel;

    [Tooltip("Base speed of the ship")]
    public float speedMove;

    // Public components
    public GameObject laserShip;
    public GameObject laserSpawnPoint;
    public ParticleSystem shipParticles;
    public ParticleSystem[] turbinesParticles;
    public Slider fuelGauge;

    // Private variables
    private bool alertSound;
    private bool readyToShot;
    private float horizontalForceButton, verticalForceButton;

    // Private components
    private Animator shipAnim;
    private Rigidbody2D shipRb2d;

    private void Awake()
    {
        shipAnim = GetComponent<Animator>();
        shipRb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        alertSound = true;
        readyToShot = true;
        fuel *= 100;
        speedMove *= 150;
    }

    private void Update()
    {
        FuelVerification();

        Movement();

        if (readyToShot && Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(LaserShot());
        }

        fuelGauge.value = 100 - fuel;

        //fuelGauge = new Vector2(0 +1.54f, 14.14f);


        fuel -= foward;
    }
    public void FuelVerification()
    {
        if (fuel > 100f)
        {
            fuel = 100f;
        }

        if (fuel >= 1)
        {
            for (int i = 0; i < turbinesParticles.Length; i++)
            {
                turbinesParticles[i].Play();
            }
        }

        if (fuel <= 25f && alertSound)
        {
            alertSound = false;
            shipRb2d.gravityScale = 1f;

            StartCoroutine(LowFuel());
        }

        if (fuel <= 15f)
        {
            alertSound = false;
            shipRb2d.gravityScale = 4f;
        }

        if (fuel <= 0)
        {
            shipRb2d.gravityScale = 16f;

            for (int i = 0; i < turbinesParticles.Length; i++)
            {
                turbinesParticles[i].Stop();
            }
        }
    }
    public void Movement()
    {
        if (fuel >= 0)
        {
            verticalForceButton = Input.GetAxis("Vertical") * Time.deltaTime;
        }

        horizontalForceButton = Input.GetAxis("Horizontal") * Time.deltaTime;

        // HorizontalMovement
        // While the game is pressing the right button the ship navigates to the right, if you press space in the meantime, the boost will be triggered
        if (horizontalForceButton >= 0.0001f)
        {
            // For boost
            if (Input.GetKey(KeyCode.Space) && fuel >= 0)
            {
                shipRb2d.velocity = new Vector2(horizontalForceButton * speedMove * 4, shipRb2d.velocity.y);
                fuel -= boost;
            }
            // Without boost
            else
            {
                shipRb2d.velocity = new Vector2(horizontalForceButton * speedMove * 1.5f, shipRb2d.velocity.y);
            }
        }

        // In case of using the left button, the ship can slow down a bit
        else if(horizontalForceButton <= -0.0001f)
        {
            shipRb2d.velocity = new Vector2(horizontalForceButton * speedMove * 0.75f, shipRb2d.velocity.y);
            fuel += back;
        }

        else if (horizontalForceButton == 0)
        {
            shipRb2d.velocity = new Vector2(shipRb2d.velocity.x, shipRb2d.velocity.y);
        }

        // VerticalMovement
        // Here verifies the vertical axis of the controls, down is faster due to the help of gravity
        if (verticalForceButton >= 0.0001f)
        {
            shipRb2d.velocity = new Vector2(shipRb2d.velocity.x, verticalForceButton * speedMove);
            fuel -= up;
        }

        else if (verticalForceButton <= -0.0001f)
        {
            shipRb2d.velocity = new Vector2(shipRb2d.velocity.x, verticalForceButton * speedMove * 2f);
            fuel += down;
        }

        else if (verticalForceButton == 0)
        {
            shipRb2d.velocity = new Vector2(shipRb2d.velocity.x, shipRb2d.velocity.y * 0);
        }

        // Animations
        if (horizontalForceButton >= 0.001f && verticalForceButton >= 0.001f)
        {
            shipAnim.SetInteger("VerticalAxis", 1);
        }

        if (horizontalForceButton <= -0.001f && verticalForceButton >= 0.001f)
        {
            shipAnim.SetInteger("VerticalAxis", -1);
        }

        if (horizontalForceButton >= 0.001f && verticalForceButton <= -0.001f)
        {
            shipAnim.SetInteger("VerticalAxis", -1);
        }

        if (horizontalForceButton <= -0.001f && verticalForceButton <= -0.001f)
        {
            shipAnim.SetInteger("VerticalAxis", 1);
        }

        if (verticalForceButton == 0f)
        {
            shipAnim.SetInteger("VerticalAxis", 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Obstacle")
        {
            StartCoroutine(GameOver());
        }

        if (collision.gameObject.tag == "Fuel")
        {
            fuel += 25f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(GameOver());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ScreenLimit")
        {
            StartCoroutine(GameOver());
        }
    }

    private IEnumerator GameOver()
    {
        shipParticles.Play();
        AudioManager.instance.PlaySFX(0);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private IEnumerator LaserShot()
    {
        fuel -= fire;
        readyToShot = false;
        Instantiate(laserShip, laserSpawnPoint.transform.position, laserSpawnPoint.transform.rotation);

        yield return new WaitForSeconds(fireRate);
        readyToShot = true;
    }

    private IEnumerator LowFuel()
    {
        AudioManager.instance.PlaySFX(3);
        yield return new WaitForSeconds(2f);
        alertSound = true;
    }
}
