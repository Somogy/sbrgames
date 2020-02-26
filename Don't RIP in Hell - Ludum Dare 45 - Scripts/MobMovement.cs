using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : MonoBehaviour
{
    //Publics Variables
    private bool facingRight;
    public float moveSpeed = 3;

    public float minPlayerDistance = 1.5f;
    public float maxPlayerDistance = 1.8f;

    //Private Variables
    private float distancia;
    private Vector2 direcao;

    //Privates Components
    private Animator mobAnim;
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

    private void Update()
    {
        if (mobRB2D.velocity.x != 0 || mobRB2D.velocity.y != 0)
        {
            mobAnim.SetBool("Walk", true);
        }

        else
        {
            mobAnim.SetBool("Walk", false);
        }
    }

    private void FixedUpdate()
    {
       if (Player.instance.playerLive)
       {
            FollowPlayer();
       }
    }

    private void FollowPlayer()
    {
        distancia = Vector2.Distance(mobRB2D.position, PlayerRB2D.position);

        if (distancia > maxPlayerDistance && distancia > minPlayerDistance)
        {
            direcao = PlayerRB2D.position - mobRB2D.position;

            horizontal = PlayerRB2D.position.x - mobRB2D.position.x;

            if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
            {
                Flip();
            }
        }
        else if (distancia < maxPlayerDistance)
        {
            direcao = Vector2.zero;

            if (distancia < minPlayerDistance)
            {
                direcao = mobRB2D.position - PlayerRB2D.position;
            }

            horizontal = mobRB2D.position.x - PlayerRB2D.position.x;

            if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
            {
                Flip();
            }
        }

        mobRB2D.MovePosition(mobRB2D.position + direcao.normalized * moveSpeed * Time.fixedDeltaTime);


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
        mobAnim = GetComponent<Animator>();
        mobRB2D = GetComponent<Rigidbody2D>();
        PlayerRB2D = player.GetComponent<Rigidbody2D>();
    }
}
