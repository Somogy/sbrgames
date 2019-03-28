using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public bool alive, laserDeath, punchDone, touchRedLine;
    public float speedMove;
    private Vector2 touchPos;

    public Animator playerAnim;
    public AudioSource playerAudio;
    public BoxCollider2D handObject, playerArea;
    public CapsuleCollider2D playerColl;
    public Rigidbody2D playerRB;
    public Transform playerTransf;
    
    private void Start()
    {
        alive = true;
        instance = this;
        laserDeath = false;
        punchDone = true;
        touchRedLine = false;
    }
    
    private void Update()
    {
        if (Input.GetButton("Fire1") && GameManager.instance.fight)
        {
            touchPos = Input.mousePosition;
            PlayerControl();
        }

        else
        {
            playerAnim.SetBool("Walk", false);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Punch();
        }
    }

    public void EnemyHit()
    {
        alive = false;
        playerAnim.SetBool("EnemyHit", true);

        if (!touchRedLine)
        {
            playerRB.AddForce(new Vector2(-2000f, 0f));
        }
    }

    private void PlayerControl()
    {
        touchPos = Camera.main.ScreenToWorldPoint(touchPos);

        if (touchPos.x > -8.7f && touchPos.x < 3.6f && touchPos.y > -4.8f && touchPos.y < 2.1f && punchDone && alive)
        {
            playerTransf.position = Vector2.Lerp(playerTransf.position, touchPos, speedMove * Time.deltaTime);
            playerAnim.SetBool("Walk", true);
        }
    }

    public void Punch()
    {
        if (punchDone && alive && GameManager.instance.fight)
        {
            playerColl.offset = new Vector2(0.37f, 0.6021827f);
            handObject.enabled = true;
            playerAnim.SetBool("Punch", true);
            StartCoroutine(PunchCD());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {       
        if (collision.gameObject.tag == "Lethal" && punchDone)
        {            
            EnemyHit();
            touchRedLine = true;
        }

        if (collision.gameObject.tag == "Laser" && alive)
        {
            EnemyHit();
            laserDeath = true;
        }
    }

    private IEnumerator PunchCD()
    {        
        punchDone = false;
        playerAudio.Play();
        yield return new WaitForSeconds(0.5f);
        handObject.enabled = false;
        playerAnim.SetBool("Punch", false);
        playerArea.offset = new Vector2(0.6f, 0.4811941f);
        punchDone = true;
    }
}
