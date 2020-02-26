using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Publics Variables
    public float moveSpeed = 3;

    //Privates Variables
    private bool facingRight;
    private float horizontal, vertical;

    //Privates Components
    private Rigidbody2D playerRB2D;

    private void Awake()
    {
        GettingComponents();
    }

    private void Start()
    {
        facingRight = true;
    }

    private void FixedUpdate()
    {
        if (Player.instance.playerLive)
        {
            playerRB2D.velocity = new Vector2(horizontal * moveSpeed * Time.fixedDeltaTime, vertical * moveSpeed * Time.fixedDeltaTime);
        }
        
        else
        {
            playerRB2D.velocity = Vector2.zero;
        }
    }

    private void Update()
    {
        if (Player.instance.playerLive)
        {
            horizontal = Input.GetAxisRaw("Horizontal") * 30;
            vertical = Input.GetAxisRaw("Vertical") * 30;

            if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void GettingComponents()
    {
        playerRB2D = GetComponent<Rigidbody2D>();
    }
}
