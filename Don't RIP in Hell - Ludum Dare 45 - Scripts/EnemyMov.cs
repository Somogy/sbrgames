using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMov : MonoBehaviour
{
    //Publics Variables
    public float moveSpeed = 3;
    private bool facingRight;

    //Private Variables
    private Vector2 direction;

    //Privates Components
    private GameObject player;
    private Rigidbody2D mobRB2D;
    private Rigidbody2D PlayerRB2D;
    private float horizontal;

    private void Awake()
    {
        facingRight = true;
        player = GameObject.Find("Player");
        GettingComponents();
    }

    private void FixedUpdate()
    {
        if (Player.instance.playerLive)
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        direction = PlayerRB2D.position - mobRB2D.position;
        mobRB2D.MovePosition(mobRB2D.position + direction.normalized * moveSpeed * Time.fixedDeltaTime);

        horizontal = PlayerRB2D.position.x - mobRB2D.position.x;

        if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
        {
            Flip();
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
        mobRB2D = GetComponent<Rigidbody2D>();
        PlayerRB2D = player.GetComponent<Rigidbody2D>();
    }
}
