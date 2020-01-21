using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Instance this
    public static PlayerMove instance;

    // Public Variables
    [Header("Walk speed of player")]
    public float walkSpeed;

    [Header("Jump force of player")]
    public float jumpForce;

    // Private Variables
    private bool grounded; // For check grounded
    private bool readyToAttack; // Countdown to player can attack again
    private float horizontalForceButton; // To get axis

    // Private Variables
    private Rigidbody2D playerRb2D;

    [Header("Determine what is ground for the character")]
    public LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;

    private void Awake()
    {
        // Instance this for access in another scripts
        instance = this;

        GettingComponents();
    }

    private void Start()
    {
        // Initial values for variables
        InitialVariablesValues();
    }

    private void FixedUpdate()
    {
        Walking();             
    }

    private void Update()
    {
        // Ground Check
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, whatIsGround);

        if (grounded)
        {
            // Attack can eliminate the slimes
            if (Input.GetButton("Fire1") && readyToAttack)
            {
                StartCoroutine(Attack());
            }

            // Jump method with space button
            if (Input.GetKey(KeyCode.Space) && grounded)
            {
                Jump();
            }
        }

        // Checking the input by player
        InputDirection();

        // Manipulante the animations
        SettingAnimation();
        
    }

    private IEnumerator Attack()
    {
        readyToAttack = false;
        Player.instance.playerAnim.SetBool("attack", true);
        Player.instance.playerAudio.PlayOneShot(Player.instance.attackSound);

        yield return new WaitForSeconds(0.31f);
        Player.instance.playerAnim.SetBool("attack", false);

        yield return new WaitForSeconds(2f);
        readyToAttack = true;
    }

    // Getting components to use later
    private void GettingComponents()
    {
        playerRb2D = GetComponent<Rigidbody2D>();
    }

    // Initial values for variables
    private void InitialVariablesValues()
    {
        grounded = true;
        readyToAttack = true;
    }

    // Method of axis input
    private void InputDirection()
    {
        horizontalForceButton = Input.GetAxisRaw("Horizontal");
    }

    // Jump method, checking grounded to use in character animation
    private void Jump()
    {
        Player.instance.playerAudio.PlayOneShot(Player.instance.jump);
        grounded = false;
        Player.instance.playerAnim.SetBool("grounded", false);
        playerRb2D.AddForce(new Vector2(0f, jumpForce));
    }

    // Manipulante the animations
    private void SettingAnimation()
    {
        if (grounded)
        {
            Player.instance.playerAnim.SetBool("grounded", true);
            Player.instance.playerAnim.SetFloat("moveSpeed", horizontalForceButton);
        }
    }

    // Apply physics to character
    private void Walking()
    {        
        // Movement of player
        playerRb2D.velocity = new Vector2(horizontalForceButton * walkSpeed, playerRb2D.velocity.y);
    }
}
