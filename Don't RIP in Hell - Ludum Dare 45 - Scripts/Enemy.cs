using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Instance this Script
    public static Enemy instance;

    //Publics Variables
    public bool enemyLive;

    [SerializeField]
    private int enemyLife;

    //Public Components
    [SerializeField]
    private GameObject mobToSpawn;

    //Privates Components
    private CapsuleCollider2D enemyColl2D;
    private SpriteRenderer enemyRenderer;
    private Transform enemyTransf;

    private void Awake()
    {
        instance = this;
        enemyLive = true;

        GettingComponents();
    }

    private void Update()
    {
        if (enemyLife <= 0 && enemyLive)
        {
            StartCoroutine(Die());
        }
    }

    private void GettingComponents()
    {
        enemyColl2D = GetComponent<CapsuleCollider2D>();
        enemyRenderer = GetComponent<SpriteRenderer>();
        enemyTransf = GetComponent<Transform>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Mob")
        {
            enemyLife -= 1;
        }
    }

    private IEnumerator Die()
    {
        enemyLive = false;
        enemyColl2D.enabled = false;
        enemyRenderer.enabled = false;
        yield return new WaitForSeconds(1f);
        Instantiate(mobToSpawn, enemyTransf.transform.position, enemyTransf.transform.rotation);
        Destroy(this.gameObject);
    }
}
