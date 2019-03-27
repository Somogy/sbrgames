using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy instance;

    public bool alive, punchDone, playerAttack;
    public float punchRecovery, punchSpeed, speedMove, timer, timerMax;
    private Vector2 newPath;

    public Animator enemyAnim;
    public AudioSource enemyAudio;
    public BoxCollider2D handObject, playerAttColl, playerArea;
    public CapsuleCollider2D enemyColl;
    public Rigidbody2D enemyRB;
    public SpriteRenderer enemyRender;
    public Transform enemyTransf;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("PlayerScore") <= 2)
        {
            handObject.offset = new Vector2(0f, 0f);
            punchRecovery = 0.25f;
            punchSpeed = 1f;
            timerMax = 1.5f;
        }

        if (PlayerPrefs.GetInt("PlayerScore") >= 4)
        {
            punchRecovery = 0.20f;
            speedMove = 3f;
        }

        if (PlayerPrefs.GetInt("PlayerScore") >= 6)
        {
            punchSpeed = 0.5f;
            timerMax = 1f;
        }

        if (PlayerPrefs.GetInt("PlayerScore") >= 8)
        {
            punchRecovery = 0.15f;
            speedMove = 3.5f;
        }

        if (PlayerPrefs.GetInt("PlayerScore") >= 10)
        {
            punchRecovery = 0.1f;
            punchSpeed = 0.25f;
            timerMax = 0.5f;
        }

        if (PlayerPrefs.GetInt("PlayerScore") >= 12)
        {
            speedMove = 5f;
            punchRecovery = 0f;
            punchSpeed = 0.1f;
            timerMax = 0.1f;

            handObject.offset = new Vector2(0.06f, 0f);
        }
    }

    private void Start()
    {
        instance = this;
        alive = true;
        playerAttack = false;
        punchDone = true;
        timer = 0.5f;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (alive && GameManager.instance.fight)
        {
            Move();                       
        }
    }

    private void BackOff()
    {
        punchDone = false;
        timer = 0;
        newPath = new Vector2(Random.Range(0.5f, 7.5f), Random.Range(-6.29f, 1.15f));

        Debug.Log("Defesa");
    }

    public void Move()
    {
        if (timer <= 0 && punchDone)
        {
            newPath = new Vector2(Random.Range(0f, 5f), Random.Range(-6.29f, 1.15f));
            timer = Random.Range(0, timerMax);
        }

        if (timer > 0 && punchDone)
        {
            enemyTransf.position = Vector2.Lerp(enemyTransf.position, newPath, speedMove * Time.deltaTime);
            enemyAnim.SetBool("Walk", true);
        }

        if (enemyTransf.position.x == 0 || enemyTransf.position.y == 0)
        {
            enemyAnim.SetBool("Walk", false);
        }
    }

    public void PlayerHit()
    {
        alive = false;
        enemyRB.AddForce(new Vector2(2000f, 0f));
        enemyAnim.SetBool("PlayerHit", true);
    }

    private void Punch()
    {
        if (punchDone && alive && GameManager.instance.fight)
        {            
            StartCoroutine(PunchCD());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (alive)
        {
            if (playerArea && !playerAttack)
            {
                Punch();
            }

            if (playerArea && playerAttColl.isActiveAndEnabled && PlayerPrefs.GetInt("PlayerScore") >= 8)
            {
                BackOff();
            }
        }        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (playerArea && alive)
        {
            Punch();
        }
    }

    private IEnumerator PunchCD()
    {        
        punchDone = false;

        yield return new WaitForSeconds(punchSpeed);
        handObject.enabled = true;
        enemyAnim.SetBool("Punch", true);
        enemyColl.offset = new Vector2(0.37f, 0.6021827f);
        enemyAudio.Play();

        yield return new WaitForSeconds(punchRecovery);
        handObject.enabled = false;
        enemyAnim.SetBool("Punch", false);
        enemyColl.offset = new Vector2(0, 0.6021827f);
        punchDone = true;
    }
}
