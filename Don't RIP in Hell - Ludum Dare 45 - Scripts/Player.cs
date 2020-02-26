using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Isntance of this Script
    public static Player instance;

    //Public Variables
    public bool playerLive;

    //Publics Components  
    [SerializeField]
    private AudioClip playerDying;

    //Privates Components
    private AudioSource playerAudio;

    private void Awake()
    {
        instance = this;
        GettingComponents();
    }

    private void Start()
    {
        playerLive = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(PlayerDeath());
        }
    }

    private void GettingComponents()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    private IEnumerator PlayerDeath()
    {
        playerLive = false;
        playerAudio.PlayOneShot(playerDying);        
        yield return new WaitForSeconds(1f);
        GameManager.instance.GameOver();
    }
}
